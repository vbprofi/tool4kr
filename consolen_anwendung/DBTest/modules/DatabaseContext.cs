﻿using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using SQLite.CodeFirst;

namespace DBTest
{
    public class DatabaseContext : DbContext
    {
        
        public DbSet<Kunden> kunden { get; set; }
        public DbSet<Bemerkung> bemerkung { get; set; }
        public DbSet<Status> status { get; set; }
        public DbSet<Ausgabe> ausgabe { get; set; }
        public DbSet<Rechnung> rechnung { get; set; }
        public DbSet<Rechnungsposten> rechnungsposten { get; set; }
        public DbSet<Abo> abo { get; set; }
        
        /*******
        public DatabaseContext() : base(new SQLiteConnection()
        {
            ConnectionString = new SQLiteConnectionStringBuilder() { DataSource = dateiname, ForeignKeys = true, JournalMode=SQLiteJournalModeEnum.Off }.ConnectionString
        }, true)
        {
                   }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
                        //Voreinstellung
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);

            // Relation ***
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

            /
             * wird nicht benötigt
modelBuilder.Entity<kunden>()
.HasRequired(k => k.bemerkung)
.WithOptional();
*********
        }
        ***/

                public DatabaseContext(DbConnection connection) 
            : base(connection, contextOwnsConnection: true)
        { }

        public DatabaseContext(string connectionString)           
        :   base(connectionString)
        {
            
        }

        
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {            
            var initializer = new SqliteCreateDatabaseIfNotExists<DatabaseContext>(modelBuilder);
            Database.SetInitializer(initializer);
        }


    }//end class
}//end namespace
