using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Coding_Tracker
{
    public class Validation
    {
        public static bool IsNumberValid(string numberInput)
        {
            if (numberInput == "MENU")
            {
                return true;
            }
            if (!Int32.TryParse(numberInput, out int number))
            {
                Console.WriteLine($"\n \"{numberInput}\" is not a valid number.");
                return false;
            }
            if (number < 0)
            {
                return false;
            }
            return true;
        }

        public static bool IsDateValid(string dateInput)
        {
            if (dateInput == "MENU")
            {
                return true;
            }
            if (!DateTime.TryParse(dateInput, out DateTime date))
            {
                Console.WriteLine($"\n \"{dateInput}\" is not a valid Date.");
                return false;
            }
            return true;

        }

        // Calculates the duration of a session.
        public static string IsDurationValid(string StartTime, string EndTime)
        {
            DateTime start = DateTime.Parse(StartTime);
            DateTime finish = DateTime.Parse(EndTime);

            if (start > finish)
            {
                return null;
            }
            else { return (finish - start).ToString(); }
        }
    }
}
