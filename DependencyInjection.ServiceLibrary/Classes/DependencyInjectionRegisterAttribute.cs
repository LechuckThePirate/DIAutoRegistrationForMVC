using System;

namespace DependencyInjection.ServiceLibrary.Classes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface)]
    public class DependencyInjectionRegisterAttribute : Attribute
    {
        public RegistrationTypeEnum RegistrationType { get; set; }

    }
}