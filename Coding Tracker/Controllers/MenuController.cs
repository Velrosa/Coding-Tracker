using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coding_Tracker
{
    internal class MenuController
    {
        public static void DisplayMenu()
        {
            string selector = MenuView.MainMenu();

            switch (selector)
            {
                case "0":
                    Environment.Exit(0);
                    break;
                case "1":
                    SessionController.GetTable(selector);
                    break;
                case "2":
                    Console.Clear();
                    //InsertRecord();
                    break;
                case "3":
                    Console.Clear();
                    //ViewRecords(selector);
                    //DeleteRecord();
                    break;
                case "4":
                    Console.Clear();
                    //ViewRecords(selector);
                    //UpdateRecord();
                    break;
                default:
                    Console.Write("Invalid Entry. press key to return... ");
                    Console.ReadKey();
                    break;

            }
        }
    }
}
