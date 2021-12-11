using System;
using System.Collections.Generic;

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
            dict.Add("key4", "value4");
            dict.Add("key5", "value5");
            dict.Add("key6", "value6");
            foreach (var item in dict)
                Console.WriteLine("{0} {1}", item.Key, item.Value);
            Console.WriteLine("count = {0}", dict.Count);
            dict.Remove("key2");
            Console.WriteLine("After Remove key2:");
            foreach (string key in dict.Keys)
                Console.WriteLine("{0} {1}", key, dict[key]);
            Console.WriteLine("count = {0}", dict.Count);
            dict["key1"] = "value4";
            Console.WriteLine("After dict[key1] = value4");
            foreach (string key in dict.Keys)
                Console.WriteLine("{0} {1}", key, dict[key]);
            Console.WriteLine("Contains key2? {0}", dict.ContainsKey("key2"));
            dict.TryGetValue("key1", out string value);
            Console.WriteLine("TryGetValue key1: {0}", value);
            KeyValuePair<string, string>[] array = new KeyValuePair<string, string>[7];
            dict.CopyTo(array, 1);
            KeyValuePair<string, string> first = new KeyValuePair<string, string>("notcopy1", "notcopy1");
            array[0] = first;
            Console.WriteLine("CopyTo array:");
            foreach (var item in array)
                Console.WriteLine("{0} {1}", item.Key, item.Value);

        }
    }
}
