using System;
using System.Collections.Generic;
using System.Text;

namespace DependenciesResolution
{
    public class B : IB
    {
        public B() 
        {
            Console.WriteLine("class B");
        }
    }
}
