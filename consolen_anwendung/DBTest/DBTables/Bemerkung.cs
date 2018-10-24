#region Using
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading.Tasks;
using DBTest.util;
#endregion Using

namespace DBTest
{
    public class Bemerkung : DBRecord
    {
        public int id { get; set; }

        public string text { get; set; }

        public int datum { get; set; }

        public int kunden_id { get; set; }
        public Kunden kunden { get; set; }

        public override string ToString()
        {
            return id + " " + kunden_id + "#" + " " + text + " " + Utils.TimeStampToDateTime(datum);
        }
    }//end class
} // end namespace
