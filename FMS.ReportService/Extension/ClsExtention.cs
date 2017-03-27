using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMS.ReportService
{ 
    public static class ClsExtention
    {
        public static DateTime BeginingOfWeek(this DateTime dt, DayOfWeek startOfWeek)
        {
            int diff = dt.DayOfWeek - startOfWeek;
            if (diff < 0)
            {
                diff += 7;
            }
            return dt.AddDays(-1 * diff).Date;
        }
        public static DateTime BeginingOfYear(this DateTime dt)
        { 
            return new DateTime(dt.Year, 01, 01);
        }
        public static DateTime BeginingOftheDay(this DateTime dt)
        {
            return new DateTime(dt.Year, dt.Month, dt.Day);
        }
    }  
}
