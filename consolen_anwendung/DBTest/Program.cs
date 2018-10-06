using System;
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
        public static String assemblyDirectory = Environment.CurrentDirectory.ToString();


        static void Main(string[] args)
        {
            //Titel der Console
                        getAssembly("title");
            //Signale ausgeben beim Starten
            Console.Beep(900, 100);
                                    Console.Beep(500, 100);
            
            //Inhalt der Console mit Infos füllen
            getAssembly();
            //dateien info
            getBINinfo(assemblyDirectory + @"\System.Data.SQLite.dll");

            //Datenbankobjekt erzeugen
            DatabaseContext context = new DatabaseContext();

kunden kunde = setKunden("Firma", "Vorname", "Nachname", "Straße", "HausNR", 12345, "Ort", "Postfach", "Land", "Telefon", "Fax", "EMail");
            context.kunden.Add(kunde);
            Ausgabe ag = setAusgabe(199, Convert.ToDecimal("3,00"));
            context.Ausgabe.Add(ag);
            Rechnung rn = setRechnung("Firma", "Vorname", "Nachname", "Straße", "HausNR", 12345, "Ort", "Postfach", "Land", "Telefon", "Fax", "EMail", 0, 0);
            context.Rechnung.Add(rn);
            Abo ab = setAbo(0, 0, 0, 0, 0, 0);
            //context.Abo.Add(ab);
            Rechnungsposten rp = setRechnungsposten(0, 0, 2, 0, 0, 0, "IBAN", "Institut", "KontoInhaber", 0);
            //context.Rechnungsposten.Add(rp);
            Status state = setStatus(0, 0, 0, 0);
            //context.Status.Add(state);
            Bemerkung bm = setBemerkung("Txt", 0, 0);
            //context.Bemerkung.Add(bm);

            //SQLite-db füllen
                                    context.SaveChanges();



            /*
             * Ausgabe der DB-Inhalte in der console
             */

            Console.WriteLine("=< kunden >============================");
                        getKunden(context.kunden.ToList());

            Console.WriteLine("=< Rechnung >============================");
            getRechnung(context.Rechnung.ToList());


            //Beenden
            Console.Write("\nBitte return drücken, um die Anwendung zu beenden."); Console.ReadLine();
            //Signale ausgeben beim beenden
            Console.Beep(500, 200);
            Console.Beep(900, 200);
        }//end main function

        private static void getAssembly(String info="")
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
                Console.WriteLine($"Productname: {versionInfo.ProductName}");
                Console.WriteLine($"ProductVersion: {versionInfo.ProductVersion}");
                Console.WriteLine($"FileVersion: {versionInfo.FileVersion}");
                Console.WriteLine("\n");
                            } catch { }
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

        private static kunden setKunden(String Firma="", String Vorname="", String Nachname="", String Straße="", string HausNR="", int PLZ=0, String Ort="", String Postfach="", String Land="", String Telefon="", String Fax="", String EMail="")
        {
            kunden kunde = new kunden()
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

        private static Bemerkung setBemerkung(String Txt, int Datum, int KundenID)
        {
            Bemerkung bm = new Bemerkung()
            {
                text = Txt,
                datum = Datum,
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

        private static Rechnung setRechnung(String Firma = "", String Vorname = "", String Nachname = "", String Straße = "", string HausNR = "", int PLZ = 0, String Ort = "", String Postfach = "", String Land = "", String Telefon = "", String Fax = "", String EMail = "", int BemerkungID=0, int Gesendet_am=0)
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

        private static Rechnungsposten setRechnungsposten(int KundenId=0, int RechnungID=0, int Anzahl=0, int AboID=0, int Konto_nr=0, int BLZ=0, String IBAN="", String Institut="", String KontoInhaber="", int BemerkungID=0)
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

        private static Abo setAbo(int a_von=0, int a_bis=0, int b_am=0, int b_von=0, int b_bis=0, int bemerkungID=0)
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
         private static void getKunden(List<DBTest.kunden> data)
        {
            foreach (var item in data)
            {
                                Console.Write(item.id + " " + item.vorname + " " + item.nachname + " " + item.straße + " " + item.hausnr + " " + item.plz + " " + item.postfach + " " + item.land + " " + item.telefon + " " + item.fax + " " + item.email + " " + TimeStampToDateTime(item.erstellt_am));
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



    }//end class
} //end namespace
