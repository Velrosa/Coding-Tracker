using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coding_Tracker
{
    internal class MenuView
    {
        public static string MainMenu()
        {
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

            return Convert.ToString(Console.ReadKey(true).KeyChar);
        }
    }
}
