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
            Console.WriteLine(ExFind.FindDir("Source").FullName);
            LOG.LogLevel = LogLevel.ALL;
            // Core.Add("36579d1dfffcb27dad793af158dc6bc0ef23dff8");

            TXQ.Utils.P2P.Client.Workdir = "C:\\WORK\\";


            // string file = @"\\SERVER\Share\Release\OS\PS2404103WIN10纯净版(20220224).dht";
            // var DHT = File.ReadAllText(file).EXJsonToType<TXQ.Utils.P2P.DHT>();
            //var D = TXQ.Utils.P2P.Client.DownLoadDHTFile(DHT, "C:\\aaa\\").Result;
            var N = TXQ.Utils.WinAPI.PcInfo.NetWork.NetworkConfigs;
            Console.WriteLine(          TXQ.Utils.WinAPI.PcInfo.NetWork.NetworkConfigs.EXToJSON(true));
            Console.Read();
        }
    }
}
