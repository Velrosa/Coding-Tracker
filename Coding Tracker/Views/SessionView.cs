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

            Console.WriteLine("\nAdding a Coding session...   \nType MENU to return, Type NOW for the current time.");
            
            Console.Write("\nPlease Enter the Start Date (DD/MM/YY HH:MM): ");
            session.StartTime = Validation.Validate(Console.ReadLine(), "date");
            if (session.StartTime == "MENU") { return; }
            
            Console.Write("\nPlease Enter the End Date (DD/MM/YY HH:MM): ");
            session.EndTime = Validation.Validate(Console.ReadLine(), "date");
            if (session.EndTime == "MENU") { return; }

            session.Duration = Validation.Duration(session.StartTime, session.EndTime);
            if (session.Duration == "INVALID")
            {
                Console.WriteLine("\nInvalid time duration between entrys, Press any key to return to the MENU.");
                Console.ReadKey();
                return;
            }

            SessionController.InsertRow(session);
        }

        public static void UpdateView(string selector)
        {
            Session session = new Session();
            
            ShowTable(selector);
            
            Console.WriteLine("\nUpdating a Coding session...  \nType MENU to return.");

            Console.Write("\nPlease Enter the ID of the entry to change: ");
            string entryId = Validation.Validate(Console.ReadLine(), "id");
            if (entryId == "MENU") { return; } else { session.Id = Convert.ToInt32(entryId); }

            Console.Write("\nPlease Enter a start date (DD/MM/YY HH:MM): ");
            session.StartTime = Validation.Validate(Console.ReadLine(), "date");
            if (session.StartTime == "MENU") { return; }

            Console.Write("\nPlease Enter a end date (DD/MM/YY HH:MM): ");
            session.EndTime = Validation.Validate(Console.ReadLine(), "date");
            if (session.EndTime == "MENU") { return; }

            session.Duration = Validation.Duration(session.StartTime, session.EndTime);
            if (session.Duration == "INVALID")
            {
                Console.WriteLine("\nInvalid time duration between entrys, Press any key to return to the MENU.");
                Console.ReadKey();
            }

            SessionController.UpdateRow(session);
        }

        public static void DeleteView(string selector)
        {
            Session session = new Session();

            ShowTable(selector);

            Console.WriteLine("\nDeleting a Coding session...  \nType MENU to return.");
            Console.Write("\nEnter ID for entry to delete: ");
            string entryId = Validation.Validate(Console.ReadLine(), "id");
            if (entryId == "MENU") { return; } else { session.Id = Convert.ToInt32(entryId); }

            SessionController.DeleteRow(session);
        }

        public static void OpenSession(string selector)
        {
            Session session = new Session();

            Console.WriteLine("\nOpening Session, \nAny Key to continue...");
            Console.ReadKey();
            
            session.StartTime = (DateTime.Now).ToString();
            session.EndTime = "Session Open.";
            session.Duration = "TBC";
            session.Active(true);

            SessionController.InsertRow(session);
        }

        public static void CloseSession(Session session)
        {
            Console.WriteLine("\nClosing Session, \nAny Key to continue...");
            Console.ReadKey();

            session.EndTime = (DateTime.Now).ToString();
            session.Duration = Validation.Duration(session.StartTime, session.EndTime);

            SessionController.UpdateRow(session);

            ShowTable("1");
        }
    }
}
