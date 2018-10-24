#region Überschrift
/*
 * Created by SharpDevelop.
 * User: user
 * Date: 20.10.2018
 * Time: 21:46
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
#endregion Überschrift
#region Using
using System;
using System.Data.Entity;
#endregion Using

namespace DBTest.modules
{
	/// <summary>
	/// Diese Klasse entspricht einer schreibenden Transaktion auf die unterliegende Datenbank.
	/// Es ist eine Ableitung von DBReader, der um Schreibmöglichkeiten erweitert wird.
	/// 
	/// Benutzung:
	/// using (DBWriter writer = db.getDBWriter())
    /// {
    /// 	// etwas schreiben durch Benutzung der Methoden von writer
    /// }
    /// (Lese- und) Schreiboperationen in den using-Block einfügen. Wenn der using-Block verlassen
    /// wird wird die Transaktion automatisch abgeschlossen.
	/// </summary>
	public class DBWriter : DBReader, IDisposable
	{
		public DBWriter(DatabaseContext context)
			: base(context)
		{
		}

		/**
		 * Speichert Änderungen in der Datenbank ab. Kann mehrmals pro Transaktion aufgerufen werden.
 		 */ 		
		public void SaveChanges()
		{
			context.SaveChanges();
		}
		
		public new void Dispose()
		{
			SaveChanges();
			base.Dispose();
		}
		
		public void addRecord(Abo record)
		{
			context.abo.Add(record);
		}
		
		public void addRecord(Ausgabe record)
		{
			context.ausgabe.Add(record);
		}
		
		public void addRecord(Bemerkung record)
		{
			context.bemerkung.Add(record);
		}
		
		public void addRecord(Kunden record)
		{
			context.kunden.Add(record);
		}
		
		public void addRecord(Rechnung record)
		{
			context.rechnung.Add(record);
		}
		
		public void addRecord(Rechnungsposten record)
		{
			context.rechnungsposten.Add(record);
		}
		
		public void addRecord(Status record)
		{
			context.status.Add(record);
		}
		
		/**
		 * Erzeugt eine neue Ausgabe mit um einer 1 höheren Nummer als die höchste in der Datenbank gespeicherte
		 * Ausgabennummer.
		 */
		public void addNextIssue(decimal price)
		{
			Ausgabe current = getCurrentIssue();
			Ausgabe next = DBRecordFactory.createAusgabe(current.ausgabe+1, price);
			addRecord(next);
		}
	}
}
