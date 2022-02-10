using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TXQ.Utils.Tool;

namespace TXQ.Utils
{
    public static class Global
    {
        /// <summary>
        /// 日志初始化，全局捕获异常
        /// </summary>
        /// <param name="logLevel">日志级别</param>
        /// <param name="CaptureApplicationException">是否捕获全局Application异常</param>
        public static void Init(Tool.LogLevel logLevel = Tool.LogLevel.INFO)
        {
            if (IsInit == false)
            {
                Tool.LOG.LogLevel = logLevel;
                IsInit = true;
                TXQ.Utils.Global.INI.Write("Log", "Loglevel", (int)LOG.LogLevel);
            }
        }
        private static bool IsInit = false;


        public static Tool.INI INI = new Tool.INI(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName + ".ini");
    }
}
