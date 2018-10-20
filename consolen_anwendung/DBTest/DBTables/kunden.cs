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
    [Table(Name = "kunden")]
    public class Kunden : DBRecord
    {
        [Column(Name = "id", IsDbGenerated = true, IsPrimaryKey = true, DbType = "INTEGER")]
        [Key]
        public int id { get; set; }

        [Column(Name = "vorname", DbType = "VARCHAR")]
        public string vorname { get; set; }

        [Column(Name = "nachname", DbType = "String")]
        public String nachname { get; set; }

        [Column(Name = "firma", DbType = "VARCHAR")]
        public string firma { get; set; }

        [Column(Name = "straße", DbType = "VARCHAR")]
        public string straße { get; set; }

        [Column(Name = "hausnr", DbType = "varchar")]
        public String hausnr { get; set; }

        [Column(Name = "plz", DbType = "integer")]
        public int plz { get; set; }

        [Column(Name = "postfach", DbType = "VARCHAR")]
        public string postfach { get; set; }

        [Column(Name = "ort", DbType = "VARCHAR")]
        public string ort { get; set; }

        [Column(Name = "land", DbType = "VARCHAR")]
        public string land { get; set; }

        [Column(Name = "telefon", DbType = "VARCHAR")]
        public string telefon { get; set; }

        [Column(Name = "fax", DbType = "VARCHAR")]
        public string fax { get; set; }

        [Column(Name = "email", DbType = "VARCHAR")]
        public string email { get; set; }

        [Column(Name = "active", DbType = "integer")]
        public int active { get; set; }

        [Column(Name = "bemerkung_id", DbType = "integer")]
                                public int bemerkung_id { get; set; }
        //public Bemerkung bemerkung { get; set; }

        [Column(Name = "erstellt_am", DbType = "integer")]
        public int erstellt_am { get; set; }

        [Column(Name = "geändert_am", DbType = "integer")]
        public int geändert_am { get; set; }

        [Column(Name = "status_id", DbType = "integer")]
        public int status_id { get; set; }
                        
        public override string ToString()
       	{
       		return id + " " + firma + " " + vorname + " " + nachname + " " + straße + " " + hausnr + " " + plz + " " + postfach + " " + land + " " + telefon + " " + fax + " " + email + " " + bemerkung_id + " " + " " + Utils.TimeStampToDateTime(erstellt_am);
       	}
//public Status status { get; set; }
    }//end class
} // end namespace
