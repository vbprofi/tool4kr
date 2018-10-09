//Diese Klasse wurde zum Datenbank testen genutzt und wird aktuell nicht mehr gebraucht
/************
 * Dieser Part im Kommentar wird in der Class Programm eingefügt
            //Datenbankobjekt erzeugen
            database db = new database(assemblyDirectory + @"\test.db");
            //Datei zur Datenbank anzeigen
            String dbfile = Path.GetFileName(db.getDBFile());
            String dbpath = db.getDBFile();
            Console.WriteLine("Ausgewählte Datenbank: " + dbfile);
            Console.WriteLine(dbpath);

            //Tabellen
            //Kunde
            db.setTBKunde();
            db.getTBKunde();
            ******************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;


namespace DBTest
{
    public class old_database_test
    {

        //datei der datenbank
        private static String dateiname = "";

        //datenbankverbindung
        private SQLiteConnection sqlite_conn;

        public old_database_test()
        {
            Console.Write("Keine Datenbank ausgewählt.");
        }
        public old_database_test(String dbname)
        {
            //dateiname der Datenbank setzen
            dateiname = dbname;
            //herstellen der Berbindung
            sqlite_conn = CreateConnection();
        }

        //zeigen welche Datenbank geladen wird
        public string getDBFile()
        {
            if (dateiname.Length <= 0)
            {
                Console.WriteLine("Keine Datei ausgewählt.");
                return "";
            }
            else
                return dateiname;
        }

        //funktion zum herrstellen der verbindung
        private static SQLiteConnection CreateConnection()
        {
            SQLiteConnection sqlite_conn;
            // Create a new database connection:
            sqlite_conn = new SQLiteConnection("Data Source=" + dateiname + "; Version = 3; New = false; Compress = True; ");
            // Open the connection:
            try
            {
                sqlite_conn.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return sqlite_conn;
        }

        //funktion zum füllen der Datenbank
        private static void InsertData(String query, SQLiteConnection conn)
        {
            SQLiteCommand sqlite_cmd;
            sqlite_cmd = conn.CreateCommand();
            sqlite_cmd.CommandText = query;
            sqlite_cmd.ExecuteNonQuery();
        }

        //funktion zum ausgeben
        public static void ReadData(String query, SQLiteConnection conn)
        {
            SQLiteDataReader sqlite_datareader;
            SQLiteCommand sqlite_cmd;
            sqlite_cmd = conn.CreateCommand();
            sqlite_cmd.CommandText = query;

            sqlite_datareader = sqlite_cmd.ExecuteReader();
            while (sqlite_datareader.Read())
            {
                string myreader = sqlite_datareader.GetString(0);
                Console.WriteLine(myreader);
            }
            conn.Close();
        }

        /*
         * Funktionen zur Datenbanktabellen füllen
         * 
         */
        public void setTBKunde()
        {
            //InsertData("insert into kunden (vorname, nachname) values(\"hans\", \"peter\")", sqlite_conn);
            Console.WriteLine("Kunde geschrieben.");
        }

        /*
         * Inhalte der Datenbanktabellen auslesen
         * 
         */

        public void getTBKunde()
        {
            //ReadData("select * from kunde", sqlite_conn);
        }

    } //end class
}//end namespace
