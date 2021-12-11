using System;

namespace CustomDict
{
    class Program
    {
        static void Main(string[] args)
        {
            CustomDictionary<string, string> dict = new CustomDictionary<string, string>();
            dict.Add("key1", "value1");
            dict.Add("key2", "value2");
            dict.Add("key3", "value3");
            foreach (var item in dict)
                Console.WriteLine("{0} {1}", item.Key, item.Value);
            dict.Remove("key2");
            Console.WriteLine("After Remove key2:");
            foreach (string key in dict.Keys)
                Console.WriteLine("{0} {1}", key, dict[key]);
            dict["key1"] = "value4";
            Console.WriteLine("After dict[key1] = value4");
            foreach (string key in dict.Keys)
                Console.WriteLine("{0} {1}", key, dict[key]);
            Console.WriteLine("Contains key2? {0}", dict.ContainsKey("key2"));
            dict.TryGetValue("key1", out string value);
            Console.WriteLine("TryGetValue key1: {0}", value);

        }
    }
}
