using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace internetmarke
{
    class Program
    {
        static void Main(string[] args)
        {
            Briefmarke bm = new Briefmarke();
           var txt = bm.PostInternetmarke();
            Console.WriteLine(txt);
            Console.ReadLine();
        }
    }
}
