using System;
using System.Collections.Generic;
using System.Text;

namespace Kinesis {

    public class SeedData {
        public string Data = "Random Number:" + (new Random()).NextDouble().ToString();

        public string Name { get; set; } = "Client: " + (new Random()).NextDouble().ToString();
    }
}
