using System;

namespace DependenciesResolution
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Первый пример:");
            DependenceInjection.AddTransient<IA, A>();
            DependenceInjection.AddTransient<IB, B>();
            DependenceInjection.Get<IA>();
            Console.WriteLine("Пример Singleton:");
            DependenceInjection.AddSingleton<MyClassSingleton, MyClassSingleton>();
            DependenceInjection.Get<MyClassSingleton>();
            DependenceInjection.Get<MyClassSingleton>();
            Console.WriteLine("Пример Transient:");
            DependenceInjection.AddTransient<MyClassTransient, MyClassTransient>();
            DependenceInjection.Get<MyClassTransient>();
            DependenceInjection.Get<MyClassTransient>();
            Console.WriteLine("Второй пример(выброс исключения из-за цикла):");
             //DependenceInjection.AddTransient<IA, A>(); - уже внедрено
             DependenceInjection.AddTransient<IB, BWithCycle>();
             DependenceInjection.Get<IA>();
        }
    }
}
