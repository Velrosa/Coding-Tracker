using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Coding_Tracker
{
    internal class Validation
    {
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
                    Console.Write(" Invalid entry, Please enter a number: ");
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
                    else if (entry == "NOW")
                    {
                        DateTime today = DateTime.Now;
                        return today.ToString();
                    }
                    else if (DateTime.TryParse(entry, out DateTime date))
                    {
                        return date.ToString();
                    }
                    else
                    {
                        Console.Write(" Invalid date, Please enter again (DD/MM/YY HH:MM:SS): ");
                        entry = Console.ReadLine();
                    }
                }            
            }
            return entry;
        }

        public static string Duration(string StartTime, string EndTime)
        {
            DateTime start = DateTime.Parse(StartTime);
            DateTime finish = DateTime.Parse(EndTime);

            if (start > finish)
            {
                return "INVALID";
            }
            else { return (finish - start).ToString(); }
        }
    }
}
