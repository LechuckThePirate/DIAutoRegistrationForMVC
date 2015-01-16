using System;
using DependencyInjection.ServiceLibrary.Classes;
using DITest.Contracts.Interfaces;

namespace DITest.NonReferenced.Services
{
    [DependencyInjectionRegister]
    public class MyService : IExternalService
    {
        public void TerrificMethodOne()
        {
            Console.WriteLine("Boo!!!");
        }

        public string SayHello()
        {
            return "Hi there!";
        }
    }
}
