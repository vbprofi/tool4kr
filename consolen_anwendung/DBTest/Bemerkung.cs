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
    [Table(Name = "bemerkung")]
    public class Bemerkung
    {

        [Column(Name = "id", IsDbGenerated = true, IsPrimaryKey = true, DbType = "INTEGER")]
        [Key]
        public int id { get; set; }

        [Column(Name = "text", DbType = "VARCHAR")]
        public string text { get; set; }

        [Column(Name = "datum", DbType = "integer")]
        public int datum { get; set; }
               
                [Column(Name = "kunden_id", DbType = "integer")]
                        public int kunden_id { get; set; }
        public kunden kunden { get; set; }
    }//end class
} // end namespace
