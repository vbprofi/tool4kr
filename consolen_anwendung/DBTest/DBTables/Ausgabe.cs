using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading.Tasks;
using DBTest.util;

namespace DBTest
{
    public class Ausgabe : DBRecord
    {
        public int id { get; set; }

        public int ausgabe { get; set; }

        public decimal preis { get; set; }

        public int datum { get; set; }

        public override string ToString()
        {
            return id + " " + ausgabe + " " + preis + "EURO " + Utils.TimeStampToDateTime(datum);
        }

    }//end class
} // end namespace
