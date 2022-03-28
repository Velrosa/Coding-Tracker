using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleTableExt;

namespace Coding_Tracker
{
    internal class TableView
    {
        public static void ShowTable(List<Session> tableData, string selector)
        {
            Console.Clear();

            Console.WriteLine("\nDisplaying all session records:\n");
            
            ConsoleTableBuilder.From(tableData).ExportAndWriteLine();

            if (selector == "1")
            {
                Console.Write("\nPress any key to return to menu... ");
                Console.ReadKey();
            }
        }

    }
}
