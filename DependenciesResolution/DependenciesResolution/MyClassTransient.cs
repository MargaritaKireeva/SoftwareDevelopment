using System;
using System.Collections.Generic;
using System.Text;

namespace DependenciesResolution
{
    
    public class MyClassTransient
    {
        static int count = 0;
        public MyClassTransient()
        {
            count++;
            Console.WriteLine("MyClass {0}", count);
        }
    }
}
