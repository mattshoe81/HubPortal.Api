using System;
using System.Collections.Generic;

using HubPortal.Data;
using HubPortal.Data.Models;
using HubPortal.QueryGenerator.ContextFreeGrammar;

namespace HubPortal.Api.Utilities {

    /// <summary>
    /// Parses the <see cref="HubPortal.Api.Models.TransactionLookupData"/> posted by the client, and
    /// returns the transactions that satisfy those criteria.
    /// </summary>
    public static class TransactionLookupRequestParser {
        private const string INCLUDE_ALL = "All";

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
            IQuery query = QueryBuilder.GetQuery(Symbols.FINDALL, Symbols.TRANSACTION);

            ParseSearchType(query, searchData);
            ParseTransactionDetails(query, searchData);
            ParseLookup(query, searchData);

            return TransactionEngine.GetTransactions(query);
        }

        private static bool IsUsable(string value) {
            return !String.IsNullOrEmpty(value) && value != TransactionLookupRequestParser.INCLUDE_ALL;
        }

        private static bool IsUsable(int? value) {
            return value != null;
        }

        private static bool IsUsable(DateTime? value) {
            return value != null;
        }

        private static void ParseCoverage(IQuery query, TransactionLookupData searchData) {
            ParseRefinement(query, Symbols.POLICY_NUMBER, searchData.PolicyNumber);
            ParseRefinement(query, Symbols.CLAIM_NUMBER, searchData.ClaimNumber);
            ParseRefinement(query, Symbols.REFERRAL_NUMBER, searchData.ReferralNumber);
            ParseRefinement(query, Symbols.FNOL_NUMBER, searchData.FNOLNumber);
            ParseRefinement(query, Symbols.CSR, searchData.CSR);
            ParseRefinement(query, Symbols.SUB_COMPANY, searchData.SubCompany);
            ParseRefinement(query, Symbols.REFERRAL_DATE, searchData.ReferralDate);
            ParseRefinement(query, Symbols.PART_NUMBER, searchData.PartNumber);
            ParseRefinement(query, Symbols.ZIP_CODE, searchData.ZipCode);
            ParseRefinement(query, Symbols.CAR_ID, searchData.CarID);
            ParseRefinement(query, Symbols.PROMO_CODE, searchData.PromoCode);
        }

        private static void ParseCreditCard(IQuery query, TransactionLookupData searchData) {
            ParseRefinement(query, Symbols.CREDIT_CARD_NUMBER, searchData.CreditCardNumber);
            ParseRefinement(query, Symbols.AMOUNT, searchData.Amount);
            ParseRefinement(query, Symbols.CTU, searchData.CTU);
            ParseRefinement(query, Symbols.WORK_ORDER_NUMBER, searchData.WorkOrderNumber);
            ParseRefinement(query, Symbols.AUTHORIZATION_CODE, searchData.AuthorizationCode);
            ParseRefinement(query, Symbols.WORK_ORDER_ID, searchData.WorkOrderID);
        }

        private static void ParseGeneric(IQuery query, TransactionLookupData searchData) {
            ParseRefinement(query, Symbols.GENERIC_SEARCH_STRING, searchData.GenericSearchString);
            ParseRefinement(query, Symbols.CHECKPOINT, searchData.Checkpoint);
        }

        private static void ParseLookup(IQuery query, TransactionLookupData searchData) {
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

        private static void ParseRefinement(IQuery query, string property, int? value) {
            if (IsUsable(value)) query.Refine(property, value.ToString());
        }

        private static void ParseRefinement(IQuery query, string property, DateTime? value) {
            if (IsUsable(value)) query.Refine(property, value.ToOracleTimeStamp());
        }

        private static void ParseRefinement(IQuery query, string property, string value) {
            if (IsUsable(value)) query.Refine(property, value);
        }

        private static void ParseSearchType(IQuery query, TransactionLookupData searchData) {
            switch (searchData.SearchType) {
                case Symbols.PROCESS:
                    ParseRefinement(query, Symbols.PROCESS_NAME, searchData.Process);
                    break;

                case Symbols.CLIENT:
                    ParseRefinement(query, Symbols.CLIENT_NAME, searchData.Client);
                    break;

                case Symbols.SOURCE:
                    ParseRefinement(query, Symbols.SOURCE, searchData.Source);
                    ParseRefinement(query, Symbols.DESTINATION, searchData.Destination);
                    ParseRefinement(query, Symbols.TRANSACTION_TYPE, searchData.TransactionType);
                    break;

                default:
                    break;
            }
        }

        private static void ParseShop(IQuery query, TransactionLookupData searchData) {
            ParseRefinement(query, Symbols.INVOICE_NUMBER, searchData.InvoiceNumber);
            ParseRefinement(query, Symbols.SHOP_NUMBER, searchData.ShopNumber);
        }

        private static void ParseTransactionDetails(IQuery query, TransactionLookupData searchData) {
            ParseRefinement(query, Symbols.START_TIME, searchData.StartTime);
            ParseRefinement(query, Symbols.END_TIME, searchData.EndTime);
            ParseRefinement(query, Symbols.MIN_TIME, searchData.MinTime);
            ParseRefinement(query, Symbols.MAX_TIME, searchData.MaxTime);
            ParseRefinement(query, Symbols.PING_OPTION, searchData.PingOptions);
            ParseRefinement(query, Symbols.FAILED, searchData.Failed);
            ParseRefinement(query, Symbols.SERVER_NAME, searchData.ServerName);
            ParseRefinement(query, Symbols.SESSION_ID, searchData.SessionID);
        }

        private static void ParseWholesale(IQuery query, TransactionLookupData searchData) {
            ParseRefinement(query, Symbols.ACCOUNT_NUMBER, searchData.AccountNumber);
            ParseRefinement(query, Symbols.WAREHOUSE_NUMBER, searchData.WarehouseNumber);
            ParseRefinement(query, Symbols.ORDER_ID, searchData.OrderID);
        }
    }
}
