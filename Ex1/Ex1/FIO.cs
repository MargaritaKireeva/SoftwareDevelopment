using System;
using System.Collections.Generic;
using System.Text;

namespace Ex1
{
    public class FIO : IComparable<FIO>
    {
        public string f { get; set; }
        public string i { get; set; }
        public string o { get; set; }
        public FIO(string f, string i, string o)
        {
            this.f = f;
            this.i = i;
            this.o = o;
        }
        public int CompareTo(FIO obj)
        {
            if (string.Compare(f, obj.f) == 1)
            {
                return 1;
            }
            else
            {
                if (f.Equals(obj.f))
                {
                    if (string.Compare(i, obj.i) == -1)
                    {
                        return 1;
                    }
                    else
                    {
                        if (Equals(i, obj.i))
                        {
                            return string.Compare(o, obj.o);
                        }
                        else return -1;
                    }
                }
                else
                {
                    return -1;
                }
            }
        }
    }
}
