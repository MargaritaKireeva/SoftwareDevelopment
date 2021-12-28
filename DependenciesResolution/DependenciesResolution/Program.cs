using System;

namespace DependenciesResolution
{
    class Program
    {
        static void Main(string[] args)
        {
            
            Console.WriteLine("Пример Singleton:");
            DependenceInjection.AddSingleton<MyClassSingleton, MyClassSingleton>();
            DependenceInjection.Get<MyClassSingleton>();
            DependenceInjection.Get<MyClassSingleton>();
            Console.WriteLine("Пример Transient:");
            DependenceInjection.AddTransient<MyClassTransient, MyClassTransient>();
            DependenceInjection.Get<MyClassTransient>();
            DependenceInjection.Get<MyClassTransient>();
            Console.WriteLine("Первый пример теперь тоже с циклом:");
            DependenceInjection.AddTransient<IA, A>();
            DependenceInjection.AddTransient<IB, B>();
            DependenceInjection.AddTransient<C, C>();
            DependenceInjection.Get<IA>();
            // DependenceInjection.AddTransient<IC, C>();
            // DependenceInjection.Get<IA>();
            Console.WriteLine("Второй пример(выброс исключения из-за цикла):");
            DependenceInjection.AddTransient<IA, A>();// - уже внедрено
            DependenceInjection.AddTransient<IB, BWithCycle>();
            DependenceInjection.Get<IA>();
        }
    }
}
