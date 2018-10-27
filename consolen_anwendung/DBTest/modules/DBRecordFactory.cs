#region Überschrift
/*
 * Created by SharpDevelop.
 * User: user
 * Date: 20.10.2018
 * Time: 13:23
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
#endregion Überschrift
#region Using
using System;
using DBTest.util;
#endregion Using

namespace DBTest
{
	/// <summary>
	/// Factory-Klasse, um neue Datensätze zu erzeugen.
	/// </summary>
	public static class DBRecordFactory
	{
	    public static Kunden createKunden(String Firma = "", String Vorname = "", String Nachname = "", String Straße = "", string HausNR = "", int PLZ = 0, String Ort = "", String Postfach = "", String Land = "", String Telefon = "", String Fax = "", String EMail = "")
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
                erstellt_am = Utils.current_timestamp(),
                geändert_am = 0,
            };
            return kunde;
        }
	    
        public static Status createStatus(int Eintritt, int Austritt, int Flag, int KundenID)
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
        
        public static Bemerkung createBemerkung(String Txt, int KundenID)
        {
            Bemerkung bm = new Bemerkung()
            {
                text = Txt,
                datum = Utils.current_timestamp(),
                kunden_id = KundenID,
            };
            Console.WriteLine("Bem="+bm);
            return bm;
        }
        
        public static Bemerkung createBemerkung(String Txt, Kunden kunde)
        {
        	return createBemerkung(Txt, kunde.id);
        }
        
        public static Ausgabe createAusgabe(int Ausg, decimal Preis)
        {
            Ausgabe ag = new Ausgabe()
            {
                ausgabe = Ausg,
                preis = Preis,
                datum = Utils.current_timestamp(),
            };

            return ag;
        }
        
        public static Rechnung createRechnung(String Firma = "", String Vorname = "", String Nachname = "", String Straße = "", string HausNR = "", int PLZ = 0, String Ort = "", String Postfach = "", String Land = "", String Telefon = "", String Fax = "", String EMail = "", int BemerkungID = 0, int Gesendet_am = 0)
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
                erstellt_am = Utils.current_timestamp(),
                gesendet_am = Gesendet_am,
            };
            return rn;
        }
        
        public static Rechnungsposten createRechnungsposten(int KundenId = 0, int RechnungID = 0, int Anzahl = 0, int AboID = 0, int Konto_nr = 0, int BLZ = 0, String IBAN = "", String Institut = "", String KontoInhaber = "", int BemerkungID = 0)
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
                erstellt_am = Utils.current_timestamp(),
                bemerkung_id = BemerkungID,
            };
            return rp;
        }
        
        public static Abo createAbo(int a_von = 0, int a_bis = 0, int b_am = 0, int b_von = 0, int b_bis = 0, int bemerkungID = 0, int kundenid = 0)
        {
            Abo ab = new Abo()
            {
                ausgabe_von = a_von,
                ausgabe_bis = a_bis,
                bezahlt_am = b_am,
                bezahlt_von = b_von,
                bezahlt_bis = b_bis,
                bemerkung_id = bemerkungID,
                kunden_id = kundenid,
            };
            return ab;
        }
        
        public static Abo createAbo(int a_von, int a_bis, int b_am, int b_von, int b_bis, Bemerkung bemerkung, Kunden kunde)
        {
        	return createAbo(a_von, a_bis, b_am, b_von, b_bis, bemerkung.id, kunde.id);
        }
	}
}
