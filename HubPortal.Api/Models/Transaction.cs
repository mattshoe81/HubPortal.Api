using System;

namespace HubPortal.Api.Models {

    public class Transaction {
        public string TransactionID { get; set; }

        public string ProcessName { get; set; }

        public string TransactionTypeName { get; set; }

        public DateTime TransactionTime { get; set; }

        public bool? TransactionCompleted { get; set; }

        public decimal? TotalElapsedTime { get; set; }

        public string Url { get; set; }

        public string ClientName1 { get; set; }

        public string ClientName2 { get; set; }

        public string PingFlag { get; set; }

        public string Successful { get; set; }

        public int CheckpointCount { get; set; }
    }
}