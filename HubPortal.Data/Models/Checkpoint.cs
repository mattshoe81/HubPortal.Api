using System;

namespace HubPortal.Data.Models {

    public class Checkpoint {

        public int CheckpointId { get; set; }

        public int ElapsedTime { get; set; }

        public int ID { get; set; }

        public string Location { get; set; }

        public string ServerName { get; set; }

        public int Size { get; set; }

        public DateTime? Time { get; set; }

        public string TransactionId { get; set; }

        public string Type { get; set; }
    }
}
