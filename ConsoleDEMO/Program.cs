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
            TXQ.Utils.Tool.LOG.LogLevel = LogLevel.ALL;
            TXQ.Utils.P2P.Client.Workdir = "D:\\11111\\";
            while (true)
            {
                System.Threading.Thread.Sleep(1000);
                LOG.INFO("+");

            }
        }
    }
}
