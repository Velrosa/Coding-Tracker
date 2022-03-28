using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleTableExt;

namespace Coding_Tracker
{
    internal class SessionView
    {
        public static void ShowTable(string selector)
        {
            Console.Clear();

            Console.WriteLine("\nDisplaying all session records:\n");
            
            ConsoleTableBuilder.From(SessionController.GetTable()).ExportAndWriteLine();

            if (selector == "1")
            {
                Console.Write("\nPress any key to return to menu... ");
                Console.ReadKey();
            }
        }

        public static void InsertView(string selector)
        {
            Console.WriteLine("Inserting a Session Record...  Type MENU to return.");
            
            Console.Write("Please Enter the Start Date (DD/MM/YY): ");
            string start_time = Validation.Validate(Console.ReadLine(), "date");
            if (start_time == "MENU") { return; }
            
            Console.Write("Please Enter the End Date (DD/MM/YY): ");
            string end_time = Validation.Validate(Console.ReadLine(), "date");
            if (end_time == "MENU") { return; }

            SessionController.InsertRow(start_time, end_time);
        }

        public static void UpdateView(string selector)
        {
            ShowTable(selector);
            
            Console.WriteLine("\nUpdating a Record...  Type MENU to return.");

            Console.Write("Please Enter the ID of the entry to change: ");
            string entryId = Validation.Validate(Console.ReadLine(), "id");
            if (entryId == "MENU") { return; }

            Console.Write("Please Enter a start date (DD/MM/YY): ");
            string startTime = Validation.Validate(Console.ReadLine(), "date");
            if (startTime == "MENU") { return; }

            Console.Write("Please Enter a end date (DD/MM/YY): ");
            string endTime = Validation.Validate(Console.ReadLine(), "date");
            if (endTime == "MENU") { return; }

            SessionController.UpdateRow(entryId, startTime, endTime);
        }

        public static void DeleteView(string selector)
        {
            ShowTable(selector);

            Console.WriteLine("\nDeleting a Record...  Type MENU to return.");
            Console.Write("Enter ID for entry to delete: ");
            string entryId = Validation.Validate(Console.ReadLine(), "id");
            if (entryId == "MENU") { return; }

            SessionController.DeleteRow(entryId);
        }

    }
}
