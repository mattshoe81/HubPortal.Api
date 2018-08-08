using System;
using System.Collections.Generic;

namespace HubPortal.Api.Models {

    /// <summary>
    /// Data used to specify the criteria by which to collect transactions during the transaction lookup.
    /// </summary>
    public class TransactionLookupData {

        #region Public Properties

        public string AccountNumber { get; set; }
        public int? Amount { get; set; }
        public string AuthorizationCode { get; set; }
        public string CarID { get; set; }
        public string Checkpoint { get; set; }
        public string ClaimNumber { get; set; }
        public string Client { get; set; }
        public int? CreditCardNumber { get; set; }
        public string CSR { get; set; }
        public string CTU { get; set; }
        public string Destination { get; set; }
        public DateTime? EndTime { get; set; }
        public string Failed { get; set; }
        public string FNOLNumber { get; set; }
        public string FullListing { get; set; }
        public string GenericSearchString { get; set; }
        public string IncludeGenericStringInTransaction { get; set; }
        public string InvoiceNumber { get; set; }
        public string LookupType { get; set; }
        public int? MaxTime { get; set; }
        public int? MinTime { get; set; }
        public string OrderID { get; set; }
        public string PartNumber { get; set; }
        public string PingOptions { get; set; }
        public string PolicyNumber { get; set; }
        public string Process { get; set; }
        public string PromoCode { get; set; }
        public DateTime? ReferralDate { get; set; }
        public string ReferralNumber { get; set; }
        public string SearchType { get; set; }
        public string ServerName { get; set; }
        public string SessionID { get; set; }
        public string ShopNumber { get; set; }
        public string Source { get; set; }
        public DateTime? StartTime { get; set; }
        public string SubCompany { get; set; }
        public IEnumerable<Transaction> Transactions { get; set; }
        public string TransactionsPerPage { get; set; }
        public string TransactionType { get; set; }
        public string WarehouseNumber { get; set; }
        public string WorkOrderID { get; set; }
        public string WorkOrderNumber { get; set; }
        public string ZipCode { get; set; }

        #endregion Public Properties
    }
}