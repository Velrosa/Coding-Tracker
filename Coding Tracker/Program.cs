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
            while (true)
            {
                MenuController.DisplayMenu();
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
