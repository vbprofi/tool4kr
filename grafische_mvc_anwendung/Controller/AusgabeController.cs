using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using KRTool.Model;
using DBTest.modules;
using DBTest.util;
using DBTest;

namespace KRTool.Controller
{
   public class AusgabeController
    {
        iAusgabe _view;
        String _dbText;

        public AusgabeController(iAusgabe view, String txtForm)
        {
            _view = view;
            KRDatabase db = new KRDatabase();
            _dbText = txtForm + Environment.NewLine + "Datenbankversion: " + db.getVersionString();
            _dbText += Environment.NewLine;
            _dbText += Environment.NewLine;
            _dbText += Environment.NewLine;
            _dbText += createSampleDataAllTables(db); //keine Änderung
            _dbText += Environment.NewLine;
            _dbText += createSampleDataAllTables(db); //schneller geworden.

            using (DBWriter writer = db.getDBWriter())
            {
                Ausgabe letzteAusgabe = writer.getCurrentIssue();
                decimal preisLetzteAusgabe = writer.getPriceOfIssue(letzteAusgabe.ausgabe);
                writer.addNextIssue(preisLetzteAusgabe + 1);
                _dbText+="Neue Ausgabe hinzugefügt: " + letzteAusgabe;
            }

                        _dbText += Environment.NewLine;
            _dbText += printTableData(db);
            _dbText += Environment.NewLine;

            using (DBReader reader = db.getDBReader())
            {
                Ausgabe letzteAusgabe = reader.getCurrentIssue();
                decimal preisLetzteAusgabe = reader.getPriceOfIssue(letzteAusgabe.ausgabe);
                _dbText+="Letzte Ausgabe: " + letzteAusgabe + " (Preis=" + preisLetzteAusgabe + " EURO)";
                _dbText += Environment.NewLine;
                var abk = reader.getAboByKunde();
                _dbText+= abk;
            }

            view.SetController(this);
        }

        public String dbText
        {
            get { return _dbText; }
        }

        /**
 * Test-Methode: Fügt zu allen Tabellen einen Datensatz hinzu und misst die Schreibzeit.
 */
        private string createSampleDataAllTables(KRDatabase db)
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

                Abo ab = DBRecordFactory.createAbo(1, 1, 1, 1, 1, 1, 1); //Tabelle abo füllen
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
            return "Schreibzeit: " + watch; //Ausgeben wie lange das Schreiben in die DB gedauert hat
        }


        /**
         * Test-Methode: Gibt alle Tabellen aus und misst die Lesezeit.
         */
        private string printTableData(KRDatabase db)
        {
            string txt = "";
            //Stopuhr zur Zeit Messung erzeugen
            CounterStopWatch watch = new CounterStopWatch();
            watch.ResetAndStart(); //Stopuhr zurücksetzen auf 0 und starten
            using (DBReader reader = db.getDBReader())
            {
                /*
                 * Ausgabe der DB-Inhalte in der console
                 */
                txt += "\n=< Kunden >============================" + Environment.NewLine; //Überschrift
                txt += printTable(reader.getKunden()); //Tabelle ausgeben
                watch++;

                txt += "=< Rechnung >============================" + Environment.NewLine; //Überschrift
                txt += printTable(reader.getRechnungen()); //Tabelle ausgeben
                watch++;

                txt += "=< Bemerkung >============================" + Environment.NewLine; //Überschrift
                txt += printTable(reader.getBemerkungen()); //Tabelle ausgeben
                watch++;

                txt += "=< Status >============================" + Environment.NewLine; //Überschrift
                txt += printTable(reader.getStatus()); //Tabelle ausgeben
                watch++;

                txt += "=< Ausgabe >============================" + Environment.NewLine; //Überschrift
                txt += printTable(reader.getAusgaben()); //Tabelle ausgeben
                watch++;

                txt += "=< Abo >============================" + Environment.NewLine; //Überschrift
                txt += printTable(reader.getAbos()); //Tabelle ausgeben
                watch++;

                txt += "=< Rechnungsposten >============================" + Environment.NewLine; //Überschrift
                txt+= printTable(reader.getRechnungsposten()); //Tabelle ausgeben
                watch++;
            }
            watch.Stop(); //Zeit anhalten
            txt += "\nLesezeit: " + watch; //Ausgeben wie lange das Lesen aus der DB gedauert hat
            return txt;
        }

        private string printTable(IEnumerable<DBRecord> data)
        {
            string txt = "";
            foreach (var item in data)
            {
                txt += item + Environment.NewLine;
            }
            return txt;
        }

        public void LoadView()
        {
            _view.AddToAusgabe(_dbText);
        }
 }
}
