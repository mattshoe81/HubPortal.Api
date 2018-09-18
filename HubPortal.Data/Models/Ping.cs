using System;
using System.Collections.Generic;
using System.Text;

namespace HubPortal.Data.Models {

    public class Ping {

        public string ClientID { get; set; }

        public string Enabled { get; set; }

        public string Expected { get; set; }

        public string Name { get; set; }

        public string PingString { get; set; }

        public string PingType { get; set; }

        public string Type { get; set; }
    }
}
