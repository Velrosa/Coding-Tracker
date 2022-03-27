using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Collections.Specialized;
using ConsoleTableExt;

namespace Coding_Tracker
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string sAttr = ConfigurationManager.AppSettings.Get("conString");


            var tableData = new List<List<object>>
            {
                new List<object>{ "Sakura Yamamoto", "Support Engineer", "London", 46},
                new List<object>{ "Serge Baldwin", "Data Coordinator", "San Francisco", 28, "something else" },
                new List<object>{ "Shad Decker", "Regional Director", "Edinburgh"},
            };

            ConsoleTableBuilder.From(tableData).ExportAndWriteLine();

            Console.ReadLine();
        }
    }
}
