using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ShiftTracker.Ui
{
    internal class Validator
    {
        internal static bool IsStringValid(string stringInput)
        {
            foreach (char c in stringInput)
            {
                if (!Char.IsLetter(c) && c != '/')
                    return false;
            }

            if (String.IsNullOrEmpty(stringInput))
            {
                return false;
            }

            return true;
        }

        internal static bool IsIdValid(string stringInput)
        {
            foreach (char c in stringInput)
            {
                if (!Char.IsDigit(c))
                    return false;
            }

            if (String.IsNullOrEmpty(stringInput))
            {
                return false;
            }

            return true;
        }

        internal static bool IsDateTimeValid(string stringInput)
        {
            if (String.IsNullOrEmpty(stringInput) || !DateTime.TryParse(stringInput, out _))
            {
                return false;
            }

            return true;
        }

        internal static bool IsEndDateValid(DateTime start, DateTime end)
        {   
            bool isValid = start < end ? true: false ;
            
            return isValid;
        }

        internal static bool IsMoneyValid(string pay)
        {
            if (String.IsNullOrEmpty(pay))
            {
                return false;
            }

            Regex rgx = new (@"^[0-9]{0,6}(\.[0-9]{1,2})?$");
            bool isValid = rgx.IsMatch(pay) ? true : false;

            return isValid;
        }
    }
}
