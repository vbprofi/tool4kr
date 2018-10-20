using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;
using System.Data.SQLite.EF6;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Linq.Mapping;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Threading.Tasks;

namespace DBTest
{
    [Table(Name = "rechnungsposten")]
    public class Rechnungsposten
    {
        [Column(Name = "id", IsDbGenerated = true, IsPrimaryKey = true, DbType = "INTEGER")]
        [Key]
        public int id { get; set; }

        [Column(Name = "kunden_id", DbType = "integer")]
        public int kunden_id { get; set; }
        public Kunden kunden { get; set; }

        [Column(Name = "rechnung_id", DbType = "integer")]
        public int rechnung_id { get; set; }
        public Rechnung rechnung { get; set; }

        [Column(Name = "anzahl", DbType = "integer")]
        public int anzahl { get; set; }

        [Column(Name = "abo_id", DbType = "integer")]
        public int abo_id { get; set; }
        public Abo abo { get; set; }

        [Column(Name = "kontonr", DbType = "integer")]
        public int kontonr { get; set; }

        [Column(Name = "blz", DbType = "integer")]
        public int blz { get; set; }

        [Column(Name = "iban", DbType = "text")]
        public String iban { get; set; }

        [Column(Name = "institut", DbType = "text")]
        public string institut { get; set; }

        [Column(Name = "kontoinhaber", DbType = "text")]
        public string kontoinhaber { get; set; }
        [Column(Name = "erstellt_am", DbType = "integer")]
        public int erstellt_am { get; set; }

        [Column(Name = "bemerkung_id", DbType = "integer")]
        public int bemerkung_id { get; set; }
        public Bemerkung bemerkung { get; set; }
    }//end class
} // end namespace
