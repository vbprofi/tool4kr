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

            /*** Relation ***/
            modelBuilder.Entity<Rechnungsposten>()
.HasRequired(rp => rp.kunden)
.WithOptional();
            modelBuilder.Entity<Rechnungsposten>()
.HasRequired(rp => rp.rechnung)
.WithOptional();
            modelBuilder.Entity<Rechnungsposten>()
.HasRequired(rp => rp.abo)
.WithOptional();
            modelBuilder.Entity<Rechnungsposten>()
.HasRequired(rp => rp.bemerkung)
.WithOptional();

            modelBuilder.Entity<Abo>()
.HasRequired(ab => ab.bemerkung)
                        .WithOptional();
            modelBuilder.Entity<Status>()
.HasRequired(s => s.kunden)
.WithOptional();
            modelBuilder.Entity<Bemerkung>()
                               .HasRequired(b => b.kunden)
                .WithOptional();

            /****
             * wird nicht benötigt
modelBuilder.Entity<kunden>()
.HasRequired(k => k.bemerkung)
.WithOptional();
***********/
        }

        public DbSet<kunden> kunden { get; set; }
        public DbSet<Bemerkung> Bemerkung { get; set; }
        public DbSet<Status> Status { get; set; }
        public DbSet<Ausgabe> Ausgabe { get; set; }
        public DbSet<Rechnung> Rechnung { get; set; }
        public DbSet<Rechnungsposten> Rechnungsposten { get; set; }
        public DbSet<Abo> Abo { get; set; }
            }//end class
}//end namespace
