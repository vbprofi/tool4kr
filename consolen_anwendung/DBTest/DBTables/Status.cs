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
    public class Status : DBRecord
    {
        public int id { get; set; }

        public int eintritt_am { get; set; }

        public int austritt_am { get; set; }

        public int flag { get; set; }

        public int kunden_id { get; set; }
        public Kunden kunden { get; set; }

        public override string ToString()
        {
            return id + " " + kunden_id + " " + flag + " " + Utils.TimeStampToDateTime(eintritt_am) + " " + Utils.TimeStampToDateTime(austritt_am);
        }
    }//end class
} // end namespace
