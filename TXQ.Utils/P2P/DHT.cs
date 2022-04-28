using System;
using System.Collections.Generic;

namespace TXQ.Utils.P2P
{

    public class DHT
    {
        public string FileName { get; set; }

        public string SHA { get; set; }

        public Dictionary<int, string> SubFiles { get; set; }

        public DateTime DateTime { get; set; }
    }
}