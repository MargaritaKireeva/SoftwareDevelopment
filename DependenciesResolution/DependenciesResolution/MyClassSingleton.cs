using System;
using System.Collections.Generic;
using System.Text;

namespace DependenciesResolution
{
    public class MyClassSingleton
    {
        static int count = 0;
        public MyClassSingleton()
        {
            count++;
            Console.WriteLine("MyClass {0}", count);
        }
    }
}
