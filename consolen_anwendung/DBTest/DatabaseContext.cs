using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;
using System.Data.SQLite.EF6;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.ComponentModel.DataAnnotations;

namespace DBTest
{
    class DatabaseContext : DbContext
    {
        private static String dateiname = Program.assemblyDirectory + @"\test.db";

                        public DatabaseContext() : base(new SQLiteConnection()
    {
                                    ConnectionString = new SQLiteConnectionStringBuilder() { DataSource = dateiname, ForeignKeys = true }.ConnectionString
    }, true)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<kunden> kunden { get; set; }
        public DbSet<Bemerkung> Bemerkung { get; set; }
        public DbSet<Status> Status { get; set; }
        public DbSet <Ausgabe> Ausgabe { get; set; }
        public DbSet <Rechnung> Rechnung { get; set; }
        public DbSet <Rechnungsposten> Rechnungsposten { get; set; }
        public DbSet <Abo> Abo { get; set; }
    }//end class
}//end namespace
