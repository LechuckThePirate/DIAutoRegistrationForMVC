using System;

namespace DependencyInjection.ServiceLibrary.Classes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface)]
    public class DependencyInjectionRegisterAttribute : Attribute
    {

        private readonly RegistrationTypeEnum _registrationType;
        public RegistrationTypeEnum RegistrationType { get { return _registrationType; } }

        public DependencyInjectionRegisterAttribute(RegistrationTypeEnum registrationType)
        {
            _registrationType = registrationType;
        }

        public DependencyInjectionRegisterAttribute()
        {
            _registrationType = RegistrationTypeEnum.Default;
        }

    }
}