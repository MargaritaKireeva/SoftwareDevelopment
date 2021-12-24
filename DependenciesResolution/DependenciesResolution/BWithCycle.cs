using System;
using System.Collections.Generic;
using System.Text;

namespace DependenciesResolution
{
    public class BWithCycle : IA
    {
        public BWithCycle(IA A)
        {
            Console.WriteLine("class BWithCycle");
        }
    }
}
