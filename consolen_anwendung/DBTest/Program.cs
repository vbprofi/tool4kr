﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Diagnostics;
using System.Threading;
using System.Data.SQLite;

namespace DBTest
{
    class Program
    {
        public static String assemblyDirectory = Environment.CurrentDirectory.ToString(); //ermittelt den aktuellen pfad der Anwendung

        static void Main(string[] args)
        {
            //Titel der Console
            getAssembly("title");
            //Signale ausgeben beim Starten
            Console.Beep(900, 100); Console.Beep(500, 100);

            //Inhalt der Console mit Infos füllen
            getAssembly();
            //dateien info
            getBINinfo(assemblyDirectory + @"\System.Data.SQLite.dll");
                        //Datenbankobjekt erzeugen
            DatabaseContext context = new DatabaseContext();
            Console.WriteLine("Datenbankversion: " + context.Database.Connection.ServerVersion);
            
            //Stopuhr zur Zeit Messung erzeugen
            Stopwatch watch = new Stopwatch();
            watch.Reset(); //zurücksetzen auf 0
            watch.Start(); //Stopuhr starten

            Kunden kunde = setKunden("Firma", "Vorname", "Nachname", "Straße", "HausNR", 12345, "Ort", "Postfach", "Land", "Telefon", "Fax", "EMail"); //Tabelle Kunden füllen
            //context.kunden.Include("b.bemerkung_id"); //Foreign-Key hinzufügen
            context.kunden.Add(kunde); //Inhalte zur Tabelle hinzufügen
            Ausgabe ag = setAusgabe(199, Convert.ToDecimal("3,00")); //Tabelle ausgabe füllen
            context.Ausgabe.Add(ag); //Inhalte zur Tabelle hinzufügen
            Rechnung rn = setRechnung("Firma", "Vorname", "Nachname", "Straße", "HausNR", 12345, "Ort", "Postfach", "Land", "Telefon", "Fax", "EMail", 0, 0); //Tabelle rechnung füllen
            context.Rechnung.Add(rn); //Inhalte zur Tabelle hinzufügen
            Abo ab = setAbo(1, 1, 1, 1, 1, 1); //Tabelle abo füllen
            context.Abo.Add(ab); //{mit foreign key} Inhalte zur Tabelle hinzufügen
            Bemerkung bm = setBemerkung("Txt", 1); //Tabelle bemerkung füllen
                                                   //context.Bemerkung.Include("kunden_id"); //Foreign-Key hinzufügen
            context.Bemerkung.Add(bm); //{mit foreign key} Inhalte zur Tabelle hinzufügen
            Rechnungsposten rp = setRechnungsposten(1, 1, 6, 1, 555555, 888888, "IBAN", "Institut", "KontoInhaber", 1); //Tabelle rechnungsposten füllen
            context.Rechnungsposten.Add(rp); //{mit foreign key} Inhalte zur Tabelle hinzufügen
            Status state = setStatus(1, 1, 1, 1); //Tabelle status füllen
            context.Status.Add(state); //{mit foreign key} Inhalte zur Tabelle hinzufügen

            //SQLite-db füllen
            context.SaveChanges(); //alle Änderungen in der DB-Datei speichern

                        watch.Stop(); //Zeit anhalten
                        Console.WriteLine("\nSchreibzeit: " + watch.ElapsedMilliseconds.ToString() + " (ms)"); //Ausgeben wielange das Schreiben in die DB gedauert hat
            watch.Reset(); //Zeit zurücksetzen auf 0
            watch.Start(); //Zeit erneut laufen lassen


            /*
            * Ausgabe der DB-Inhalte in der console
            */

            Console.WriteLine("\n=< Kunden >============================"); //Überschrift
            getKunden(context.kunden.ToList()); //Tabelle ausgeben
            Console.WriteLine("=< Rechnung >============================"); //Überschrift
            getRechnung(context.Rechnung.ToList()); //Tabelle ausgeben
            Console.WriteLine("=< Bemerkung >============================"); //Überschrift
            getBemerkung(context.Bemerkung.ToList()); //Tabelle ausgeben
            Console.WriteLine("=< Status >============================"); //Überschrift
            getStatus(context.Status.ToList()); //Tabelle ausgeben
            Console.WriteLine("=< Ausgabe >============================"); //Überschrift
            getAusgabe(context.Ausgabe.ToList()); //Tabelle ausgeben
            Console.WriteLine("=< Abo >============================"); //Überschrift
            getAbo(context.Abo.ToList()); //Tabelle ausgeben
            Console.WriteLine("=< Rechnungsposten >============================"); //Überschrift
            getRechnungsposten(context.Rechnungsposten.ToList()); //Tabelle ausgeben

            watch.Stop(); //Zeit anhalten
            Console.WriteLine("\nLesezeit: " + watch.ElapsedMilliseconds.ToString() + " (ms)"); //Ausgeben wie lange das Lesen aus der DB gedauert hat

            //Beenden
            Console.Write("\nBitte return drücken, um die Anwendung zu beenden."); Console.ReadLine();
            //Signale ausgeben beim beenden
            Console.Beep(500, 200); Console.Beep(900, 200);
        }//end main function



        /*******
        *
        *
        * ********/

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

        private static void getBINinfo(string AppDirectory)
        {
            try
            {
                var versionInfo = FileVersionInfo.GetVersionInfo(AppDirectory);

                Console.WriteLine("DLL-Geladen");
                Console.WriteLine("Productname: " + versionInfo.ProductName);
                Console.WriteLine("ProductVersion: " + versionInfo.ProductVersion);
                Console.WriteLine("FileVersion: " + versionInfo.FileVersion);
                            }
            catch { }
        }
            
        //aktuelle Zeit als Integer berechnen
        private static int current_timestamp()
        {
            DateTime date1 = new DateTime(1970, 1, 1);  //Refernzdatum (festgelegt)
                                                        //DateTime date2 = DateTime.Now;              //jetztiges Datum / Uhrzeit
            DateTime date2 = DateTime.Now.ToUniversalTime();
            TimeSpan ts = new TimeSpan(date2.Ticks - date1.Ticks);  // das Delta ermitteln
                                                                    // Das Delta als gesammtzahl der sekunden ist der Timestamp
            return (Convert.ToInt32(ts.TotalSeconds));
        }

        //UnixTimestamp aus der Datenbank in datetime umwandeln
        public static DateTime TimeStampToDateTime(double unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }

        /***********
        * Funktionen zum Füllen der DB
        * Tabellen:
        *
        * ***************/

        private static Kunden setKunden(String Firma = "", String Vorname = "", String Nachname = "", String Straße = "", string HausNR = "", int PLZ = 0, String Ort = "", String Postfach = "", String Land = "", String Telefon = "", String Fax = "", String EMail = "")
        {
            Kunden kunde = new Kunden()
            {
                firma = Firma,
                vorname = Vorname,
                nachname = Nachname,
                straße = Straße,
                hausnr = HausNR,
                plz = PLZ,
                ort = Ort,
                postfach = Postfach,
                land = Land,
                telefon = Telefon,
                fax = Fax,
                email = EMail,
                active = 1,
                bemerkung_id = 0,
                status_id = 0,
                erstellt_am = current_timestamp(),
                geändert_am = 0,
            };
            return kunde;
        }

        private static Status setStatus(int Eintritt, int Austritt, int Flag, int KundenID)
        {
            Status state = new Status()
            {
                eintritt_am = Eintritt,
                austritt_am = Austritt,
                flag = Flag,
                kunden_id = KundenID,
            };
            return state;
        }

        private static Bemerkung setBemerkung(String Txt, int KundenID)
        {
            Bemerkung bm = new Bemerkung()
            {
                text = Txt,
                datum = current_timestamp(),
                kunden_id = KundenID,
            };
            return bm;
        }

        private static Ausgabe setAusgabe(int Ausg, decimal Preis)
        {
            Ausgabe ag = new Ausgabe()
            {
                ausgabe = Ausg,
                preis = Preis,
                datum = current_timestamp(),
            };

            return ag;
        }

        private static Rechnung setRechnung(String Firma = "", String Vorname = "", String Nachname = "", String Straße = "", string HausNR = "", int PLZ = 0, String Ort = "", String Postfach = "", String Land = "", String Telefon = "", String Fax = "", String EMail = "", int BemerkungID = 0, int Gesendet_am = 0)
        {
            Rechnung rn = new Rechnung()
            {
                firma = Firma,
                vorname = Vorname,
                nachname = Nachname,
                straße = Straße,
                hausnr = HausNR,
                plz = PLZ,
                ort = Ort,
                postfach = Postfach,
                land = Land,
                telefon = Telefon,
                fax = Fax,
                email = EMail,
                bemerkung_id = BemerkungID,
                erstellt_am = current_timestamp(),
                gesendet_am = Gesendet_am,
            };
            return rn;
        }

        private static Rechnungsposten setRechnungsposten(int KundenId = 0, int RechnungID = 0, int Anzahl = 0, int AboID = 0, int Konto_nr = 0, int BLZ = 0, String IBAN = "", String Institut = "", String KontoInhaber = "", int BemerkungID = 0)
        {
            Rechnungsposten rp = new Rechnungsposten()
            {
                kunden_id = KundenId,
                rechnung_id = RechnungID,
                anzahl = Anzahl,
                abo_id = AboID,
                kontonr = Konto_nr,
                blz = BLZ,
                iban = IBAN,
                institut = Institut,
                kontoinhaber = KontoInhaber,
                erstellt_am = current_timestamp(),
                bemerkung_id = BemerkungID,
            };
            return rp;
        }

        private static Abo setAbo(int a_von = 0, int a_bis = 0, int b_am = 0, int b_von = 0, int b_bis = 0, int bemerkungID = 0)
        {
            Abo ab = new Abo()
            {
                ausgabe_von = a_von,
                ausgabe_bis = a_bis,
                bezahlt_am = b_am,
                bezahlt_von = b_von,
                bezahlt_bis = b_bis,
                bemerkung_id = bemerkungID,
            };
            return ab;
        }


        /********
        * Funktion zur Ausgabe
        */
        private static void getKunden(List<DBTest.Kunden> data)
        {
            foreach (var item in data)
            {
                Console.Write(item.id + " " + item.firma + " " + item.vorname + " " + item.nachname + " " + item.straße + " " + item.hausnr + " " + item.plz + " " + item.postfach + " " + item.land + " " + item.telefon + " " + item.fax + " " + item.email + " " + item.bemerkung_id + " " + " " + TimeStampToDateTime(item.erstellt_am));
                Console.WriteLine();
            }
        }

        private static void getRechnung(List<DBTest.Rechnung> data)
        {
            foreach (var item in data)
            {
                Console.Write(item.id + " " + item.kunden_id + " " + item.firma + " " + item.vorname + " " + item.nachname + " " + item.straße + " " + item.hausnr + " " + item.plz + " " + item.postfach + " " + item.land + " " + item.telefon + " " + item.fax + " " + item.email + " " + TimeStampToDateTime(item.erstellt_am));
                Console.WriteLine();
            }
        }

        private static void getBemerkung(List<DBTest.Bemerkung> data)
        {
            foreach (var item in data)
            {
                Console.Write(item.id + " " + item.kunden_id + "#" + " " + item.text + " " + TimeStampToDateTime(item.datum));
                Console.WriteLine();
            }
        }

        private static void getStatus(List<DBTest.Status> data)
        {
            foreach (var item in data)
            {
                Console.Write(item.id + " " + item.kunden_id + "#" + item.kunden.status_id + " " + item.flag + " " + TimeStampToDateTime(item.eintritt_am) + " " + TimeStampToDateTime(item.austritt_am));
                Console.WriteLine();
            }
        }

        private static void getAusgabe(List<DBTest.Ausgabe> data)
        {
            foreach (var item in data)
            {
                Console.Write(item.id + " " + item.ausgabe + " " + item.preis + "EURO " + TimeStampToDateTime(item.datum));
                Console.WriteLine();
            }
        }

        private static void getAbo(List<DBTest.Abo> data)
        {
            foreach (var item in data)
            {
                Console.Write(item.id + " " + item.ausgabe_bis + " " + item.ausgabe_von + " " + TimeStampToDateTime(item.bezahlt_am) + " " + item.bemerkung_id);
                Console.WriteLine();
            }
        }

        private static void getRechnungsposten(List<DBTest.Rechnungsposten> data)
        {
            foreach (var item in data)
            {
                Console.Write(item.id + " " + item.abo_id + " " + item.anzahl + " " + TimeStampToDateTime(item.erstellt_am) + " " + item.bemerkung_id + " " + item.kunden_id + " " + item.kontoinhaber + " " + item.institut + " " + item.kontonr);
                Console.WriteLine();
            }
        }

    }//end class
} //end namespace
