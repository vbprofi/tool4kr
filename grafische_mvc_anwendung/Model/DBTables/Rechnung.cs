#region Using
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading.Tasks;
using KRTool.Model.util;
#endregion Using

namespace KRTool.Model
{
    public class Rechnung : DBRecord
    {
        public int id { get; set; }

        public int kunden_id { get; set; }

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

        public int bemerkung_id { get; set; }

        public int erstellt_am { get; set; }

        public int gesendet_am { get; set; }

        public override string ToString()
        {
            return id + " " + kunden_id + " " + firma + " " + vorname + " " + nachname + " " + straße + " " + hausnr + " " + plz + " " + postfach + " " + land + " " + telefon + " " + fax + " " + email + " " + Utils.TimeStampToDateTime(erstellt_am);
        }
    }//end class
} // end namespace
