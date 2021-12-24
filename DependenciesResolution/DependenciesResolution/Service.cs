using System;
using System.Collections.Generic;
using System.Text;

namespace DependenciesResolution
{
    class Service
    {
        public Type ServiceType { get; }

        public Type ImplementationType { get; }

        public object Implementation { get; set; }

        public ServiceLifeTime LifeTime { get; }


        public Service(Type ServiceType, Type ImplementationType, ServiceLifeTime LifeTime)
        {
            this.ServiceType = ServiceType;
            this.LifeTime = LifeTime;
            this.ImplementationType = ImplementationType;
        }
    }
    public enum ServiceLifeTime  
    { 
        Transient, 
        Singleton 
    }
 
}
