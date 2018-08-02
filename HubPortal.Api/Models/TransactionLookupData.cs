using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HubPortal.Api.Models
{
    public class TransactionLookupData
    {

        public IEnumerable<Transaction> Transactions { get; set; }

        public string SearchType { get; set; }

        public string Process { get; set; }

        public string Client { get; set; }

        public string Source { get; set; }

        public string Destination { get; set; }

        public string TransactionType { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public string MinTime { get; set; }

        public string MaxTime { get; set; }

        public string PingOptions { get; set; }

        public string Failed { get; set; }

        public string ServerName { get; set; }

        public string SessionID { get; set; }

        public string Ignore { get; set; }

        public string PolicyNumber { get; set; }

        public string ReferralNumber { get; set; }

        public string CSR { get; set; }

        public DateTime ReferralDate { get; set; }

        public string ZipCode { get; set; }

        public string PromoCode { get; set; }

        public string CreditCardNumber { get; set; }

        public string CTU { get; set; }

        public string AuthorizationCode { get; set; }

        public string AccountNumber { get; set; }

        public string OrderID { get; set; }

        public string InvoiceNumber { get; set; }

        public string GenericSearchString { get; set; }

        public string IncludeGenericStringInTransaction { get; set; }

        public string ClaimNumber { get; set; }

        public string FNOLNumber { get; set; }

        public string SubCompany { get; set; }

        public string PartNumber { get; set; }

        public string CarID { get; set; }

        public string Amount { get; set; }

        public string WorkOrderNumber { get; set; }

        public string WorkOrderID { get; set; }

        public string WarehouseNumber { get; set; }

        public string ShopNumber { get; set; }

        public string Checkpoint { get; set; }

        public string FullListing { get; set; }

        public string CountOnly { get; set; }

        public string Pagination { get; set; }

        public string TransactionsPerPage { get; set; }

        public string LookupType { get; set; }

    }
}
