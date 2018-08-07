namespace HubPortal.QueryGenerator.ContextFreeGrammar {

    /// <summary>
    /// Contains all Constants to hold the string values for all symbols in the Context Free Grammar.
    /// </summary>
    public class Symbols {

        #region Constants

        // Transaction Search
        public const string PROCESS = "process";

        public const string CLIENT = "client";
        public const string SOURCE = "source";

        // StringList
        public const string PROCESS_NAMES = "processNames";

        public const string CLIENT_NAMES = "clientNames";
        public const string TRANSACTION_TYPES = "transactionTypes";

        // Lookup
        public const string COVERAGE = "coverage";

        public const string CREDIT_CARD = "creditCard";
        public const string WHOLESALE = "wholesale";
        public const string SHOP = "shop";
        public const string GENERIC = "generic";

        // Property
        public const string PROCESS_NAME = "processName";

        public const string CLIENT_NAME = "clientName";
        public const string SOURCE_NAME = "source";
        public const string DESTINATION = "destination";
        public const string TRANSACTION_TYPE = "transactionType";
        public const string START_TIME = "startTime";
        public const string END_TIME = "endTime";
        public const string MIN_TIME = "minTime";
        public const string MAX_TIME = "maxTime";
        public const string PING_OPTION = "pingOptions";
        public const string FAILED = "failed";
        public const string SERVER_NAME = "serverName";
        public const string SESSION_ID = "sessionID";
        public const string IGNORE = "ignore";
        public const string POLICY_NUMBER = "policyNumber";
        public const string REFERRAL_NUMBER = "referralNumber";
        public const string CSR = "csr";
        public const string REFERRAL_DATE = "referralDate";
        public const string ZIP_CODE = "zipCode";
        public const string PROMO_CODE = "promoCode";
        public const string CREDIT_CARD_NUMBER = "creditCardNumber";
        public const string CTU = "ctu";
        public const string AUTHORIZATION_CODE = "authorizationCode";
        public const string ACCOUNT_NUMBER = "accountNumber";
        public const string ORDER_ID = "orderID";
        public const string INVOICE_NUMBER = "invoiceNumber";
        public const string GENERIC_SEARCH_STRING = "genericSearchString";
        public const string INCLUDE_GENERIC_STRING = "includeGenericStringInTransaction";
        public const string CLAIM_NUMBER = "claimNumber";
        public const string FNOL_NUMBER = "fnolNumber";
        public const string SUB_COMPANY = "subcompany";
        public const string PART_NUMBER = "partNumber";
        public const string CAR_ID = "carID";
        public const string AMOUNT = "amount";
        public const string WORK_ORDER_NUMBER = "workOrderNumber";
        public const string WORK_ORDER_ID = "workOrderID";
        public const string WAREHOUSE_NUMBER = "warehouseNumber";
        public const string SHOP_NUMBER = "shopNumber";
        public const string CHECKPOINT = "checkpoint";
        public const string LOOKUP_TYPE = "lookupType";

        #endregion Constants
    }
}