using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace TXQ.Utils.Tool
{
    public static class CMD
    {
        /// <summary>
        /// 编码类型;默认为GB2312;请勿修改此选项
        /// </summary>
        public static Encoding GB2312
        {
            get
            {
                System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                return Encoding.GetEncoding("GB2312");
            }
        }


        /// <summary>
        /// 把CMD命令写入BAT文件并执行
        /// </summary>
        /// <param name="Cmd">命令</param>
        /// <param name="ShowCmd">是否显示运行界面</param>
        /// <param name="WaitForExit">是否等待运行完成</param>
        public static void Run(string Cmd, bool ShowCmd = true, bool WaitForExit = true)
        {
            string tempfile = Path.GetTempPath() + Guid.NewGuid().ToString() + ".BAT";
            using var sw = new StreamWriter(tempfile, false, GB2312);
            sw.Write(Cmd);
            sw.Close();

            using Process p = new Process();
            p.StartInfo.FileName = tempfile;
            if (!ShowCmd)
            {
                p.StartInfo.CreateNoWindow = true;
                p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            }
            p.Start();
            if (WaitForExit)
            {
                p.WaitForExit();
                File.Delete(tempfile);
            }
        }

        /// <summary>
        /// 把CMD命令写入BAT文件并执行;并获取返回值
        /// </summary>
        /// <param name="Cmd">命令</param>
        /// <returns>返回值;0=成功</returns>
        public static int Run(string Cmd)
        {
            string tempfile = Path.GetTempPath() + Guid.NewGuid().ToString() + ".BAT";
            using var sw = new StreamWriter(tempfile, false, GB2312);
            sw.Write(Cmd);
            sw.Close();

            using var p = new Process();
            p.StartInfo.FileName = tempfile;
            p.StartInfo.UseShellExecute = false;
            p.Start();
            p.WaitForExit();
            File.Delete(tempfile);
            return p.ExitCode;
        }

        /// <summary>
        /// 运行命令
        /// </summary>
        /// <param name="Cmd">要运行的命令</param>
        /// <param name="outputHandler">异步输出的委托</param>
        /// <param name="errorHandler">异步错误的委托</param>
        /// <returns></returns>
        public static int Run(string Cmd, DataReceivedEventHandler outputHandler, DataReceivedEventHandler errorHandler)
        {
            string tempfile = Path.GetTempPath() + Guid.NewGuid().ToString() + ".BAT";
            using var sw = new StreamWriter(tempfile, false, GB2312);
            sw.Write(Cmd);
            sw.Close();

            Process cmdProcess = new Process();

            cmdProcess.StartInfo.FileName = tempfile;
            //若要使用异步输出则必须不使用操作系统外壳
            cmdProcess.StartInfo.UseShellExecute = false;
            //打开输出重定向
            cmdProcess.StartInfo.RedirectStandardOutput = true;
            //打开错误重定向
            cmdProcess.StartInfo.RedirectStandardError = true;
            //若要使用异步输出则必须不创建新窗口
            cmdProcess.StartInfo.CreateNoWindow = true;
            //指定异步输出使用的编码方式
            cmdProcess.StartInfo.StandardOutputEncoding = GB2312;
            //指定异步错误使用的编码方式
            cmdProcess.StartInfo.StandardErrorEncoding = GB2312;
            //将输出处理函数重定向到输出处理委托
            cmdProcess.OutputDataReceived += outputHandler;
            //将错误处理函数重定向到错误处理委托
            cmdProcess.ErrorDataReceived += errorHandler;
            //启动控制台进程
            cmdProcess.Start();
            //开始异步读取输出流
            cmdProcess.BeginOutputReadLine();
            //开始异步读取错误流
            cmdProcess.BeginErrorReadLine();
            //等待进程退出
            cmdProcess.WaitForExit();
            int ExitCode = cmdProcess.ExitCode;
            //结束异步读取错误流
            cmdProcess.CancelErrorRead();
            //结束异步读取输出流
            cmdProcess.CancelOutputRead();
            return ExitCode;
        }

        /// <summary>
        /// 运行CMD命令并实时显示在Form中
        /// </summary>
        /// <param name="CMD">CMD命令</param>
        /// <param name="SHOWERR">最后一个命令ErrorLevel!=0&&SHOWERR==True时，不自动关闭命令窗口</param>
        public static void RunCmdInForm(string CMD, bool SHOWERR = false)
        {
            new TXQ.Utils.Interior.RunCommandForm(CMD, SHOWERR).ShowDialog();
        }

        /// <summary>
        /// 运行CMD命令并获取输出
        /// </summary>
        /// <param name="Cmd">CMD命令</param>
        /// <returns>输出</returns>
        public static string RunCMDGetStdout(string Cmd)
        {
            string tempfile = Path.GetTempPath() + Guid.NewGuid().ToString() + ".BAT";
            using var sw = new StreamWriter(tempfile, false, GB2312);
            sw.Write(Cmd);
            sw.Close();


            using var proc = new Process();
            proc.StartInfo.CreateNoWindow = true;
            proc.StartInfo.FileName = tempfile;
            proc.StartInfo.UseShellExecute = false;
            proc.StartInfo.RedirectStandardError = true;
            proc.StartInfo.RedirectStandardInput = true;
            proc.StartInfo.RedirectStandardOutput = true;
            proc.Start();
            string outStr = proc.StandardOutput.ReadToEnd();
            proc.Close();
            return outStr;

        }


        /// <summary>
        /// 调用C System函数，在控制台中执行cli命令。
        /// </summary>
        /// <param name="command"></param>
        public static void SystemCmd(string command)
        {
            system(command);
        }

        [DllImport("msvcrt.dll", SetLastError = false, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        private extern static void system(string command); // longjmp

    }
}

