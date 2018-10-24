#region Überschrift
/***
 * Diese Klasse wurde manuell erstellt.
 * Die Klasse enthält die Datasets, welches einer Datenbank entspricht (ein Dataset entspricht einer Datenbanktabelle).
 * Das Entity Framework (EF6) erstellt selbständig die entsprechende Datenbank, sollte sie nicht vorhanden sein, und Datenbanktabellen automatisch.
 * Dem Konstruktor muss eine Zeichenfolge (Connectionstring) übergeben werden, um eiine Datenbank verwenden oder erstellen zu können.
 ****/
#endregion Überschrift
#region Using
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using SQLite.CodeFirst;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.ComponentModel.DataAnnotations;
#endregion Using

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


        public DatabaseContext(DbConnection connection)
    : base(connection, contextOwnsConnection: true)
        {
        }

        public DatabaseContext(string connectionString)
        : base(connectionString)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            var initializer = new SqliteCreateDatabaseIfNotExists<DatabaseContext>(modelBuilder);
            Database.SetInitializer(initializer);
            //Voreinstellung
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);
        }

    }//end class
}//end namespace
