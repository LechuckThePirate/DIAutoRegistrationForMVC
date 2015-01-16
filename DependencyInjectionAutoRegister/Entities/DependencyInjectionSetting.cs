using System;
using System.Collections.Generic;

namespace DependencyInjectionAutoRegister.Entities
{
    public class DependencyInjectionSetting
    {
        public List<string> AssemblyFilter { get; set; } 
        public List<string> ExternalAssemblyFilter { get; set; }
        public List<Type> ClassesForcedToRegister { get; set; }
    }
}
