using System;
using System.Collections.Generic;
using System.Text;

namespace KinesisProducer {

    public class SeedData {
        public string RandomNumberData = "Random Number:" + (new Random()).NextDouble().ToString();

        public string Client { get; set; } = "Client: " + (new Random()).NextDouble().ToString();
    }
}
