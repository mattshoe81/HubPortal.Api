using System;

namespace HubPortal.Data.Models {

    public class Process {

        public string DocumentationURL { get; set; }

        public int? Duration { get; set; }

        public string Pingable { get; set; }

        public DateTime? PingDate { get; set; }

        public string PingDesirable { get; set; }

        public string PingName { get; set; }

        public DateTime? PingTime { get; set; }

        public string ProcessName { get; set; }

        public string Status { get; set; }

        public DateTime? SuccessPingDate { get; set; }

        public DateTime? SuccessPingTime { get; set; }
    }
}
