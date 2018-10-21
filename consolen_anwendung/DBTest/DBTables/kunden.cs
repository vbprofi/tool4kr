using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading.Tasks;
using DBTest.util;

namespace DBTest
{
        public class Kunden : DBRecord
    {
                        public int id { get; set; }

                public string vorname { get; set; }

                public String nachname { get; set; }

                public string firma { get; set; }

                public string straße { get; set; }

                public String hausnr { get; set; }

                public int plz { get; set; }

                public string postfach { get; set; }

                public string ort { get; set; }

                public string land { get; set; }

                public string telefon { get; set; }

                public string fax { get; set; }

                public string email { get; set; }

                public int active { get; set; }

                                        public int bemerkung_id { get; set; }
        //public Bemerkung bemerkung { get; set; }

                public int erstellt_am { get; set; }

                public int geändert_am { get; set; }

                public int status_id { get; set; }
                        
        public override string ToString()
       	{
       		return id + " " + firma + " " + vorname + " " + nachname + " " + straße + " " + hausnr + " " + plz + " " + postfach + " " + land + " " + telefon + " " + fax + " " + email + " " + bemerkung_id + " " + " " + Utils.TimeStampToDateTime(erstellt_am);
       	}
//public Status status { get; set; }
    }//end class
} // end namespace
