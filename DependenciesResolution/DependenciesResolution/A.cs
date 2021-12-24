using System;
using System.Collections.Generic;
using System.Text;

namespace DependenciesResolution
{
    public class A : IA
    {
        public A(IB B) 
        {
            Console.WriteLine("class A");
        }

    }
}
