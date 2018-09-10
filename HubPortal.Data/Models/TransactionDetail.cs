using System;

namespace HubPortal.Data.Models {

    public class TransactionDetail {

        public string AccountNumber { get; set; }

        public string Amount { get; set; }

        public string AuthorizationCode { get; set; }

        public string ClaimNumber { get; set; }

        public string Completed { get; set; }

        public string CreditCardNumber { get; set; }

        public string Csr { get; set; }

        public string Ctu { get; set; }

        public string Destination { get; set; }

        public string DestinationType { get; set; }

        public int? ElapsedTime { get; set; }

        public string FnolNumber { get; set; }

        public string InvoiceNumber { get; set; }

        public string PhoneNumber { get; set; }

        public string Ping { get; set; }

        public string PolicyNumber { get; set; }

        public string ProcessId { get; set; }

        public string ProcessName { get; set; }

        public string ReferralDate { get; set; }

        public string ReferralNumber { get; set; }

        public string ServiceLayer { get; set; }

        public string SessionID { get; set; }

        public string ShopNumber { get; set; }

        public string Source { get; set; }

        public string SourceType { get; set; }

        public string SubCompany { get; set; }

        public string Successful { get; set; }

        public string TransactionId { get; set; }

        public DateTime? TransactionTime { get; set; }

        public string TransactionType { get; set; }

        public string URL { get; set; }

        public string WarehouseNumber { get; set; }

        public string WorkOrderId { get; set; }

        public string WorkOrderNumber { get; set; }
    }
}
