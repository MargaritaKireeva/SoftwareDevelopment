using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Ex1

{
   
    class Program

    {
        static List<FIO> Input() 
        {
            using (StreamReader fileIn = new StreamReader("input.txt"))
            {
                List<FIO> list = new List<FIO>();
                string line;
                while ((line = fileIn.ReadLine()) != null)
                {
                    string[] text = line.Split(' ');
                    list.Add(new FIO(text[0], text[1], text[2]));
                }
                return list;
            }
        }
        static void Print(List<FIO> list) 
        {
            using (StreamWriter fileOut = new StreamWriter("output.txt"))
            {
                foreach (FIO item in list)
                {
                    fileOut.WriteLine("{0} {1} {2}", item.f, item.i, item.o);
                }
            }
        }
            static void Main(string[] args)
        {
            List<FIO> list = Input();
            list.Sort();
            Print(list);

        }
    }
}
