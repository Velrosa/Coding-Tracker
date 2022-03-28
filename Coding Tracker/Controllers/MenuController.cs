using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace Coding_Tracker
{
    internal class MenuController
    {
        static string conString = ConfigurationManager.AppSettings.Get("conString");
        public static void DisplayMenu()
        {
            string selector = MenuView.MainMenu();

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
                    SessionView.DeleteView(selector);
                    break;
                case "4":
                    Console.Clear();
                    SessionView.UpdateView(selector);
                    break;
                default:
                    Console.Write("Invalid Entry. press key to return... ");
                    Console.ReadKey();
                    break;

            }
        }
        internal class Program
        {
            static void Main(string[] args)
            {
                if (!File.Exists("database.sqlite3"))
                {
                    SQLiteConnection.CreateFile("database.sqlite3");

                    using (var con = new SQLiteConnection(conString))
                    {
                        using (var cmd = con.CreateCommand())
                        {
                            cmd.CommandText = "CREATE TABLE sessions (id INTEGER PRIMARY KEY AUTOINCREMENT, start_time TEXT, end_time TEXT, total_time TEXT);";
                            cmd.ExecuteNonQuery();
                        }
                    }
                }

                while (true)
                {
                    DisplayMenu();
                }
            }
        }
    }
}
