using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading.Tasks;
using DBTest.util;

namespace DBTest
{
        public class Rechnungsposten : DBRecord
    {
                public int id { get; set; }

                public int kunden_id { get; set; }
        public Kunden kunden { get; set; }

                public int rechnung_id { get; set; }
        public Rechnung rechnung { get; set; }

                public int anzahl { get; set; }

                public int abo_id { get; set; }
        public Abo abo { get; set; }

                public int kontonr { get; set; }

                public int blz { get; set; }

                public String iban { get; set; }

                public string institut { get; set; }

                public string kontoinhaber { get; set; }

                public int erstellt_am { get; set; }

                public int bemerkung_id { get; set; }
        public Bemerkung bemerkung { get; set; }
                        
        public override string ToString()
       	{
        	return id + " " + abo_id + " " + anzahl + " " + Utils.TimeStampToDateTime(erstellt_am) + " " + bemerkung_id + " " + kunden_id + " " + kontoinhaber + " " + institut + " " + kontonr;
       	}
    }//end class
} // end namespace
