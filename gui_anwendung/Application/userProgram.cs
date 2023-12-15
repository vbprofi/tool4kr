using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

using System.Collections;
using KRTool.Model;
using KRTool.View;
using KRTool.Controller;


namespace KRTool
{
    class UserProgram
    {
       public UsersView view;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        //[STAThread]
        //static void _Main()
        public UserProgram()
        {
            view = new UsersView();
            view.Visible = false;

            // Add some dummy data
            IList users = new ArrayList();
                        users.Add(new User("Vladimir",   "Putin",         "122",    "Government of Russia",          User.SexOfPerson.Male));
            users.Add(new User("Barack",     "Obama",         "123",    "Government of USA",             User.SexOfPerson.Male));
            users.Add(new User("Stephen",    "Harper",        "124",    "Government of Canada",          User.SexOfPerson.Male));
            users.Add(new User("Jean",       "Charest",       "125",    "Government of Quebec",          User.SexOfPerson.Male));
            users.Add(new User("David",      "Cameron",       "126",    "Government of United Kingdom",  User.SexOfPerson.Male));
            users.Add(new User("Angela",     "Merkel",        "127",    "Government of Germany",         User.SexOfPerson.Female));
            users.Add(new User("Nikolas",    "Sarkozy",       "128",    "Government of France",          User.SexOfPerson.Male));
            users.Add(new User("Silvio",     "Berlusconi",    "129",    "Government of Italy",           User.SexOfPerson.Male));
            users.Add(new User("Yoshihiko",  "Noda",          "130",    "Government of Japan",           User.SexOfPerson.Male));
            
            UsersController controller = new UsersController(view, users);
            controller.LoadView();
            view.ShowDialog();
        }

        public void start()
        {
                        //view.ShowDialog();
        }
    }
}
