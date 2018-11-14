#region Überschrift
/*
 * Created by SharpDevelop.
 * User: user
 * Date: 20.10.2018
 * Time: 21:32
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
#endregion Überschrift
#region Using
using System;
using System.Linq;
using System.Data.Entity;
using KRTool.Model.util;
#endregion Using

namespace KRTool.Model
{
	/// <summary>
	/// Diese Klasse entspricht einer lesenden Transaktion auf die unterliegende Datenbank.
	/// Die Klasse öffnet bei Instanziierung automatisch eine DbContextTransaction.
	/// Die Klasse implementiert das Interface IDisposable; die Dispose-Methode schließt die Transaktion ab. Dabei
	/// wird auch die Dispose-Methode der im Konstruktor geöffneten DbContextTransaction aufgerufen.
	/// 
	/// Benutzung:
	/// using (DBReader reader = db.getDBReader())
    /// {
    /// 	// etwas auslesen durch Benutzung der Methoden von reader
    /// }
    /// Schreiboperationen in den using-Block einfügen. Wenn der using-Block verlassen wird wird die Transaktion
    /// automatisch abgeschlossen.
	/// </summary>
	public class DBReader : IDisposable
	{
		protected DatabaseContext context;
		protected DbContextTransaction transaction;
		
		public DBReader(DatabaseContext context)
		{
			this.context = context;
			this.transaction = context.Database.BeginTransaction();
		}
		
		public void Dispose()
		{
			transaction.Commit();
			transaction.Dispose();
		}

		public DbSet<Abo> getAbos()
		{
			return context.abo;
		}
		
		public DbSet<Ausgabe> getAusgaben()
		{
			return context.ausgabe;
		}
		
		public DbSet<Bemerkung> getBemerkungen()
		{
			return context.bemerkung;
		}
		
		public DbSet<Kunden> getKunden()
		{
			return context.kunden;
		}
		
		public DbSet<Rechnung> getRechnungen()
		{
			return context.rechnung;
		}
		
		public DbSet<Rechnungsposten> getRechnungsposten()
		{
			return context.rechnungsposten;
		}
		
		public DbSet<Status> getStatus()
		{
			return context.status;
		}

		/**
		 * Liest die höchste in der Datenbank gespeicherte Ausgabennummer aus.
		 */
		public Ausgabe getCurrentIssue()
		{	
			int maxNo = -1;
			Ausgabe currentIssue = null;
			DbSet<Ausgabe> ausgaben = getAusgaben();
			foreach (Ausgabe a in ausgaben)
			{
				if (a.ausgabe >= maxNo)
				{
					maxNo = a.ausgabe;
					currentIssue = a;
				}
			}
			return currentIssue;
		}

		/**
		 * Liest der Preis einer übergebenen Ausgabennummer aus.
		 * zbw: Exemplarische Query
		 */
		public decimal getPriceOfIssue(int issueNumber)
		{
			decimal price = -1;
			var query = from a in getAusgaben()
            			where a.ausgabe == issueNumber
            			select a;
			var issue = query.FirstOrDefault<Ausgabe>();
			price = issue.preis;
			return price;
		}

        public string getAboByKunde()
        {
            
            var query = from a in getAbos()
                         join k in getKunden()
                         on a.kunden_id equals k.id
                         //into abobykunde
                                                  where a.kunden_id == k.id
                                                                           select new {
                             id = a.id,
                             ausgabe_von = a.ausgabe_von,
                             ausgabe_bis = a.ausgabe_bis,
                             bezahlt_am = a.bezahlt_am,
                             bezahlt_von = a.bezahlt_von,
                             bezahlt_bis = a.bezahlt_bis,
                                                          vorname = k.vorname,
                             nachname = k.nachname,
                             plz = k.plz,
                             ort = k.ort,
                             kundenID = k.id,
                             bemerkung = a.bemerkung_id
                         };
            
            var abks = query.ToArray(); //.FirstOrDefault<Abo>();

            var abk = "";
            foreach (var item in abks)
            {
                abk +=
                    item.id + " " +
                    item.ausgabe_von + " " +
                    item.ausgabe_bis + " " +
                    Utils.TimeStampToDateTime(item.bezahlt_am) + " " +
                    Utils.TimeStampToDateTime(item.bezahlt_von) + " " +
                    Utils.TimeStampToDateTime(item.bezahlt_bis) + " " +
                    item.kundenID + " " +
                    item.vorname + " " +
                    item.nachname + " " +
                    item.plz + " " +
                    item.ort + Environment.NewLine;
            }
            return abk;
        }
	}//end class
}//end namespace
