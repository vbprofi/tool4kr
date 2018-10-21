using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading.Tasks;
using DBTest.util;

namespace DBTest
{
        public class Abo : DBRecord
    {
                        public int id { get; set; }

                public int ausgabe_von { get; set; }
        
                public int ausgabe_bis { get; set; }
        
                public int bezahlt_am { get; set; }

                public int bezahlt_von { get; set; }

                public int bezahlt_bis { get; set; }

                public int bemerkung_id { get; set; }
        public Bemerkung bemerkung { get; set; }
        
       	public override string ToString()
       	{
       		return id + " " + ausgabe_bis + " " + ausgabe_von + " " + Utils.TimeStampToDateTime(bezahlt_am) + " " + bemerkung_id;
       	}

    }//end class
} // end namespace
