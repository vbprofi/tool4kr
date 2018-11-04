﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

using System.Collections;
using System.IO;
using System.Reflection;
using System.Diagnostics;
using KRTool.Model;
using KRTool.View;
using KRTool.Controller;
using DBTest.util;

namespace KRTool
{
    static class Program
    {
        public static String assemblyDirectory = Environment.CurrentDirectory.ToString(); //ermittelt den aktuellen pfad der Anwendung
        //public static String dbdateiname = assemblyDirectory + @"\test.db"; //Dateiname der SQLite-Datenbank
        private static String dlldateiname = assemblyDirectory + @"\dlls.txt"; //Datei für schreiben der DLL-Infos
      static  bool createdNew; //Variable zum Prüfen des eigenen Prozesses

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            System.Threading.Mutex mutex = new System.Threading.Mutex(true, Application.ProductName, out createdNew);
            if (createdNew)
            {
                String txt = "";
                txt += getAssembly();
                //DLL-Dateiinfos in Datei schreiben
                writeLoadDLL();

                Form1 view = new Form1();
                view.Visible = false;

                AusgabeController controller = new AusgabeController(view, txt);
                controller.LoadView();
                view.Text = getAssembly("title");
                view.ShowDialog();
                mutex.ReleaseMutex();
            }
            else
            {
                MessageBox.Show("Programm wurde bereits gestartet!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            }

        private static String getBINinfo(string AppDirectory)
        {
            String wText = "";
            try
            {
                var versionInfo = FileVersionInfo.GetVersionInfo(AppDirectory);

                wText += "Productname: " + versionInfo.ProductName + Environment.NewLine;
                wText += "ProductVersion: " + versionInfo.ProductVersion + Environment.NewLine;
                wText += "FileVersion: " + versionInfo.FileVersion + Environment.NewLine;
                return wText;
            }
            catch { return wText; }
            //return wText;
        }

        //geladene DLL-Infos in Textdatei schreiben
        private static void writeLoadDLL()
        {
            String wText = "";
            try
            {
                RWFile wFile = new RWFile();
                wText += DateTime.Now + ": Programm gestartet." + Environment.NewLine + "Folgende DLL-Dateien geladen..." + Environment.NewLine + "----------------------" + Environment.NewLine;
                //dateien info
                wText += getBINinfo(assemblyDirectory + @"\autogeneratedDB.dll") + Environment.NewLine;
                wText += getBINinfo(assemblyDirectory + @"\System.Data.SQLite.dll") + Environment.NewLine;
                wText += getBINinfo(assemblyDirectory + @"\EntityFramework.dll") + Environment.NewLine;
                wText += getBINinfo(assemblyDirectory + @"\System.Data.SQLite.EF6.dll") + Environment.NewLine;
                wText += getBINinfo(assemblyDirectory + @"\SQLite.CodeFirst.dll") + Environment.NewLine;
                wText += getBINinfo(assemblyDirectory + @"\Controller.dll") + Environment.NewLine;
                wText += getBINinfo(assemblyDirectory + @"\Model.dll") + Environment.NewLine;
                wText += getBINinfo(assemblyDirectory + @"\View.dll") + Environment.NewLine;

                wFile.WriteFile(dlldateiname, wText);
            }
            catch { }
        }

        private static string getAssembly(String info = "")
        {
            Assembly execAssembly = Assembly.GetCallingAssembly();
            AssemblyName name = execAssembly.GetName();

            if (info.Length <= 0 || info == "")
            {
                return string.Format("{0} v{1:0} for .Net ({2}){3}",
                                name.Name,
                name.Version.ToString(),
                execAssembly.ImageRuntimeVersion,
                Environment.NewLine
                );
            }
            else
            if (info == "title") return name.Name + " v" + name.Version.ToString();

            return "";
        }

    }//end class
}//end namespace
