using System;
using System.Collections.Generic;
using System.Text;

namespace HubPortal.QueryGenerator.Extensions
{
    public static class StringExtensions {

        /// <summary>
        /// Appends a '<code>Refinement</code>' to <code>this</code>. See ContextFreeGrammar.txt for the definition of '<code>Refinement</code>'.
        /// </summary>
        /// <param name="original">this</param>
        /// <param name="property">name of the property</param>
        /// <param name="value">value of the property</param>
        /// <returns></returns>
        public static string RefineQuery(this string original, string property, string value) {
            return original += $" {{ {property} : '{value}' }}";
        }

        /// <summary>
        /// Returns a formatted query defined by the context free grammar, where <code>this</code> is the search type.
        /// <code>this</code> MUST be the search type.
        /// </summary>
        /// <param name="searchType"></param>
        /// <returns></returns>
        public static string GetQuery(this string searchType) {
            return $"FIND {searchType} ";
        }


        internal static bool IsValidSearchType(this string token) {
            return token == "process" || token == "client" || token == "source";
        }

        public static bool IsValidValue(this string token) {
            foreach (char character in token) 
                if (!Char.IsLetterOrDigit(character) && character != '&' && character != '-') return false;

            return true;
        }

        public static bool IsValidProperty(this string token) {
            return token == "transactionType"
                    || token == "startDate"
                    || token == "endDate"
                    || token == "startTime"
                    || token == "endTime"
                    || token == "minTime"
                    || token == "maxTime"
                    || token == "pingOptions"
                    || token == "failed"
                    || token == "serverName"
                    || token == "sessionId"
                    || token == "ignore"
                    || token == "policyNumber"
                    || token == "referralNumber"
                    || token == "csr"
                    || token == "referralDate"
                    || token == "zipCode"
                    || token == "promoCode"
                    || token == "creditCardNumber"
                    || token == "ctu"
                    || token == "authorizationCode"
                    || token == "accountNumber"
                    || token == "orderID"
                    || token == "invoiceNumber"
                    || token == "genericSearchString"
                    || token == "includeGenericStringInTransaction"
                    || token == "claimNumber"
                    || token == "fnolNumber"
                    || token == "subcompany"
                    || token == "partNumber"
                    || token == "carID"
                    || token == "amount"
                    || token == "workOrderNumber"
                    || token == "workOrderID"
                    || token == "warehouseNumber"
                    || token == "shopNumber"
                    || token == "checkpoint"
                    || token == "lookupType"
                    || token == "processName"
                    || token == "clientName";

        }

    }
}
