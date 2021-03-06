﻿using System;

namespace HubPortal.Data.Models {

    public class Transaction {

        public int CheckpointCount { get; set; }

        public string ClientName1 { get; set; }

        public string ClientName2 { get; set; }

        public string PingFlag { get; set; }

        public string ProcessName { get; set; }

        public string Successful { get; set; }

        public decimal? TotalElapsedTime { get; set; }

        public bool? TransactionCompleted { get; set; }

        public string TransactionID { get; set; }

        public DateTime? TransactionTime { get; set; }

        public string TransactionTypeName { get; set; }

        public string Url { get; set; }
    }
}
