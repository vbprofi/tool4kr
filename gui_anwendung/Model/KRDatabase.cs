﻿#region Überschrift
/*
 * Created by SharpDevelop.
 * User: user
 * Date: 20.10.2018
 * Time: 13:44
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
#endregion Überschrift
#region Using
using System;
using System.Runtime.CompilerServices;
using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;
#endregion Using

namespace KRTool.Model
{
    /// <summary>
    /// Zentrale Klasse für Datenbankoperationen. Abstrahiert die Datenbank und stellt Datenbanken-Transaktionen
    /// über Objekte der Klassen DBReader und DBWriter zur Verfügung.
    /// </summary>
    public class KRDatabase
    {
       static String dbdateiname = Environment.CurrentDirectory + @"\test.db";
        /** The context */
        private DatabaseContext context = new DatabaseContext("Data Source =" + dbdateiname + "; Version = 3;Cache Size=2048;Journal Mode=Off;Synchronous=Full;Compress=True;UTF8Encoding=True;");

        public KRDatabase()
        {
                        /*
             * folgende Zeilen könnten die datensätze einlesen
            var abo = context.abo.ToList();
            var ausgabe = context.ausgabe.ToList();
            var bm = context.bemerkung.ToList();
            var kunden = context.kunden.ToList();
            var rechnung = context.rechnung.ToList();
            var rechnungsposten = context.rechnungsposten.ToList();
            var status = context.status.ToList();
            */
            //Konfigurieren           
            configure(context);
        }

        public DBReader getDBReader()
        {
            return new DBReader(context);
        }

        public DBWriter getDBWriter()
        {
            return new DBWriter(context);
        }

        public String getVersionString()
        {
            return "v " + context.Database.Connection.ServerVersion + " | Modell " + context.Version.ToString();
        }

        /**
		 * Init-Methode zum konfigurieren eines Context.
		 */
        private void configure(DatabaseContext context)
        {
            context.Configuration.AutoDetectChangesEnabled = false;
            context.Configuration.LazyLoadingEnabled = false;
            context.Configuration.ValidateOnSaveEnabled = false;
            context.Configuration.UseDatabaseNullSemantics = false;
            context.Configuration.ProxyCreationEnabled = false;
            context.Configuration.EnsureTransactionsForFunctionsAndCommands = false;
            context.SaveChanges();
        }
    }
}
