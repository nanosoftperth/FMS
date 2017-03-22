using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

namespace FMS.ReportLogic
{
    public static class ExtentionModules
    {

        //[Extension()]
        //public static void PrintAndPunctuate(string aString, string punc)
        //{
        //    Console.WriteLine((aString + punc));
        //} 
     
        //public DateTime StartOfWeek(DateTime dt, DayOfWeek startDayOfWeek)
        //{
        //    int diff = (dt.DayOfWeek - startDayOfWeek);
        //    if ((diff < 0))
        //    {
        //        diff += 7;
        //    }
        //    return dt.AddDays(((1 * diff)
        //                    * -1)).Date;
        //} 
        public static DateTime timezoneToPerth(this DateTime d)
        {
            return d.AddHours(Business.SingletonAccess.ClientSelected_TimeZone.Offset_FromHQToPerth);
        } 

        
        //[Extension()]
        //public static DateTime timezoneToPerth(DateTime d, void Question) {
        //    DateTime thisd;
        //    if (d.HasValue) {
        //        thisd = d.Value.timezoneToPerth;
        //    }
         
        //    return thisd;
        //}

        //[Extension()]
        //public static DateTime timezoneToPerth(DateTime d) {
        //    return d.AddHours(Business.SingletonAccess.ClientSelected_TimeZone.Offset_FromHQToPerth);
        //}

        //[Extension()]
        //public static DateTime timezoneToClient(DateTime d, void Question) {
        //    DateTime thisd;
        //    if (d.HasValue) {
        //        thisd = d.Value.timezoneToClient;
        //    }

        //    return thisd;
        //}

        //[Extension()]
        //public static DateTime timezoneToClient(DateTime d) {
        //    return d.AddHours(Business.SingletonAccess.ClientSelected_TimeZone.Offset_FromPerthToHQ);
        //}
    }
}

