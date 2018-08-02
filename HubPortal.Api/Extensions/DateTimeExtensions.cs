using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace HubPortal.Api.Extensions
{
    public static class DateTimeExtensions {

        public static string ToOracleTimeStamp(this DateTime date) {
            string day = date.Day.ToString().PadLeft(2, '0');
            string month = date.ToString("MMM");
            string year = date.ToString("yy");
            string hour = date.ToString("hh");
            string minute = date.Minute.ToString().PadLeft(2, '0');
            string second = date.Second.ToString().PadLeft(2, '0');
            string milli = date.Millisecond.ToString().PadRight(9, '0');
            string amPm = date.ToString("tt", CultureInfo.InvariantCulture);

            return $"{day}-{month}-{year} {hour}.{minute}.{second}.{milli} {amPm}";
        }

        public static string ToOracleDate(this DateTime date) {
            string day = date.Day.ToString().PadLeft(2, '0');
            string month = date.ToString("MMM");
            string year = date.ToString("yy");

            return $"{day}-{month}-{year}";
        }

        public static DateTime FromOracleTimeStamp(this DateTime date, string timestamp) {
            string format = String.Format("yy-MMM-dd hh.mm.ss.fffffff{0} tt", timestamp.Substring(26, 2));

            return DateTime.ParseExact(timestamp.ToUpper(), format, CultureInfo.InvariantCulture);
        }

        public static DateTime FromOracleDate(this DateTime dateTime, string date) {
            return DateTime.ParseExact(date, "dd-MMM-yy", CultureInfo.InvariantCulture);
        }

        public static string ToJson(this DateTime dateTime) {
            return dateTime.ToString("yyyy-MM-dd'T'HH:mm:ss.fff'Z'");
        }



    }
}
