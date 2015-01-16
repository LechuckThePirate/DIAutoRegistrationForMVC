using System;
using System.Web.Mvc;
using Autofac;
using Autofac.Builder;
using Autofac.Integration.Mvc;
using DependencyInjection.ServiceLibrary.Classes;
using DependencyInjection.ServiceLibrary.Interfaces;

namespace DependencyInjectionEngine.Autofac
{
    public class AutofacDependencyInjectionEngine : IDependencyInjectionEngine
    {

        private ContainerBuilder _containerBuilder;

        public void BuildDependencies()
        {
            if (null == _containerBuilder)
                throw new Exception("AutofacDependencyInjectionEngine: No container has been initialized yet!");

            var container = _containerBuilder.Build();

            var resolver = new AutofacDependencyResolver(container);
            DependencyResolver.SetResolver(resolver);
        }

        public void Register(Type TClass, Type @interface, RegistrationTypeEnum registrationType = RegistrationTypeEnum.Default)
        {
            RegisterAsType(_containerBuilder.RegisterType(TClass).As(@interface), registrationType);
        }

        public void Register(Type TClass, RegistrationTypeEnum registrationType)
        {
            RegisterAsType(_containerBuilder.RegisterType(TClass).AsSelf(), registrationType);
        }

        public void RegisterAsType(IRegistrationBuilder<object, ConcreteReflectionActivatorData, SingleRegistrationStyle> registrationBuilder,
            RegistrationTypeEnum registrationType)
        {
            switch (registrationType)
            {
                case RegistrationTypeEnum.Default:
                    registrationBuilder.InstancePerDependency();
                    break;
                case RegistrationTypeEnum.Singleton:
                    registrationBuilder.SingleInstance();
                    break;
            }
        }

        public AutofacDependencyInjectionEngine()
        {
            _containerBuilder = new ContainerBuilder();
        }
    }
}