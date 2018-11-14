#region Überschrift
/*
 * Created by SharpDevelop.
 * User: user
 * Date: 20.10.2018
 * Time: 15:56
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
#endregion Überschrift
#region Using
using System;
#endregion Using

namespace KRTool.Model
{
	/// <summary>
	/// Abstrakte Oberklasse für einen Datensatz in einer Tabelle der DB.
	/// </summary>
	public abstract class DBRecord
	{
		/**
		 * Zwingt abgeleitete Klassen dazu, diese Methode zu implementieren.
		 */
		public abstract override string ToString();
		
	}
}
