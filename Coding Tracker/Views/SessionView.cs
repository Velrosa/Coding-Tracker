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
            Session session = new Session();

            Console.WriteLine("Inserting a Session Record...  Type MENU to return.");
            
            Console.Write("Please Enter the Start Date (DD/MM/YY): ");
            session.StartTime = Validation.Validate(Console.ReadLine(), "date");
            if (session.StartTime == "MENU") { return; }
            
            Console.Write("Please Enter the End Date (DD/MM/YY): ");
            session.EndTime = Validation.Validate(Console.ReadLine(), "date");
            if (session.EndTime == "MENU") { return; }

            SessionController.InsertRow(session);
        }

        public static void UpdateView(string selector)
        {
            Session session = new Session();
            
            ShowTable(selector);
            
            Console.WriteLine("\nUpdating a Record...  Type MENU to return.");

            Console.Write("Please Enter the ID of the entry to change: ");
            string entryId = Validation.Validate(Console.ReadLine(), "id");
            if (entryId == "MENU") { return; } else { session.Id = Convert.ToInt32(entryId); }

            Console.Write("Please Enter a start date (DD/MM/YY): ");
            session.StartTime = Validation.Validate(Console.ReadLine(), "date");
            if (session.StartTime == "MENU") { return; }

            Console.Write("Please Enter a end date (DD/MM/YY): ");
            session.EndTime = Validation.Validate(Console.ReadLine(), "date");
            if (session.EndTime == "MENU") { return; }

            SessionController.UpdateRow(session);
        }

        public static void DeleteView(string selector)
        {
            Session session = new Session();

            ShowTable(selector);

            Console.WriteLine("\nDeleting a Record...  Type MENU to return.");
            Console.Write("Enter ID for entry to delete: ");
            string entryId = Validation.Validate(Console.ReadLine(), "id");
            if (entryId == "MENU") { return; } else { session.Id = Convert.ToInt32(entryId); }

            SessionController.DeleteRow(session);
        }

    }
}
