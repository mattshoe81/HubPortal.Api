using Oracle.ManagedDataAccess.Client;
using System;
using System.Globalization;

namespace HubPortal.Api.Extensions {

    public static class StringExtensions {

        public static string ToOracleTimeStamp(this string obj) {
            string str = obj;
            DateTime dateTime = (String.IsNullOrEmpty(str))
                ? DateTime.MinValue
                : DateTime.ParseExact(str, "yyyy-MM-dd'T'HH:mm:ss.fff'Z'", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal);

            return dateTime.ToOracleTimeStamp();
        }

        public static string ToOracleDate(this string obj) {
            string str = obj;
            DateTime dateTime = (String.IsNullOrEmpty(str))
                ? DateTime.MinValue
                : DateTime.ParseExact(str, "yyyy-MM-dd'T'HH:mm:ss.fff'Z'", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal);

            return dateTime.ToOracleDate();
        }

        public static string ToJson(this string str, OracleDbType type) {
            DateTime dateTime = new DateTime();
            string result = str;
            if (type.ToString() == "TimeStamp") {
                dateTime = dateTime.FromOracleTimeStamp(str);
            } else if (type.ToString() == "Date") {
                dateTime = dateTime.FromOracleDate(str);
            }

            return dateTime.ToJson();
        }
    }
}