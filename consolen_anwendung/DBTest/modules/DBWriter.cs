/*
 * Created by SharpDevelop.
 * User: user
 * Date: 20.10.2018
 * Time: 21:46
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Data.Entity;

namespace DBTest.modules
{
	/// <summary>
	/// Diese Klasse entspricht einer schreibenden Transaktion auf die unterliegende Datenbank.
	/// Die Klasse öffnet bei Instanziierung automatisch eine DbContextTransaction.
	/// Die Klasse implementiert das Interface IDisposable; die Dispose-Methode schließt die Transaktion ab und schreibt
	/// Änderungen in die Datenbank. Dabei wird auch die Dispose-Methode der im Konstruktor geöffneten DbContextTransaction
	/// aufgerufen.
	/// 
	/// Benutzung:
	/// using (DBWriter writer = db.getDBWriter())
    /// {
    /// 	// etwas schreiben durch Benutzung der Methoden von writer
    /// }
    /// Schreiboperationen in den using-Block einfügen. Wenn der using-Block verlassen wird wird die Transaktion
    /// automatisch abgeschlossen.
	/// </summary>
	public class DBWriter : IDisposable
	{
		private DatabaseContext context;
		private DbContextTransaction transaction;
		
		public DBWriter(DatabaseContext context)
		{
			this.context = context;
			this.transaction = context.Database.BeginTransaction();
		}
		
		public void Dispose()
		{
			context.SaveChanges();
			transaction.Commit();
			transaction.Dispose();
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
	}
}
