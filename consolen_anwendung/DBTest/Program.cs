﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Diagnostics;
using System.Threading;
using System.Data.SQLite;
using System.Data.Entity;
using DBTest.modules;
using DBTest.util;
using System.Transactions;
using autogeneratedDB;

namespace DBTest
{
    class Program
    {
        public static String assemblyDirectory = Environment.CurrentDirectory.ToString(); //ermittelt den aktuellen pfad der Anwendung
        public static String dbdateiname = assemblyDirectory + @"\test.db"; //Dateiname der SQLite-Datenbank
        private static String dlldateiname = assemblyDirectory + @"\dlls.txt"; //Datei für schreiben der DLL-Infos

        static void Main(string[] args)
        {
            //Titel der Console
            getAssembly("title");
            //Signale ausgeben beim Starten
            Console.Beep(900, 100); Console.Beep(500, 100);

            //Inhalt der Console mit Infos füllen
            getAssembly();
            //DLL-Dateiinfos in Datei schreiben
            writeLoadDLL();
            KRDatabase db = new KRDatabase();
            
            Console.WriteLine("Datenbankversion: " + db.getVersionString());

            createSampleDataAllTables(db); //keine Änderung
            createSampleDataAllTables(db); //schneller geworden.
                                                //merke mit eingebautem Transactions keinen unterschied beim ersten Füllen. Beim zweiten gibts einen kleinen unterschied. kA, woran das liegt. (siehe funktion)
                                                //performance: https://codingsight.com/entity-framework-improving-performance-when-saving-data-to-database/

            printTableData(db);
            
            Console.WriteLine("--------------------------------------------------------");
            using (DBReader reader = db.getDBReader())
            {
	            Ausgabe letzteAusgabe = reader.getCurrentIssue();
	            decimal preisLetzteAusgabe = reader.getPriceOfIssue(letzteAusgabe.ausgabe);
  		        Console.WriteLine("Letzte Ausgabe: "+letzteAusgabe+" (Preis="+preisLetzteAusgabe+" EURO)");
            }
            
            //Beenden
            Console.Write("\nBitte return drücken, um die Anwendung zu beenden."); Console.ReadLine();
            //Signale ausgeben beim beenden
            Console.Beep(500, 200); Console.Beep(900, 200);
        }//end main function

        /**
         * Test-Methode: Fügt zu allen Tabellen einen Datensatz hinzu und misst die Schreibzeit.
         */
        private static void createSampleDataAllTables(KRDatabase db)
        {
            //Stopuhr zur Zeit Messung erzeugen
            CounterStopWatch watch = new CounterStopWatch();
            watch.ResetAndStart(); //Stopuhr zurücksetzen auf 0 und starten
        	using (DBWriter writer = db.getDBWriter())
            {
                Kunden kunde = DBRecordFactory.createKunden("Firma", "Vorname", "Nachname", "Straße", "HausNR", 12345, "Ort", "Postfach", "Land", "Telefon", "Fax", "EMail"); //Tabelle Kunden füllen
                writer.addRecord(kunde); //Inhalte zur Tabelle hinzufügen
                watch++;

                Ausgabe ag = DBRecordFactory.createAusgabe(199, Convert.ToDecimal("3,00")); //Tabelle ausgabe füllen
                writer.addRecord(ag); //Inhalte zur Tabelle hinzufügen
                watch++;

                Rechnung rn = DBRecordFactory.createRechnung("Firma", "Vorname", "Nachname", "Straße", "HausNR", 12345, "Ort", "Postfach", "Land", "Telefon", "Fax", "EMail", 0, 0); //Tabelle rechnung füllen
                writer.addRecord(rn); //Inhalte zur Tabelle hinzufügen
                watch++;

                Abo ab = DBRecordFactory.createAbo(1, 1, 1, 1, 1, 1); //Tabelle abo füllen
                writer.addRecord(ab); //{mit foreign key} Inhalte zur Tabelle hinzufügen
                watch++;

                Bemerkung bm = DBRecordFactory.createBemerkung("Txt", 1); //Tabelle bemerkung füllen
                                                       //context.Bemerkung.Include("kunden_id"); //Foreign-Key hinzufügen
                writer.addRecord(bm); //{mit foreign key} Inhalte zur Tabelle hinzufügen
                watch++;

                Rechnungsposten rp = DBRecordFactory.createRechnungsposten(1, 1, 6, 1, 555555, 888888, "IBAN", "Institut", "KontoInhaber", 1); //Tabelle rechnungsposten füllen
                writer.addRecord(rp); //{mit foreign key} Inhalte zur Tabelle hinzufügen
                watch++;

                Status state = DBRecordFactory.createStatus(1, 1, 1, 1); //Tabelle status füllen
                writer.addRecord(state); //{mit foreign key} Inhalte zur Tabelle hinzufügen
                watch++;
            }
            watch.Stop(); //Zeit anhalten
            Console.WriteLine("Schreibzeit: " + watch); //Ausgeben wie lange das Schreiben in die DB gedauert hat        	
        }

        /**
         * Test-Methode: Gibt alle Tabellen aus und misst die Lesezeit.
         */
        private static void printTableData(KRDatabase db)
        {
            //Stopuhr zur Zeit Messung erzeugen
            CounterStopWatch watch = new CounterStopWatch();
            watch.ResetAndStart(); //Stopuhr zurücksetzen auf 0 und starten
        	using (DBReader reader = db.getDBReader())
            {
                /*
                 * Ausgabe der DB-Inhalte in der console
                 */
                Console.WriteLine("\n=< Kunden >============================"); //Überschrift
                printTable(reader.getKunden()); //Tabelle ausgeben
                watch++;

                Console.WriteLine("=< Rechnung >============================"); //Überschrift
                printTable(reader.getRechnungen()); //Tabelle ausgeben
                watch++;

                Console.WriteLine("=< Bemerkung >============================"); //Überschrift
                printTable(reader.getBemerkungen()); //Tabelle ausgeben
                watch++;

                Console.WriteLine("=< Status >============================"); //Überschrift
                printTable(reader.getStatus()); //Tabelle ausgeben
                watch++;

                Console.WriteLine("=< Ausgabe >============================"); //Überschrift
                printTable(reader.getAusgaben()); //Tabelle ausgeben
                watch++;

                Console.WriteLine("=< Abo >============================"); //Überschrift
                printTable(reader.getAbos()); //Tabelle ausgeben
                watch++;

                Console.WriteLine("=< Rechnungsposten >============================"); //Überschrift
                printTable(reader.getRechnungsposten()); //Tabelle ausgeben
                watch++;
            }
            watch.Stop(); //Zeit anhalten
            Console.WriteLine("\nLesezeit: " + watch); //Ausgeben wie lange das Lesen aus der DB gedauert hat
        }

        private static void printTable(IEnumerable<DBRecord> data)
        {
            foreach (var item in data)
            {
                Console.WriteLine(item);
            }
        }

        private static void getAssembly(String info = "")
        {
            Assembly execAssembly = Assembly.GetCallingAssembly();
            AssemblyName name = execAssembly.GetName();

            if (info.Length <= 0 || info == "")
            {
                Console.WriteLine(string.Format("{0}{1} v{2:0} for .Net ({3}){0}",
                Environment.NewLine,
                name.Name,
                name.Version.ToString(),
                execAssembly.ImageRuntimeVersion
                ));
            }
            else
            if (info == "title") Console.Title = name.Name + " v" + name.Version.ToString();
        }

        private static String getBINinfo(string AppDirectory)
        {
            String wText = "";
            try
            {
                var versionInfo = FileVersionInfo.GetVersionInfo(AppDirectory);
                                
                wText += "Productname: " + versionInfo.ProductName + Environment.NewLine;
                wText += "ProductVersion: " + versionInfo.ProductVersion+ Environment.NewLine;
                wText += "FileVersion: " + versionInfo.FileVersion + Environment.NewLine;
                return wText;
            }
            catch { return wText; }
            return wText;
        }

        //geladene DLL-Infos in Textdatei schreiben
        private static void writeLoadDLL()
{
            String wText = "";
            try
                {
RWFile wFile = new RWFile();
wText += DateTime.Now + ": Programm gestartet." + Environment.NewLine + "Folgende DLL-Dateien geladen..."+ Environment.NewLine + "----------------------" + Environment.NewLine;
                //dateien info
                wText += getBINinfo(assemblyDirectory + @"\autogeneratedDB.dll") + Environment.NewLine;
           wText += getBINinfo(assemblyDirectory + @"\System.Data.SQLite.dll") + Environment.NewLine;
          wText +=  getBINinfo(assemblyDirectory + @"\EntityFramework.dll")+ Environment.NewLine;
                       wText += getBINinfo(assemblyDirectory + @"\System.Data.SQLite.EF6.dll") + Environment.NewLine;
                
wFile.WriteFile(dlldateiname, wText);
                } catch { }
}
    }//end class
} //end namespace
