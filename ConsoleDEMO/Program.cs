using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TXQ.Utils.Tool;

namespace ConsoleDEMO
{
    internal class Program
    {
        static void Main(string[] args)
        {
            LOG.LogLevel = LogLevel.ALL;

            TXQ.Utils.P2P.Client.Init(true);

            // Core.Add("36579d1dfffcb27dad793af158dc6bc0ef23dff8");




            string file = @"\\SERVER\Share\Release\OS\PS2404103WIN10纯净版(20220224).dht";
            var DHT = File.ReadAllText(file).EXJsonToType<TXQ.Utils.P2P.DHT>();
            var D = TXQ.Utils.P2P.Client.DownLoadFile(DHT, "").Result;
        }
    }
}
