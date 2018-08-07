using System;
using System.Globalization;

namespace HubPortal.Api.Extensions {

    public static class DateTimeExtensions {

        #region Public Methods

        /// <summary>
        /// Given an Oracle Date string, generates an equivalent DateTime instance.
        /// </summary>
        /// <param name="date"></param>
        /// <param name="date">Oracle Date string</param>
        /// <returns>DateTime instance equivalent to the given Oracle Date</returns>
        public static DateTime FromOracleDate(this DateTime dateTime, string date) {
            return DateTime.ParseExact(date, "dd-MMM-yy", CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Given an Oracle Timestamp string, generates an equivalent DateTime instance.
        /// </summary>
        /// <param name="date"></param>
        /// <param name="timestamp">Oracle Timestamp string</param>
        /// <returns>DateTime instance equivalent to the given Oracle Timestamp</returns>
        public static DateTime FromOracleTimeStamp(this DateTime date, string timestamp) {
            string format = String.Format("yy-MMM-dd hh.mm.ss.fffffff{0} tt", timestamp.Substring(26, 2));

            return DateTime.ParseExact(timestamp.ToUpper(), format, CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Converts this into a string complying with the format of an Oracle Date.
        /// </summary>
        /// <param name="date">this</param>
        /// <returns>string formatted to comply with Oracle Date format</returns>
        public static string ToOracleDate(this DateTime date) {
            string day = date.Day.ToString().PadLeft(2, '0');
            string month = date.ToString("MMM");
            string year = date.ToString("yy");

            return $"{day}-{month}-{year}";
        }

        /// <summary>
        /// Converts this into a string complying with the format of an Oracle TimeStamp.
        /// </summary>
        /// <param name="date">this</param>
        /// <returns>string formatted to comply with Oracle Timestamp format</returns>
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

        #endregion Public Methods
    }
}