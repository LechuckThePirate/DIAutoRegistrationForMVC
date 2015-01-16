using System;
using System.Collections.Generic;

namespace DependencyInjectionAutoRegister.Entities
{
    public class DependencyInjectionSetting
    {
        public Func<string,bool> ReferencedAssemblyFilter { get; set; } 
        public Func<string,bool> NonReferencedAssemblyFilter { get; set; }
        public List<Type> ClassesToRegister { get; set; }
    }
}
