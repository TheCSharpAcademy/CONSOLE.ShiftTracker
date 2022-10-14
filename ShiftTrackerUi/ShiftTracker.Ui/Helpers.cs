using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShiftTracker.Ui
{
    internal static class Helpers
    {
        internal static int CalculateDuration(DateTime start, DateTime end)
        {
            TimeSpan duration = end - start;
            double minutes = duration.TotalMinutes;
            
            return 0;
        }
    }
}
