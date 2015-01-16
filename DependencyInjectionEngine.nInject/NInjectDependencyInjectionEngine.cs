using System;
using System.Web.Mvc;
using DependencyInjection.ServiceLibrary.Classes;
using DependencyInjection.ServiceLibrary.Interfaces;
using Ninject;
using Ninject.Web.Mvc;

namespace DependencyInjectionEngine.nInject
{
    public class NInjectDependencyInjectionEngine : IDependencyInjectionEngine
    {
        private IKernel _kernel;

        public NInjectDependencyInjectionEngine()
        {
            _kernel = new StandardKernel();
        }
        
        public void Register(Type TClass, Type @interface, RegistrationTypeEnum registrationType)
        {
            var binder = _kernel.Bind(@interface).To(TClass);
            if (registrationType == RegistrationTypeEnum.Singleton)
                binder.InSingletonScope();
        }

        public void Register(Type TClass, RegistrationTypeEnum registrationType)
        {
            var binder = _kernel.Bind(TClass).ToSelf();
            if (registrationType == RegistrationTypeEnum.Singleton)
                binder.InSingletonScope();
        }

        public void BuildDependencies()
        {
            DependencyResolver.SetResolver(new NinjectDependencyResolver(_kernel));
        }

    }
}