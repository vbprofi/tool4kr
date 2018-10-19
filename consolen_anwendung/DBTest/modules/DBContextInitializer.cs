/****
 * Klasse zum erzeugen der Datenbankstruktur, weil Entity Framework 6 das wohl nicht von selbst kann
 * Quellen:
 * https://stackoverflow.com/questions/41977047/sqlite-with-entity-framework-6-no-such-table
 * https://stackoverflow.com/questions/42355481/auto-create-database-in-entity-framework-core
 * https://www.codeguru.com/csharp/article.php/c19999/Understanding-Database-Initializers-in-Entity-Framework-Code-First.htm
 *****/

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
    class DBContextInitializer : CreateDatabaseIfNotExists<DatabaseContext>
    {

        public DBContextInitializer()
        {
            createTBKunde();
            createTBAusgabe();
            createTBRechnung();
            createTBBemerkung();
            createTBAbo();
            createTBStatus();
            createTBRechnungsposten();
        }

        protected override void Seed(DatabaseContext context)
        {
            context.SaveChanges();
        }

        /***
             * Tabelle Kunden
             ****/
        private void createTBKunde()
        {
            DatabaseContext context = new DatabaseContext();

            context.Database.ExecuteSqlCommand("CREATE TABLE IF NOT EXISTS kunden(" +
                   "id integer NOT NULL PRIMARY KEY AUTOINCREMENT," +
  "firma text NOT NULL," +
  "vorname text NOT NULL," +
  "nachname text NOT NULL," +
  "straße text NOT NULL," +
  "hausnr text NOT NULL," +
  "plz integer NOT NULL," +
  "postfach text NOT NULL," +
  "ort text NOT NULL," +
  "land text NOT NULL," +
  "telefon text NOT NULL," +
  "fax text NOT NULL," +
  "email text NOT NULL," +
  "active integer NOT NULL," +
  "bemerkung_id integer NOT NULL," +
  "erstellt_am integer NOT NULL," +
  "geändert_am integer NOT NULL," +
  "status_id integer NOT NULL" +
                ")");

            context.SaveChanges();
        }

        /***
             * Tabelle ausgabe
             ****/
        private void createTBAusgabe()
        {
            DatabaseContext context = new DatabaseContext();
            context.Database.ExecuteSqlCommand("CREATE TABLE IF NOT EXISTS ausgabe(" +
                  "id integer NOT NULL PRIMARY KEY AUTOINCREMENT," +
  "ausgabe integer NOT NULL," +
  "preis numeric NOT NULL," +
  "datum integer NOT NULL" +
                ")");

            context.SaveChanges();
        }

        /***
 * Tabelle rechnung
 ****/
        private void createTBRechnung()
        {
            DatabaseContext context = new DatabaseContext();
            context.Database.ExecuteSqlCommand("CREATE TABLE IF NOT EXISTS rechnung(" +
                  "id integer NOT NULL PRIMARY KEY AUTOINCREMENT," +
                    "kunden_id integer NOT NULL," +
  "firma text NOT NULL," +
  "vorname text NOT NULL," +
  "nachname text NOT NULL," +
  "straße text NOT NULL," +
  "hausnr text NOT NULL," +
  "plz integer NOT NULL," +
  "postfach text NOT NULL," +
  "ort text NOT NULL," +
  "land text NOT NULL," +
  "telefon text NOT NULL," +
  "fax text NOT NULL," +
  "email text NOT NULL," +
  "bemerkung_id integer NOT NULL," +
  "erstellt_am integer NOT NULL," +
  "gesendet_am integer NOT NULL" +
            ")");

            context.SaveChanges();
        }

        /***
         * Tabelle bemerkung
         ****/
        private void createTBBemerkung()
        {
            DatabaseContext context = new DatabaseContext();
            context.Database.ExecuteSqlCommand("CREATE TABLE IF NOT EXISTS bemerkung(" +
                  "id integer NOT NULL PRIMARY KEY AUTOINCREMENT," +
                    "text text NOT NULL," +
  "datum integer NOT NULL," +
  "kunden_id integer NOT NULL," +
  "FOREIGN KEY (kunden_id) REFERENCES kunden (id)" +
                  ")");

            context.SaveChanges();
        }

        /***
* Tabelle abo
****/
        private void createTBAbo()
        {
            DatabaseContext context = new DatabaseContext();
            context.Database.ExecuteSqlCommand("CREATE TABLE IF NOT EXISTS abo(" +
                  "id integer NOT NULL PRIMARY KEY AUTOINCREMENT," +
                    "ausgabe_von integer NOT NULL," +
  "ausgabe_bis integer NOT NULL," +
  "bezahlt_am integer NOT NULL," +
  "bezahlt_von integer NOT NULL," +
  "bezahlt_bis integer NOT NULL," +
  "bemerkung_id integer NOT NULL," +
"  FOREIGN KEY (bemerkung_id) REFERENCES bemerkung (id)" +
                  ")");

            context.SaveChanges();
        }

        /***
             * Tabelle status
             ****/
        private void createTBStatus()
        {
            DatabaseContext context = new DatabaseContext();
            context.Database.ExecuteSqlCommand("CREATE TABLE IF NOT EXISTS status(" +
"id integer NOT NULL PRIMARY KEY AUTOINCREMENT," +
"eintritt_am  integer NOT NULL," +
"austritt_am integer NOT NULL," +
"flag integer NOT NULL," +
"kunden_id integer NOT NULL," +
"FOREIGN KEY (kunden_id) REFERENCES kunden (id)" +
")");

            context.SaveChanges();
        }

        /***
 * Tabelle rechnungsposten
 ****/
        private void createTBRechnungsposten()
        {
            DatabaseContext context = new DatabaseContext();
            context.Database.ExecuteSqlCommand("CREATE TABLE IF NOT EXISTS rechnungsposten(" +
                  "id integer NOT NULL PRIMARY KEY AUTOINCREMENT," +
                    "kunden_id integer NOT NULL," +
  "rechnung_id integer NOT NULL," +
  "anzahl integer NOT NULL," +
  "abo_id integer NOT NULL," +
  "kontonr integer NOT NULL," +
  "blz integer NOT NULL," +
  "iban text NOT NULL," +
  "institut text NOT NULL," +
  "kontoinhaber text NOT NULL," +
  "erstellt_am integer NOT NULL," +
  "bemerkung_id integer NOT NULL," +
  "FOREIGN KEY (abo_id) REFERENCES abo (id) ON DELETE CASCADE ON UPDATE NO ACTION," +
  "FOREIGN KEY (rechnung_id) REFERENCES rechnung (id) ON DELETE CASCADE ON UPDATE NO ACTION," +
  "FOREIGN KEY (kunden_id) REFERENCES kunden (id) ON DELETE CASCADE ON UPDATE NO ACTION" +
                  ")");

            context.SaveChanges();
        }
    }//end class
}//end namespace
