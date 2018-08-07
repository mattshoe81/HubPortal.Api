using HubPortal.QueryGenerator.ContextFreeGrammar;
using HubPortal.QueryGenerator.Exceptions;
using System;

namespace HubPortal.QueryGenerator.Extensions {

    internal static class StringExtensions {

        /// <summary>
        /// Appends a '<code>Refinement</code>' to <code>this</code>. See 'ContextFreeGrammar.txt' for the definition of '<code>Refinement</code>'.
        /// </summary>
        /// <param name="original">this</param>
        /// <param name="property">name of the property</param>
        /// <param name="value">value of the property</param>
        /// <returns>The original query string appended with a refinement</returns>
        public static string RefineQuery(this string original, string property, string value) {
            return original + $" {{ {property} : '{value}' }}";
        }

        /// <summary>
        /// Returns a formatted query defined by the context free grammar, where <code>this</code> is the search type.
        /// <code>this</code> MUST be the search type.
        /// </summary>
        /// <param name="searchTypeOrNameList"></param>
        /// <returns>A formatted string according to the context free grammar where <code>#this</code> is the SearchType</returns>
        internal static string GetCfgQuery(this string searchTypeOrNameList) {
            if (!searchTypeOrNameList.IsValidTransactionSearch() && !searchTypeOrNameList.IsValidNameList()) throw new QuerySyntaxException(searchTypeOrNameList, "valid TransactionSearch or StringList");
            return $"FIND {searchTypeOrNameList}";
        }

        /// <summary>
        /// Appends the given '<code>Lookup</code>' to <code>this</code>. See 'ContextFreeGrammar.txt' for the definition of '<code>Lookup</code>'.
        /// </summary>
        /// <param name="original">this</param>
        /// <param name="property">name of the property</param>
        /// <param name="value">value of the property</param>
        /// <returns>The original query string appended with a refinement</returns>
        public static string AddLookup(this string original, string lookup) {
            return original + $" AND {lookup}";
        }

        internal static bool IsValidSearchType(this string token) {
            return token.IsValidTransactionSearch() || token.IsValidNameList();
        }

        internal static bool IsValidTransactionSearch(this string token) {
            return token == Symbols.PROCESS || token == Symbols.CLIENT || token == Symbols.SOURCE;
        }

        internal static bool IsValidNameList(this string token) {
            return token == Symbols.PROCESS_NAMES || token == Symbols.CLIENT_NAMES || token == Symbols.TRANSACTION_TYPES;
        }

        public static bool IsValidValue(this string token) {
            char[] validChars = { '&', '-', ' ', '.' };
            foreach (char character in token) {
                bool isLetterOrDigit = Char.IsLetterOrDigit(character);
                bool isValidSpecialChar = Array.IndexOf(validChars, character) > -1;
                if (!isLetterOrDigit && !isValidSpecialChar) {
                    return false;
                }
            }
                

            return true;
        }

        public static bool IsValidLookup(this string token) {
            return token == Symbols.COVERAGE
                || token == Symbols.CREDIT_CARD
                || token == Symbols.WHOLESALE
                || token == Symbols.SHOP
                || token == Symbols.GENERIC;
        }

        public static bool IsValidProperty(this string token) {
            return token == Symbols.TRANSACTION_TYPE
                    || token == Symbols.DESTINATION
                    || token == Symbols.START_TIME
                    || token == Symbols.END_TIME
                    || token == Symbols.MIN_TIME
                    || token == Symbols.MAX_TIME
                    || token == Symbols.PING_OPTION
                    || token == Symbols.FAILED
                    || token == Symbols.SERVER_NAME
                    || token == Symbols.SESSION_ID
                    || token == Symbols.IGNORE
                    || token == Symbols.POLICY_NUMBER
                    || token == Symbols.REFERRAL_NUMBER
                    || token == Symbols.CSR
                    || token == Symbols.REFERRAL_DATE
                    || token == Symbols.ZIP_CODE
                    || token == Symbols.PROMO_CODE
                    || token == Symbols.CREDIT_CARD_NUMBER
                    || token == Symbols.CTU
                    || token == Symbols.AUTHORIZATION_CODE
                    || token == Symbols.ACCOUNT_NUMBER
                    || token == Symbols.ORDER_ID
                    || token == Symbols.INVOICE_NUMBER
                    || token == Symbols.GENERIC_SEARCH_STRING
                    || token == Symbols.INCLUDE_GENERIC_STRING
                    || token == Symbols.CLAIM_NUMBER
                    || token == Symbols.FNOL_NUMBER
                    || token == Symbols.SUB_COMPANY
                    || token == Symbols.PART_NUMBER
                    || token == Symbols.CAR_ID
                    || token == Symbols.AMOUNT
                    || token == Symbols.WORK_ORDER_NUMBER
                    || token == Symbols.WORK_ORDER_ID
                    || token == Symbols.WAREHOUSE_NUMBER
                    || token == Symbols.SHOP_NUMBER
                    || token == Symbols.CHECKPOINT
                    || token == Symbols.PROCESS_NAME
                    || token == Symbols.CLIENT_NAME
                    || token == Symbols.SOURCE_NAME;
        }
    }
}