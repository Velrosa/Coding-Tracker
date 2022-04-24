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
        // Displays a table with all the current records in.
        public static void ShowTable(string selector)
        {
            Console.Clear();

            Console.WriteLine("\n Displaying all session records:\n");
            
            ConsoleTableBuilder.From(SessionController.GetTable()).ExportAndWriteLine();

            if (selector == "1")
            {
                Console.Write("\n Press any key to return to menu... ");
                Console.ReadKey();
            }
        }

        // Interface for inserting a record into the database.
        public static void InsertView(string selector)
        {
            Session session = new Session();

            Console.WriteLine("\n Adding a Coding session...   \n Type MENU to return, Type NOW for the current time.");
            
            Console.Write("\n Please Enter the Start Date (DD/MM/YY HH:MM): ");
            session.StartTime = Console.ReadLine();
            if (session.StartTime == "NOW")
            {
                session.StartTime = DateTime.Now.ToString();
            }
            while (!Validation.IsDateValid(session.StartTime))
            {
                session.StartTime = Console.ReadLine();
            }
            if (session.StartTime == "MENU") return;
            
            Console.Write("\n Please Enter the End Date (DD/MM/YY HH:MM): ");
            session.EndTime = Console.ReadLine();
            if (session.EndTime == "NOW")
            {
                session.EndTime = DateTime.Now.ToString();
            }
            while (!Validation.IsDateValid(session.EndTime))
            {
                session.EndTime = Console.ReadLine();
            }
            if (session.EndTime == "MENU") return;

            session.Duration = Validation.IsDurationValid(session.StartTime, session.EndTime);
            if (session.Duration == null)
            {
                Console.WriteLine("\n Invalid time duration between entrys, Press any key to return to the MENU.");
                Console.ReadKey();
                return;
            }

            SessionController.InsertRow(session);
        }
        
        // Interface for updating a record in the database.
        public static void UpdateView(string selector)
        {
            Session session = new Session();
            
            ShowTable(selector);
            
            Console.WriteLine("\n Updating a Coding session...  \n Type MENU to return. Type NOW for the current time.");

            Console.Write("\n Please Enter the ID of the entry to change: ");
            string entryId = Console.ReadLine();
            while (!Validation.IsNumberValid(entryId))
            {
                entryId = Console.ReadLine();
            }
            if (entryId == "MENU") return;
            else { session.Id = Convert.ToInt32(entryId); }

            Console.Write("\n Please Enter a start date (DD/MM/YY HH:MM): ");
            session.StartTime = Console.ReadLine();
            if (session.StartTime == "NOW")
            {
                session.StartTime = DateTime.Now.ToString();
            }
            while (!Validation.IsDateValid(session.StartTime))
            {
                session.StartTime = Console.ReadLine();
            }
            if (session.StartTime == "MENU") return;

            Console.Write("\n Please Enter a end date (DD/MM/YY HH:MM): ");
            session.EndTime = Console.ReadLine();
            if (session.EndTime == "NOW")
            {
                session.EndTime = DateTime.Now.ToString();
            }
            while (!Validation.IsDateValid(session.EndTime))
            {
                session.EndTime = Console.ReadLine();
            }
            if (session.EndTime == "MENU") return;

            session.Duration = Validation.IsDurationValid(session.StartTime, session.EndTime);
            if (session.Duration == null)
            {
                Console.WriteLine("\n Invalid time duration between entrys, Press any key to return to the MENU.");
                Console.ReadKey();
            }

            SessionController.UpdateRow(session);
        }

        // Interface for deleting a record in the database.
        public static void DeleteView(string selector)
        {
            Session session = new Session();

            ShowTable(selector);

            Console.WriteLine("\n Deleting a Coding session...  \n Type MENU to return.");
            Console.Write("\n Enter ID for entry to delete: ");
            string entryId = Console.ReadLine();
            while (!Validation.IsNumberValid(entryId))
            {
                entryId = Console.ReadLine();
            }
            if (entryId == "MENU") return;
            else { session.Id = Convert.ToInt32(entryId); }

            SessionController.DeleteRow(session);
        }

        // Opens an active coding session, give it the current time and session open state.
        public static void OpenSession(string selector)
        {
            Session session = new Session();

            Console.WriteLine("\n Opening Active Session... \n\n Any Key to continue...");
            Console.ReadKey();
            
            session.StartTime = (DateTime.Now).ToString();
            session.EndTime = "Session Open.";
            session.Duration = "TBC";

            SessionController.InsertRow(session);
        }

        // Updates the active coding session (if there is one) with its end time as the current time and Duration calculated.
        public static void CloseSession(Session session)
        {
            Console.WriteLine("\n Closing Active Session... \n\n Any Key to continue...");
            Console.ReadKey();

            session.EndTime = (DateTime.Now).ToString();
            session.Duration = Validation.IsDurationValid(session.StartTime, session.EndTime);

            SessionController.UpdateRow(session);

            ShowTable("1");
        }
    }
}
