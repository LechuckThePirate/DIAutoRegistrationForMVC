using System;

namespace DependencyInjection.ServiceLibrary.Classes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface)]
    public class DependencyInjectionIgnoreAttribute : Attribute
    {
    }
}
