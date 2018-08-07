using HubPortal.Api.Extensions;
using HubPortal.Api.Models;
using HubPortal.QueryGenerator.ContextFreeGrammar;
using System;
using System.Collections.Generic;

namespace HubPortal.Api.DataAccess {

    /// <summary>
    /// Parses the <see cref="HubPortal.Api.Models.TransactionLookupData"/> posted by the client, and
    /// returns the transactions that satisfy those criteria.
    /// </summary>
    public static class TransactionRequestParser {

        #region Public Methods

        /// <summary>
        /// Given the raw <see cref="HubPortal.Api.Models.TransactionLookupData"/> posted by the
        /// client, will return a list of transactions that satisfy those criteria.
        /// </summary>
        /// <param name="searchData">Raw data from client's post request</param>
        /// <returns>List of transactions</returns>
        public static IEnumerable<Transaction> GetTransactions(TransactionLookupData searchData) {
            if (searchData.SearchType == "" || searchData.SearchType == null)
                throw new Exception("Search Type not defined");

            IEnumerable<Transaction> transactions = new List<Transaction>();
            IQuery query = CFGBuilder.GetQuery(searchData.SearchType);

            ParseSearchType(query, searchData);
            ParseTransactionDetails(query, searchData);
            ParseLookup(query, searchData);
            transactions = TransactionLookupEngine.GetTransactions(query);

            return transactions;
        }

        #endregion Public Methods

        #region Private Methods

        private static void ParseCoverage(IQuery query, TransactionLookupData searchData) {
            if (!String.IsNullOrEmpty(searchData.PolicyNumber)) query.Refine(Symbols.POLICY_NUMBER, searchData.PolicyNumber);
            if (!String.IsNullOrEmpty(searchData.ClaimNumber)) query.Refine(Symbols.CLAIM_NUMBER, searchData.ClaimNumber);
            if (!String.IsNullOrEmpty(searchData.ReferralNumber)) query.Refine(Symbols.REFERRAL_NUMBER, searchData.ReferralNumber);
            if (!String.IsNullOrEmpty(searchData.FNOLNumber)) query.Refine(Symbols.FNOL_NUMBER, searchData.FNOLNumber);
            if (!String.IsNullOrEmpty(searchData.CSR)) query.Refine(Symbols.CSR, searchData.CSR);
            if (!String.IsNullOrEmpty(searchData.SubCompany)) query.Refine(Symbols.SUB_COMPANY, searchData.SubCompany);
            if (searchData.ReferralDate != null && searchData.ReferralDate != DateTime.MinValue) query.Refine(Symbols.REFERRAL_DATE, searchData.ReferralDate.ToOracleTimeStamp());
            if (!String.IsNullOrEmpty(searchData.PartNumber)) query.Refine(Symbols.PART_NUMBER, searchData.PartNumber);
            if (!String.IsNullOrEmpty(searchData.ZipCode)) query.Refine(Symbols.ZIP_CODE, searchData.ZipCode);
            if (!String.IsNullOrEmpty(searchData.CarID)) query.Refine(Symbols.CAR_ID, searchData.CarID);
            if (!String.IsNullOrEmpty(searchData.PromoCode)) query.Refine(Symbols.PROMO_CODE, searchData.PromoCode);
        }

        private static void ParseCreditCard(IQuery query, TransactionLookupData searchData) {
            if (!String.IsNullOrEmpty(searchData.CreditCardNumber?.ToString())) query.Refine(Symbols.CREDIT_CARD_NUMBER, searchData.CreditCardNumber?.ToString());
            if (!String.IsNullOrEmpty(searchData.Amount?.ToString())) query.Refine(Symbols.AMOUNT, searchData.Amount?.ToString());
            if (!String.IsNullOrEmpty(searchData.CTU)) query.Refine(Symbols.CTU, searchData.CTU);
            if (!String.IsNullOrEmpty(searchData.WorkOrderNumber)) query.Refine(Symbols.WORK_ORDER_NUMBER, searchData.WorkOrderNumber);
            if (!String.IsNullOrEmpty(searchData.AuthorizationCode)) query.Refine(Symbols.AUTHORIZATION_CODE, searchData.AuthorizationCode);
            if (!String.IsNullOrEmpty(searchData.WorkOrderID)) query.Refine(Symbols.WORK_ORDER_ID, searchData.WorkOrderID);
        }

        private static void ParseGeneric(IQuery query, TransactionLookupData searchData) {
            if (!String.IsNullOrEmpty(searchData.GenericSearchString)) query.Refine(Symbols.GENERIC_SEARCH_STRING, searchData.GenericSearchString);
            if (!String.IsNullOrEmpty(searchData.Checkpoint)) query.Refine(Symbols.CHECKPOINT, searchData.Checkpoint);
        }

        private static void ParseLookup(IQuery query, TransactionLookupData searchData) {
            query.AddLookup(searchData.LookupType);
            switch (searchData.LookupType) {
                case Symbols.COVERAGE:
                    ParseCoverage(query, searchData);
                    break;

                case Symbols.CREDIT_CARD:
                    ParseCreditCard(query, searchData);
                    break;

                case Symbols.WHOLESALE:
                    ParseWholesale(query, searchData);
                    break;

                case Symbols.SHOP:
                    ParseShop(query, searchData);
                    break;

                case Symbols.GENERIC:
                    ParseGeneric(query, searchData);
                    break;
            }
        }

        private static void ParseSearchType(IQuery query, TransactionLookupData searchData) {
            switch (searchData.SearchType) {
                case Symbols.PROCESS:
                    if (searchData.Process != "All") query.Refine(Symbols.PROCESS_NAME, searchData.Process);
                    break;

                case Symbols.CLIENT:
                    if (searchData.Client != "All") query.Refine(Symbols.CLIENT_NAME, searchData.Client);
                    break;

                case Symbols.SOURCE:
                    if (searchData.Source != "All") query.Refine(Symbols.SOURCE, searchData.Source);
                    if (searchData.Destination != "All") query.Refine(Symbols.DESTINATION, searchData.Destination);
                    if (searchData.TransactionType != "All") query.Refine(Symbols.TRANSACTION_TYPE, searchData.TransactionType);
                    break;

                default:
                    break;
            }
        }

        private static void ParseShop(IQuery query, TransactionLookupData searchData) {
            if (!String.IsNullOrEmpty(searchData.InvoiceNumber)) query.Refine(Symbols.INVOICE_NUMBER, searchData.InvoiceNumber);
            if (!String.IsNullOrEmpty(searchData.ShopNumber)) query.Refine(Symbols.SHOP_NUMBER, searchData.ShopNumber);
        }

        private static void ParseTransactionDetails(IQuery query, TransactionLookupData searchData) {
            query.Refine(Symbols.START_TIME, searchData.StartTime.ToOracleTimeStamp());
            query.Refine(Symbols.END_TIME, searchData.EndTime.ToOracleTimeStamp());
            if (!String.IsNullOrEmpty(searchData.MinTime?.ToString())) query.Refine(Symbols.MIN_TIME, searchData.MinTime?.ToString());
            if (!String.IsNullOrEmpty(searchData.MaxTime?.ToString())) query.Refine(Symbols.MAX_TIME, searchData.MaxTime?.ToString());
            if (!String.IsNullOrEmpty(searchData.PingOptions)) query.Refine(Symbols.PING_OPTION, searchData.PingOptions);
            if (!String.IsNullOrEmpty(searchData.Failed)) query.Refine(Symbols.FAILED, searchData.Failed);
            if (!String.IsNullOrEmpty(searchData.ServerName)) query.Refine(Symbols.SERVER_NAME, searchData.ServerName);
            if (!String.IsNullOrEmpty(searchData.SessionID)) query.Refine(Symbols.SESSION_ID, searchData.SessionID);
        }

        private static void ParseWholesale(IQuery query, TransactionLookupData searchData) {
            if (!String.IsNullOrEmpty(searchData.AccountNumber)) query.Refine(Symbols.ACCOUNT_NUMBER, searchData.AccountNumber);
            if (!String.IsNullOrEmpty(searchData.WarehouseNumber)) query.Refine(Symbols.WAREHOUSE_NUMBER, searchData.WarehouseNumber);
            if (!String.IsNullOrEmpty(searchData.OrderID)) query.Refine(Symbols.ORDER_ID, searchData.OrderID);
        }

        #endregion Private Methods
    }
}