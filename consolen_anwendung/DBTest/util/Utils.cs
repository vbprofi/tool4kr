/*
 * Created by SharpDevelop.
 * User: user
 * Date: 20.10.2018
 * Time: 13:29
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
#region Using
using System;
#endregion Using

namespace DBTest.util
{
	/// <summary>
	/// Klasse mit allgemeinen Utility-Funktionen, die nirgendwo sonst reinpassen.
	/// </summary>
	public static class Utils
	{
		//aktuelle Zeit als Integer berechnen
        public static int current_timestamp()
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
	}
}
