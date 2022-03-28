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
            Console.Clear();
            
            Console.WriteLine("\nMAIN MENU\n\n" +
                                "What would you like to do?\n\n" +
                                "Type 0 to Close Application.\n" +
                                "Type 1 to View All Records.\n" +
                                "Type 2 to Insert Record.\n" +
                                "Type 3 to Delete Record.\n" +
                                "Type 4 to Update Record.\n");

            return Convert.ToString(Console.ReadKey(true).KeyChar);
        }
    }
}
