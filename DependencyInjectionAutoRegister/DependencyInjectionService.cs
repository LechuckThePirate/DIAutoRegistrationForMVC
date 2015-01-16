using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using DependencyInjection.ServiceLibrary.Classes;
using DependencyInjection.ServiceLibrary.Interfaces;
using DependencyInjectionAutoRegister.Entities;

namespace DependencyInjectionAutoRegister
{
    public class DependencyInjectionService
    {
        private IDependencyInjectionEngine _engine;
        protected IDependencyInjectionEngine Engine
        {
            get { return _engine; }
        }

        public DependencyInjectionService(IDependencyInjectionEngine engine)
        {
            _engine = engine;
        }

        public virtual void RegisterDependencies(DependencyInjectionSetting options)
        {
            var assemblies = new List<Assembly>();
            assemblies.AddRange(GetReferencedAssemblies(options.ReferencedAssemblyFilter));
            assemblies.AddRange(GetNonReferencedAssemblies(options.NonReferencedAssemblyFilter));
            assemblies.SelectMany(a => a.GetTypes())
                .Where(t => t.GetCustomAttributes(typeof(DependencyInjectionRegisterAttribute), false).Any())
                .Distinct()
                .ToList()
                .ForEach(RegisterType);

            options.ClassesToRegister.ForEach(RegisterClass);

            Engine.BuildDependencies();

        }

        protected RegistrationTypeEnum GetRegistrationType(Type t)
        {
            var attr = t.GetCustomAttributes(typeof(DependencyInjectionRegisterAttribute), false).FirstOrDefault()
                as DependencyInjectionRegisterAttribute;
            attr = attr ?? new DependencyInjectionRegisterAttribute();
            return attr.RegistrationType;
        }

        protected virtual void RegisterType(Type t)
        {
            t.GetInterfaces().ToList().ForEach(i => _engine.Register(t, i, GetRegistrationType(t)));
        }

        protected virtual void RegisterClass(Type t)
        {
            _engine.Register(t, GetRegistrationType(t));
        }

        private IEnumerable<Assembly> GetReferencedAssemblies(Func<string,bool> filter) 
        {
            return AppDomain.CurrentDomain.GetAssemblies()
                .Where(a => filter != null && filter(a.GetName().Name));
        } 

        private IEnumerable<Assembly> GetNonReferencedAssemblies(Func<string,bool> filter)
        {
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bin");
            return Directory.GetFiles(path, "*.dll")
                .Concat(Directory.GetFiles(path, "*.exe"))
                .Where(file => filter != null && filter(file))
                .Select(Assembly.LoadFrom);
        }

        protected virtual void Register<TClass>(Type @interface) where TClass : class
        {
            var attr = typeof (TClass).GetCustomAttributes(typeof(DependencyInjectionRegisterAttribute), false).FirstOrDefault()
                as DependencyInjectionRegisterAttribute;
            _engine.Register(typeof(TClass), @interface, (attr != null) ? attr.RegistrationType : RegistrationTypeEnum.Default);
        }

    }
}
