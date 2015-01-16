using DependencyInjection.ServiceLibrary.Classes;

namespace DIRegistrationTest.Services
{
    [DependencyInjectionRegister(RegistrationTypeEnum.Singleton)]
    public class MyLocalService : ILocalService
    {
        public string GuessWhat()
        {
            return "I'm another service";
        }
    }
}