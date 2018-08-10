using HubPortal.QueryGenerator.ContextFreeGrammar;

namespace HubPortal.QueryGenerator.Extensions {

    internal static class StringExtensions {

        #region Internal Methods

        internal static bool IsValidItem(this string token) {
            return token == Symbols.PROCESS
                || token == Symbols.CHECKPOINT
                || token == Symbols.TRANSACTION
                || token == Symbols.TRANSACTION_TYPE
                || token == Symbols.CLIENT;
        }

        internal static bool IsValidProperty(this string token) {
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

        internal static bool IsValidSearchType(this string token) {
            return token == Symbols.TRANSACTION
                || token == Symbols.OUTAGE
                || token == Symbols.SUCCESS
                || token == Symbols.PROCESS
                || token == Symbols.CHECKPOINT
                || token.IsValidStringList();
        }

        internal static bool IsValidStringList(this string token) {
            return token == Symbols.PROCESS_LIST
                || token == Symbols.CLIENT_LIST
                || token == Symbols.TRANSACTION_TYPE_LIST;
        }

        #endregion Internal Methods
    }
}