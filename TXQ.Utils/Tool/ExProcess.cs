using System;
using System.Diagnostics;

namespace TXQ.Utils.Tool
{
    public static class ExProcess
    {
        /// <summary>
        /// 运行程序获取标准输出,退出值
        /// </summary>
        /// <param name="FileName">程序</param>
        /// <param name="Arguments">参数</param>
        /// <returns>程序运行的标准输出,退出值</returns>
        public static (string StandardOutput, int ExitCode) Run(string FileName, string Arguments = null)
        {
            Process proc = new Process();
            proc.StartInfo.CreateNoWindow = true;
            proc.StartInfo.FileName = FileName;
            proc.StartInfo.Arguments = Arguments;
            proc.StartInfo.UseShellExecute = false;
            proc.StartInfo.RedirectStandardError = true;
            proc.StartInfo.RedirectStandardInput = true;
            proc.StartInfo.RedirectStandardOutput = true;
            proc.Start();
            string outStr = proc.StandardOutput.ReadToEnd();

            proc.WaitForExit();
            proc.Close();
            return (outStr, proc.ExitCode);

        }
    }
}
