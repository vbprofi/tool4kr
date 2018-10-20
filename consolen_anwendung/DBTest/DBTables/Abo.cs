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
    [Table(Name = "abo")]
    public class Abo : DBRecord
    {
        [Column(Name = "id", IsDbGenerated = true, IsPrimaryKey = true, DbType = "INTEGER")]
        [Key]
        public int id { get; set; }

        [Column(Name = "ausgabe_von", DbType = "integer")]
        public int ausgabe_von { get; set; }
        
        [Column(Name = "ausgabe_bis", DbType = "integer")]
        public int ausgabe_bis { get; set; }
        
        [Column(Name = "bezahlt_am", DbType = "integer")]
        public int bezahlt_am { get; set; }

        [Column(Name = "bezahlt_von", DbType = "integer")]
        public int bezahlt_von { get; set; }

        [Column(Name = "bezahlt_bis", DbType = "integer")]
        public int bezahlt_bis { get; set; }

        [Column(Name = "bemerkung_id", DbType = "integer")]
        public int bemerkung_id { get; set; }
        public Bemerkung bemerkung { get; set; }
        
       	public override string ToString()
       	{
       		return id + " " + ausgabe_bis + " " + ausgabe_von + " " + Utils.TimeStampToDateTime(bezahlt_am) + " " + bemerkung_id;
       	}

    }//end class
} // end namespace
