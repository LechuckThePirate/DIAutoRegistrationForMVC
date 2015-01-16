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

        private IEnumerable<Type> _allTypes;

        public DependencyInjectionService(IDependencyInjectionEngine engine)
        {
            _engine = engine;
        }

        public virtual void RegisterDependencies(DependencyInjectionSetting options)
        {
            var assemblies = new List<Assembly>();
            assemblies.AddRange(GetReferencedAssemblies(options.AssemblyFilter));
            assemblies.AddRange(GetNonReferencedAssemblies(options.ExternalAssemblyFilter));

            _allTypes = assemblies.SelectMany(a => a.ExportedTypes);
            var typesToRegister = _allTypes
                .Where(t => t.GetCustomAttributes(typeof(DependencyInjectionRegisterAttribute), false).Any()
                         && !t.GetCustomAttributes(typeof(DependencyInjectionIgnoreAttribute),false).Any())
                .Distinct().ToList();

            typesToRegister.Where(t => t.IsInterface).ToList()
                .ForEach(RegisterInterface);

            typesToRegister.Where(t => t.IsClass && !t.IsAbstract).ToList()
                .ForEach(RegisterClass);

            if (options.ClassesForcedToRegister!= null)
                options.ClassesForcedToRegister.ForEach(RegisterClassAsSelf);

            Engine.BuildDependencies();

        }

        protected RegistrationTypeEnum GetRegistrationType(Type t)
        {
            var attr = t.GetCustomAttributes(typeof(DependencyInjectionRegisterAttribute), false).FirstOrDefault()
                as DependencyInjectionRegisterAttribute;
            attr = attr ?? new DependencyInjectionRegisterAttribute();
            return attr.RegistrationType;
        }

        protected virtual void RegisterClass(Type t)
        {
            if (t.GetInterfaces().Any())
                t.GetInterfaces()
                    .Where(i => !i.GetCustomAttributes(typeof(DependencyInjectionIgnoreAttribute),false).Any())
                    .ToList()
                    .ForEach(i => _engine.Register(t, i, GetRegistrationType(t)));
            else
                RegisterClassAsSelf(t);
        }

        protected virtual void RegisterClassAsSelf(Type t)
        {
            _engine.Register(t, GetRegistrationType(t));
        }

        protected virtual void RegisterInterface(Type @interface)
        {
            _allTypes.Where(t => !t.IsAbstract && t.IsClass && @interface.IsAssignableFrom(t))
                .ToList()
                .ForEach(t => _engine.Register(t,@interface, GetRegistrationType(t)));
        }

        private bool IsAnyMatch(string s, IEnumerable<string> matches)
        {
            return (matches != null && matches.Any(m => s.Equals(m) || s.StartsWith(m)));
        }

        private IEnumerable<Assembly> GetReferencedAssemblies(IEnumerable<string> filter ) 
        {
            return AppDomain.CurrentDomain.GetAssemblies()
                .Where(a => IsAnyMatch(a.GetName().Name,filter));
        } 

        private IEnumerable<Assembly> GetNonReferencedAssemblies(IEnumerable<string> matches)
        {
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bin");
            return Directory.GetFiles(path, "*.dll")
                .Concat(Directory.GetFiles(path, "*.exe"))
                .Where(file => IsAnyMatch(file,matches))
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
