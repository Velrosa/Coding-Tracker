using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data.SQLite;
using System.Configuration;

namespace Coding_Tracker
{
    internal class Program
    {
        // Stores Connection string to database in a variable from the config file.
        static string conString = ConfigurationManager.AppSettings.Get("conString");
        static void Main(string[] args)
        {
            // Checks if the database file exists, if not it creates one and populates it with a table.
            if (!File.Exists("database.sqlite3"))
            {
                SQLiteConnection.CreateFile("database.sqlite3");

                using (var con = new SQLiteConnection(conString))
                {
                    using (var cmd = con.CreateCommand())
                    {
                        con.Open();
                        cmd.CommandText = "CREATE TABLE sessions (start_time TEXT, end_time TEXT, total_time TEXT);"; //id INTEGER PRIMARY KEY AUTOINCREMENT, 
                        cmd.ExecuteNonQuery();
                    }
                }
            }

            // Main runtime loop for the application.
            while (true)
            {
                DisplayMenu();
            }
        }
        public static void DisplayMenu()
        {
            // Variable for if an Active coding session is open or not.
            string activeState;
            if (SessionController.GetActive() == null) { activeState = "Begin"; } else { activeState = "End"; }

            Console.Clear();

            Console.WriteLine("\n MAIN MENU\n\n" +
                                " What would you like to do?\n\n" +
                                " Type 0 to Close Application.\n" +
                                " Type 1 to View All Coding Sessions.\n" +
                                " Type 2 to Add a Coding Session.\n" +
                                " Type 3 to Update a Coding Session.\n" +
                                " Type 4 to Delete a Coding Session.\n" +
                                " Type 5 to {0} an active Coding Session\n", activeState);

            // Users selection from the Menu.
            string selector = Convert.ToString(Console.ReadKey(true).KeyChar);

            switch (selector)
            {
                case "0":
                    Environment.Exit(0);
                    break;
                case "1":
                    SessionView.ShowTable(selector);
                    break;
                case "2":
                    Console.Clear();
                    SessionView.InsertView(selector);
                    break;
                case "3":
                    Console.Clear();
                    SessionView.UpdateView(selector);
                    break;
                case "4":
                    Console.Clear();
                    SessionView.DeleteView(selector);
                    break;
                case "5":
                    Console.Clear();
                    if (SessionController.GetActive() == null)
                    {
                        SessionView.OpenSession(selector);
                    }
                    else { SessionView.CloseSession(SessionController.GetActive()); }

                    break;
                default:
                    Console.Write(" Invalid Entry. press any key to return... ");
                    Console.ReadKey();
                    break;

            }
        }
    }
}
