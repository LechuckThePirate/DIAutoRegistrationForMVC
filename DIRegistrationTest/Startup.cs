using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using DependencyInjectionAutoRegister;
using DependencyInjectionAutoRegister.Entities;
using DependencyInjectionEngine.Unity;
using DIRegistrationTest;
using Microsoft.Owin;
using Owin;
using DependencyInjection.ServiceLibrary.Interfaces;

[assembly: OwinStartup(typeof(Startup))]
namespace DIRegistrationTest
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

            // Change here to your preferred IoC framework
            //   available: UnityDependencyInjectionEngine, 
            //              AutofacDependencyInjectionEngine, 
            //              NInjectDependencyInjectionEngine
            ConfigureSimpleDependencyInjector(new UnityDependencyInjectionEngine());
        }

        private void ConfigureSimpleDependencyInjector(IDependencyInjectionEngine engine)
        {
            new DependencyInjectionService(engine).RegisterDependencies(new DependencyInjectionSetting()
            {
                AssemblyFilter = new List<string> { "DITest.", "DIRegistrationTest" }
            });
        }


        // Here is a custom settings option if you wanna do more tests...
        // To use this one, you should "de-reference" DITest.NonReferenced.Service, as it's assembly 
        // is loaded as External (the .dll should be copied manually to the /bin project's folder
        private void ConfigureCustomDependencyInjector(IDependencyInjectionEngine engine)
        {

            new DependencyInjectionService(engine)
                .RegisterDependencies(new DependencyInjectionSetting
                {
                    // Filters types to avoid scanning too much classes... you can set it null if you don't mind
                    AssemblyFilter = new List<string> { "DITest.", "DIRegistrationTest" },
                    // Assembly files (.dll or .exe) that aren't referenced and you want to include. Normally not needed
                    ExternalAssemblyFilter = new List<string> { "DITest.NonReferenced.Service"},
                    // Classes you want to include without having the DependencyIndectionRegisterAttribute
                    ClassesForcedToRegister = Assembly.GetExecutingAssembly().GetTypes()
                        .Where(t => typeof (Controller).IsAssignableFrom(t))
                        .ToList()
                });

        }

    }
}
