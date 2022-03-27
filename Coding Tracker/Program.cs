using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Collections.Specialized;
using ConsoleTableExt;
using System.Text.RegularExpressions;
using System.Data.SQLite;

namespace Coding_Tracker
{
    internal class Program
    {

        public static string conString = ConfigurationManager.AppSettings.Get("conString");
        static void Main(string[] args)
        {
            var tableData = new List<List<object>>
            {
                new List<object>{ "Sakura Yamamoto", "Support Engineer", "London", 46},
                new List<object>{ "Serge Baldwin", "Data Coordinator", "San Francisco", 28, "something else" },
                new List<object>{ "Shad Decker", "Regional Director", "Edinburgh"},
            };

            //ConsoleTableBuilder.From(tableData).ExportAndWriteLine();

            //Console.ReadLine();

            while (true)
            {
                MainMenu();
            }
        }

        public static void MainMenu()
        {
            Database databaseObject = new Database();

            Console.Clear();

            Console.WriteLine("\nMAIN MENU\n\n" +
                "What would you like to do?\n\n" +
                "Type 0 to Close Application.\n" +
                "Type 1 to View All Records.\n" +
                "Type 2 to Insert Record.\n" +
                "Type 3 to Delete Record.\n" +
                "Type 4 to Update Record.\n");

            string selector = Convert.ToString(Console.ReadKey(true).KeyChar);

            switch (selector)
            {
                case "0":
                    Environment.Exit(0);
                    break;
                case "1":
                    Console.Clear();
                    ViewRecords(selector);
                    break;
                case "2":
                    Console.Clear();
                    InsertRecord();
                    break;
                case "3":
                    Console.Clear();
                    ViewRecords(selector);
                    DeleteRecord();
                    break;
                case "4":
                    Console.Clear();
                    ViewRecords(selector);
                    UpdateRecord();
                    break;
                default:
                    Console.Write("Invalid Entry. press key to return... ");
                    Console.ReadKey();
                    break;

            }

        }

        public static string Validate(string entry, string type)
        {
            int valid_num = 0;


            if (type == "number" || type == "id")
            {
                bool isNumber = int.TryParse(entry, out valid_num);
                while (!isNumber || valid_num < 0)
                {
                    if (entry == "MENU")
                    {
                        return entry;
                    }
                    Console.Write("Invalid entry, Please enter a number: ");
                    entry = Console.ReadLine();
                    isNumber = int.TryParse(entry, out valid_num);
                }
            }

            if (type == "date")
            {
                while (true)
                {
                    if (entry == "MENU")
                    {
                        return entry;
                    }
                    else if (!Regex.IsMatch(entry, @"^\d{1,2}/\d{1,2}/\d{2,4}$"))
                    {
                        Console.Write("Invalid date, Please enter again (DD/MM/YY): ");
                        entry = Console.ReadLine();
                    }
                    else break;

                }
            }
            return entry;

        }

        public static void InsertRecord()
        {
            Console.WriteLine("Inserting a Session Record...  Type MENU to return.");
            Console.Write("Please Enter the Start Date (DD/MM/YY): ");
            string start_time = Validate(Console.ReadLine(), "date");
            if (start_time == "MENU") { return; }

            Console.Write("Please Enter the End Date (DD/MM/YY): ");
            string end_time = Validate(Console.ReadLine(), "date");
            if (end_time == "MENU") { return; }

            using (var con = new SQLiteConnection(conString))
            {
                using (var cmd = con.CreateCommand())
                {
                    con.Open();
                    cmd.CommandText = "INSERT INTO sessions (start_time, end_time) VALUES (@start_time, @end_time)";
                    cmd.Parameters.AddWithValue("@start_time", start_time);
                    cmd.Parameters.AddWithValue("@end_time", end_time);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void ViewRecords(string selector)
        {
            Console.WriteLine("\nDisplaying all session records:\n");

            using (var con = new SQLiteConnection(conString))
            {
                using (var cmd = con.CreateCommand())
                {
                    con.Open();
                    cmd.CommandText = "SELECT rowid, * FROM sessions";

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            Console.WriteLine("  ID  | Start Time | End Time");
                            Console.WriteLine("-----------------------------");
                            while (reader.Read())
                            {
                                Console.WriteLine("{0,5} | {1,8} | {2}", reader["id"], reader["start_time"], reader["end_time"]);
                            }
                        }
                    }
                }
            }

            if (selector == "1")
            {
                Console.Write("\nPress any key to return to menu... ");
                Console.ReadKey();
            }

        }

        public static void DeleteRecord()
        {
            Console.WriteLine("\nDeleting a Record...  Type MENU to return.");
            Console.Write("Enter ID for entry to delete: ");
            string delete_index = Validate(Console.ReadLine(), "id");
            if (delete_index == "MENU") { return; }

            using (var con = new SQLiteConnection(conString))
            {
                using (var cmd = con.CreateCommand())
                {
                    con.Open();
                    cmd.CommandText = "DELETE FROM sessions WHERE id=(@Id)";
                    cmd.Parameters.AddWithValue("@Id", delete_index);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void UpdateRecord()
        {
            Console.WriteLine("\nUpdating a Record...  Type MENU to return.");

            Console.Write("Please Enter the ID of the entry to change: ");
            string entry_id = Validate(Console.ReadLine(), "id");
            if (entry_id == "MENU") { return; }

            Console.Write("Please Enter a start date (DD/MM/YY): ");
            string start_time = Validate(Console.ReadLine(), "date");
            if (start_time == "MENU") { return; }

            Console.Write("Please Enter a end date (DD/MM/YY): ");
            string end_time = Validate(Console.ReadLine(), "date");
            if (end_time == "MENU") { return; }

            using (var con = new SQLiteConnection(conString))
            {
                using (var cmd = con.CreateCommand())
                {
                    con.Open();
                    cmd.CommandText = "UPDATE sessions SET start_time=(@start_time), end_time=(@end_time) WHERE id=(@id) ";
                    cmd.Parameters.AddWithValue("@start_time", start_time);
                    cmd.Parameters.AddWithValue("@end_time", end_time);
                    cmd.Parameters.AddWithValue("@id", entry_id);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
