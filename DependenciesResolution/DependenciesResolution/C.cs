using System;
using System.Collections.Generic;
using System.Text;

namespace DependenciesResolution
{
    public class C
    {
        public C(IA a)
        {
            Console.WriteLine("class C");
        }
    }
}
