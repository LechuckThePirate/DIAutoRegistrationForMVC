using System.Management.Instrumentation;
using System.Web.Mvc;
using DependencyInjection.ServiceLibrary.Classes;
using DependencyInjection.ServiceLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using Unity.Mvc5;

namespace DependencyInjectionEngine.Unity
{
    public class UnityDependencyInjectionEngine : IDependencyInjectionEngine
    {

        private UnityContainer _container;

        public UnityDependencyInjectionEngine()
        {
            _container = new UnityContainer();
        }

        public void Register(Type TClass, Type @interface, RegistrationTypeEnum registrationType)
        {
            _container.RegisterType(@interface, TClass, GetLifetimeManager(registrationType));
        }

        public void Register(Type TClass, RegistrationTypeEnum registrationType)
        {
            _container.RegisterType(TClass, GetLifetimeManager(registrationType));
        }

        private LifetimeManager GetLifetimeManager(RegistrationTypeEnum registrationType)
        {
            switch (registrationType)
            {
                case RegistrationTypeEnum.Default:
                    return null;
                case RegistrationTypeEnum.Singleton:
                    return new ContainerControlledLifetimeManager();
            }
            return null;
        }

        public void BuildDependencies()
        {
            DependencyResolver.SetResolver(new UnityDependencyResolver(_container));
        }
    }
}
