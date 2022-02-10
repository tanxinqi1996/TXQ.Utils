using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace TXQ.Utils.Tool
{
    /// <summary>
    /// Console增强类
    /// </summary>
    public static class TConsole
    {
        /// <summary>
        /// 打开Console
        /// </summary>
        /// <returns></returns>
        [DllImport("kernel32.dll")]
        public static extern bool AllocConsole();


        /// <summary>
        /// 关闭Console
        /// </summary>
        /// <returns></returns>
        [DllImport("kernel32.dll")]
        public static extern bool FreeConsole();


        /// <summary>
        /// Console WriteLine
        /// </summary>
        /// <param name="STR"></param>
        /// <param name="color"></param>
        public static void WriteLine(string STR, ConsoleColor color=ConsoleColor.White)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(STR);
            Console.ResetColor();
        }


        /// <summary>
        /// Console Write
        /// </summary>
        /// <param name="STR"></param>
        /// <param name="color"></param>
        public static void Write(string STR, ConsoleColor color = ConsoleColor.White)
        {
            Console.ForegroundColor = color;
            Console.Write(STR);
            Console.ResetColor();
        }
    }
}
