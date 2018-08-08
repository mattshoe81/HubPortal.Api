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

        #region Private Fields

        private const string INCLUDE_ALL = "All";

        #endregion Private Fields

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

            return TransactionLookupEngine.GetTransactions(query);
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
            if (searchData.ReferralDate != null) query.Refine(Symbols.REFERRAL_DATE, searchData.ReferralDate.ToOracleTimeStamp());
            if (!String.IsNullOrEmpty(searchData.PartNumber)) query.Refine(Symbols.PART_NUMBER, searchData.PartNumber);
            if (!String.IsNullOrEmpty(searchData.ZipCode)) query.Refine(Symbols.ZIP_CODE, searchData.ZipCode);
            if (!String.IsNullOrEmpty(searchData.CarID)) query.Refine(Symbols.CAR_ID, searchData.CarID);
            if (!String.IsNullOrEmpty(searchData.PromoCode)) query.Refine(Symbols.PROMO_CODE, searchData.PromoCode);
        }

        private static void ParseCreditCard(IQuery query, TransactionLookupData searchData) {
            if (searchData.CreditCardNumber != null) query.Refine(Symbols.CREDIT_CARD_NUMBER, searchData.CreditCardNumber?.ToString());
            if (searchData.Amount != null) query.Refine(Symbols.AMOUNT, searchData.Amount?.ToString());
            if (!String.IsNullOrEmpty(searchData.CTU)) query.Refine(Symbols.CTU, searchData.CTU);
            if (!String.IsNullOrEmpty(searchData.WorkOrderNumber)) query.Refine(Symbols.WORK_ORDER_NUMBER, searchData.WorkOrderNumber);
            if (!String.IsNullOrEmpty(searchData.AuthorizationCode)) query.Refine(Symbols.AUTHORIZATION_CODE, searchData.AuthorizationCode);
            if (!String.IsNullOrEmpty(searchData.WorkOrderID)) query.Refine(Symbols.WORK_ORDER_ID, searchData.WorkOrderID);
        }

        private static void ParseGeneric(IQuery query, TransactionLookupData searchData) {
            if (!String.IsNullOrEmpty(searchData.GenericSearchString)) query.Refine(Symbols.GENERIC_SEARCH_STRING, searchData.GenericSearchString);
            if (searchData.Checkpoint != INCLUDE_ALL) query.Refine(Symbols.CHECKPOINT, searchData.Checkpoint);
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
                    if (searchData.Process != INCLUDE_ALL) query.Refine(Symbols.PROCESS_NAME, searchData.Process);
                    break;

                case Symbols.CLIENT:
                    if (searchData.Client != INCLUDE_ALL) query.Refine(Symbols.CLIENT_NAME, searchData.Client);
                    break;

                case Symbols.SOURCE:
                    if (searchData.Source != INCLUDE_ALL) query.Refine(Symbols.SOURCE, searchData.Source);
                    if (searchData.Destination != INCLUDE_ALL) query.Refine(Symbols.DESTINATION, searchData.Destination);
                    if (searchData.TransactionType != INCLUDE_ALL) query.Refine(Symbols.TRANSACTION_TYPE, searchData.TransactionType);
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
            if (searchData.StartTime != null) query.Refine(Symbols.START_TIME, searchData.StartTime.ToOracleTimeStamp());
            if (searchData.EndTime != null) query.Refine(Symbols.END_TIME, searchData.EndTime.ToOracleTimeStamp());
            if (searchData.MinTime != null) query.Refine(Symbols.MIN_TIME, searchData.MinTime?.ToString());
            if (searchData.MaxTime != null) query.Refine(Symbols.MAX_TIME, searchData.MaxTime?.ToString());
            if (searchData.PingOptions != INCLUDE_ALL) query.Refine(Symbols.PING_OPTION, searchData.PingOptions);
            if (searchData.Failed != INCLUDE_ALL) query.Refine(Symbols.FAILED, searchData.Failed);
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