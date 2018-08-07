using HubPortal.Api.Extensions;
using HubPortal.Api.Models;
using HubPortal.QueryGenerator.ContextFreeGrammar;
using System;
using System.Collections.Generic;

namespace HubPortal.Api.DataAccess {

    public class TransactionRequestParser {
        public TransactionLookupData SearchData { get; set; }

        public TransactionRequestParser() {
        }

        public TransactionRequestParser(TransactionLookupData data) {
            this.SearchData = data;
        }

        public static IEnumerable<Transaction> GetTransactions(TransactionLookupData searchData) {
            if (searchData.SearchType == "" || searchData.SearchType == null) throw new Exception("Search Type not defined");
            IEnumerable<Transaction> transactions = new List<Transaction>();
            IQuery query = CFGBuilder.GetQuery(searchData.SearchType);

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

            query.Refine(Symbols.START_TIME, searchData.StartTime.ToOracleTimeStamp());
            query.Refine(Symbols.END_TIME, searchData.EndTime.ToOracleTimeStamp());
            if (!String.IsNullOrEmpty(searchData.MinTime?.ToString())) query.Refine(Symbols.MIN_TIME, searchData.MinTime?.ToString());
            if (!String.IsNullOrEmpty(searchData.MaxTime?.ToString())) query.Refine(Symbols.MAX_TIME, searchData.MaxTime?.ToString());
            if (!String.IsNullOrEmpty(searchData.PingOptions)) query.Refine(Symbols.PING_OPTION, searchData.PingOptions);
            if (!String.IsNullOrEmpty(searchData.Failed)) query.Refine(Symbols.FAILED, searchData.Failed);
            if (!String.IsNullOrEmpty(searchData.ServerName)) query.Refine(Symbols.SERVER_NAME, searchData.ServerName);
            if (!String.IsNullOrEmpty(searchData.SessionID)) query.Refine(Symbols.SESSION_ID, searchData.SessionID);

            if (!String.IsNullOrEmpty(searchData.LookupType)) {
                query.AddLookup(searchData.LookupType);
                switch (searchData.LookupType) {
                    case Symbols.COVERAGE:
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
                        break;
                }
            }

            transactions = TransactionLookupEngine.GetTransactions(query);

            return transactions;
        }
    }
}