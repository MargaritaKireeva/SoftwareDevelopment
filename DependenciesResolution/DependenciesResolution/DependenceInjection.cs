using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using System.Text;



namespace DependenciesResolution
{
    public static class DependenceInjection
    {
        static List<Service> services = new List<Service>();
        public static void AddTransient<TService, TImplementation> ()
        {
            services.Add(new Service(typeof(TService), typeof(TImplementation), ServiceLifeTime.Transient));
        }
        public static void AddSingleton<TService, TImplementation>()
        {
            services.Add(new Service(typeof(TService), typeof(TImplementation), ServiceLifeTime.Singleton));
        }
        static object Get(Type T)
        {
            Service serviceGet = null;
            bool serviceIsFound = false;
            foreach (Service service in services)
            {
                if (service.ServiceType.Equals(T))
                {
                    serviceGet = service;
                    serviceIsFound = true;
                }
            }
            if (!serviceIsFound)
                throw new Exception("Service not found");
            if (serviceGet.Implementation != null)
            {
                return serviceGet.Implementation;
            }
            var actualType = serviceGet.ImplementationType;
            var constructor = actualType.GetConstructors().First();
            foreach (var x in constructor.GetParameters())
            {
                if (IsCycle(T, x.ParameterType))
                    throw new Exception("Cyclical dependence found");
                    
            }
            List<object> parameters = new List<object>();
            foreach (var x in constructor.GetParameters())
            {
                parameters.Add(Get(x.ParameterType));
            }
            var parametersArray = parameters.ToArray();
            var implementation = Activator.CreateInstance(actualType, parametersArray);
            if (serviceGet.LifeTime == ServiceLifeTime.Singleton)
            {
                serviceGet.Implementation = implementation;
            }
            return implementation;
        }
        static bool IsCycle(Type serviceType, Type parameterType)
        {
            Service service_ = null;
            foreach (Service service in services)
            {
                if (service.ServiceType.Equals(parameterType))
                {
                    service_ = service;
                }
            }
            Type actualParameterType = service_.ImplementationType;
            var constructorParameterType = actualParameterType.GetConstructors().First();
            foreach(var x in constructorParameterType.GetParameters())
            {
                if (serviceType == x.ParameterType)
                    return true;
            }
            return false;
        }
        public static T Get<T>()
        {
            return (T)Get(typeof(T));
        }
    }
}
