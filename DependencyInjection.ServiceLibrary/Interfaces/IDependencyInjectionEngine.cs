using System;
using DependencyInjection.ServiceLibrary.Classes;

namespace DependencyInjection.ServiceLibrary.Interfaces
{
    public interface IDependencyInjectionEngine
    {
        void Register(Type TClass, Type @interface, RegistrationTypeEnum registrationType);
        void Register(Type TClass, RegistrationTypeEnum registrationType);
        void BuildDependencies();
    }
}
