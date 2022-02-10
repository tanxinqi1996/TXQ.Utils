using Seagull.BarTender.Print;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TXQ.Utils.Tool;

namespace TXQ.Utils.Print.Bartender
{
    public class Main
    {
        public static Engine BartenderEngine;
        public static void Init()
        {
            if (BartenderEngine == null)
            {
                try
                {
                    BartenderEngine = new Engine();
                    BartenderEngine.Start();
                }
                catch (Exception EX)
                {
                    BartenderEngine = null;
                    LOG.INFO("Bartender Start Fail");
                    throw new Exception("致命错误,请检查是否安装Bartender .net SDK" + Environment.NewLine + EX.Message);
                }
                LOG.INFO("Bartender Start");
            }
            else if (BartenderEngine.IsAlive)
            {
                LOG.INFO("Bartender IsAlive");
                return;
            }
            else
            {
                BartenderEngine = new Engine();
                BartenderEngine.Start();
                LOG.INFO("Bartender Start");
            }
        }
        public static void ReStart()
        {
            if (BartenderEngine != null)
            {
                BartenderEngine.Stop();
            }
            Init();
        }
        public static void Stop()
        {
            if (BartenderEngine != null)
            {
                if (BartenderEngine.IsAlive)
                {
                    BartenderEngine.Stop();
                    LOG.INFO("Bartender Stop");
                }
            }
        }

    }
}
