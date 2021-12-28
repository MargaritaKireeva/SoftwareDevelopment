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
        static List<Type> dependenciesForIsCycle = new List<Type>();
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
            dependenciesForIsCycle.Add(T);
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
                dependenciesForIsCycle.Clear();
                return serviceGet.Implementation;
            }
            var actualType = serviceGet.ImplementationType;
            var constructor = actualType.GetConstructors().First();
            List<object> parameters = new List<object>();
            foreach (var x in constructor.GetParameters())
            {
                if (dependenciesForIsCycle.Contains(x.ParameterType))
                    throw new Exception("Cyclical dependence found");
                parameters.Add(Get(x.ParameterType));
            }
            var parametersArray = parameters.ToArray();
            var implementation = Activator.CreateInstance(actualType, parametersArray);
            if (serviceGet.LifeTime == ServiceLifeTime.Singleton)
            {
                serviceGet.Implementation = implementation;
            }
            dependenciesForIsCycle.Clear();
            return implementation;
        }
      
        public static T Get<T>()
        {
            return (T)Get(typeof(T));
        }
        /*  static bool IsCycle(Type parameterType)
        {
            if (dependenciesForIsCycle.Contains(parameterType))
                return true;
            else
            {
                dependenciesForIsCycle.Add(parameterType);
                return false;
            }
            
        }*/
        /*  static bool IsCycle(Type serviceType, Type parameterType)
          {
              Service service_ = null;
              bool serviceFound = false;
              foreach (Service service in services)
              {
                  if (service.ServiceType.Equals(parameterType))
                  {
                      service_ = service;
                      serviceFound = true;
                  }
              }
              if (!serviceFound)
                  throw new Exception("Dependence for the constructor parameter was not injected");
              Type actualParameterType = service_.ImplementationType;
              var constructorParameterType = actualParameterType.GetConstructors().First();
              foreach(var x in constructorParameterType.GetParameters())
              {
                  if (serviceType == x.ParameterType)
                      return true;

                  foreach (var y in x.ParameterType.GetConstructors().First().GetParameters())
                      IsCycle(x.ParameterType, y.ParameterType);
              }


              return false;
          }*/

    }
}
