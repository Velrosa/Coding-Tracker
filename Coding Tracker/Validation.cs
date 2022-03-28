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
                    Console.Write("Invalid entry, Please enter a number: ");
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
                    else if (!Regex.IsMatch(entry, @"^\d{1,2}/\d{1,2}/\d{2,4}$"))
                    {
                        Console.Write("Invalid date, Please enter again (DD/MM/YY): ");
                        entry = Console.ReadLine();
                    }
                    else break;

                }
            }
            return entry;
        }
    }
}
