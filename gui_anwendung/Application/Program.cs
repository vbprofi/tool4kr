using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Threading;
using System.Threading.Tasks;
using System.Net;

namespace KRTool
{
    static class Program
    {
        static bool createdNew; //Variable zum Prüfen des eigenen Prozesses

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            System.Threading.Mutex mutex = new System.Threading.Mutex(true, Application.ProductName, out createdNew);
            if (createdNew)
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                WindowsFormsSynchronizationContext.SetSynchronizationContext(new WindowsFormsSynchronizationContext());

                //MVC Testbeispiel: UserProgram
                UserProgram u = new UserProgram();
                Thread t1 = new Thread(new ThreadStart(u.start));
                t1.Start();
                Application.DoEvents();
                KRTool p = new KRTool();
                p.ExitRequested += p_ExitRequested;

                //while (true)
                //{
                //if (!t1.IsAlive)
                    //{
                        Task programStart = p.Start();
                        HandleExceptions(programStart);
                //t1.Abort();
                //Thread.Sleep(1000);
                //break;
                //}
                //}
                
                Application.Run();
                mutex.ReleaseMutex(); //Speicher freigeben
                mutex.Close(); //beenden
                mutex.Dispose(); //speicher aufräumen
                MessageBox.Show("Programm wird nun beendet." + Environment.NewLine + "Auf wiedersehen!", "Programm wird beendet!", MessageBoxButtons.OK, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
            }
            else
            {
                MessageBox.Show("Eine Instanz dieser Anwendung läuft bereits." + Environment.NewLine + "Bitte schließen Sie die Anwendung, um das Programm neu zu starten.", "Fehler beim Starten der Anwendung", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        static void p_ExitRequested(object sender, EventArgs e)
        {
            Application.ExitThread();
        }

        private static async void HandleExceptions(Task task)
        {
            try
            {
                await Task.Yield();
                await task;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Application.Exit();
            }
        }

    }//end class
}//end namespace
