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
using DBTest.util;

namespace DBTest
{
    [Table(Name = "ausgabe")]
    public class Ausgabe : DBRecord
    {
        [Column(Name = "id", IsDbGenerated = true, IsPrimaryKey = true, DbType = "INTEGER")]
        [Key]
        public int id { get; set; }

        [Column(Name = "ausgabe", DbType = "integer")]
        public int ausgabe { get; set; }

        [Column(Name = "preis", DbType = "numeric")]
        public decimal preis { get; set; }

        [Column(Name = "datum", DbType = "integer")]
        public int datum { get; set; }
        
        public override string ToString()
       	{
       		return id + " " + ausgabe + " " + preis + "EURO " + Utils.TimeStampToDateTime(datum);
       	}

    }//end class
} // end namespace
