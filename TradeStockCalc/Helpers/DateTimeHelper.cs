using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeStockCalc.Helpers
{
    public static class DateTimeHelper
    {

        /// <summary>
        /// Calculates the date difference in years assuming 1 year = 365 days.
        /// </summary>
        /// <param name="current">current date to start from</param>
        /// <param name="next">the date to whcih calculate the difference</param>
        /// <returns></returns>
        public static double GetYearsDiff(this DateTime current, DateTime next)
        {
            if (next <= current)
                throw new ArgumentException("Next date must be greater than current.");

            int totalDays = (next - current).Days;
            
            return totalDays / 365;
        }

        public static int DaysInYear(this DateTime current)
        {
            return DateTime.IsLeapYear(current.Year) ? 366 : 365;
        }
    }
}
