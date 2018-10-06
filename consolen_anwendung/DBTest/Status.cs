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
    [Table(Name = "status")]
    public class Status
    {

        [Column(Name = "id", IsDbGenerated = true, IsPrimaryKey = true, DbType = "INTEGER")]
        [Key]
        public int id { get; set; }

        [Column(Name = "eintritt_am", DbType = "integer")]
        public int eintritt_am { get; set; }

        [Column(Name = "austritt_am", DbType = "integer")]
        public int austritt_am { get; set; }

        [Column(Name = "flag", DbType = "integer")]
        public int flag { get; set; }

        [Column(Name = "kunden_id", DbType = "integer")]
        public int kunden_id { get; set; }
    }//end class
} // end namespace
