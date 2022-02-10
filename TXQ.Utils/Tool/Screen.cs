using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;

namespace TXQ.Utils.Tool
{
    /// <summary>
    /// 屏幕
    /// </summary>
    public static class PrScreen
    {
        /// <summary>
        /// 调用微信截图
        /// </summary>
        /// <returns></returns>
        [DllImport("/Lib/PrScrn.dll", EntryPoint = "PrScrn")]
        private static extern int PrScrn();


        /// <summary>
        ///调用微信截图API，第一次调用会释放DLL到Lib目录
        /// </summary>
        /// <returns>0=退出截图;1=复制截图到剪切板；2=保存截图</returns>
        public static int PrintScrn()
        {
            string path = Environment.CurrentDirectory + "//Lib//PrScrn.dll";
            string dir = Environment.CurrentDirectory + "//Lib";
            if (System.IO.File.Exists(path) == false)
            {
                if (System.IO.Directory.Exists(dir) == false)
                {
                    System.IO.Directory.CreateDirectory(dir);
                }
                var FS = new FileStream(path, FileMode.CreateNew);
                FS.Write(TXQ.Utils.Properties.Resources.PrScrn, 0, TXQ.Utils.Properties.Resources.PrScrn.Length);
                FS.Close();
            }
            return PrScrn();
        }
    }
}
