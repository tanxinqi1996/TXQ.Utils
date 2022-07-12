using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TXQ.Utils.Interior
{
    internal partial class RunCommandForm : Form
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="CMD">CMD命令</param>
        /// <param name="WaitOnError">CMD命令返回!=0时,等待用户操作,不自动退出</param>
        public RunCommandForm(string CMD, bool WaitOnError = true)
        {
            InitializeComponent();
            ControlBox = false;
            Text = "正在执行命令...";

            showerr = WaitOnError;
            Task.Run(() =>
            {
                try
                {
                    string tempfile =$"{Path.GetTempPath()}{Guid.NewGuid()}.BAT";
                    //using StreamWriter sw = new StreamWriter(tempfile, false, TXQ.Utils.Tool.CMD.DefaultEncoding);
                    //sw.Write(CMD);
                    //sw.Close();
                    File.WriteAllText(tempfile,CMD, TXQ.Utils.Tool.CMD.DefaultEncoding);
                    cmdProcess = new Process();
                    cmdProcess.StartInfo.FileName = tempfile;
                    cmdProcess.StartInfo.UseShellExecute = false;
                    cmdProcess.StartInfo.RedirectStandardOutput = true;
                    cmdProcess.StartInfo.RedirectStandardError = true;
                    cmdProcess.StartInfo.CreateNoWindow = true;
                    cmdProcess.StartInfo.StandardOutputEncoding = TXQ.Utils.Tool.CMD.DefaultEncoding;
                    cmdProcess.StartInfo.StandardErrorEncoding = TXQ.Utils.Tool.CMD.DefaultEncoding;
                    cmdProcess.OutputDataReceived += new DataReceivedEventHandler(Output);
                    cmdProcess.ErrorDataReceived += new DataReceivedEventHandler(Error);
                    cmdProcess.Start();
                    cmdProcess.BeginOutputReadLine();
                    cmdProcess.BeginErrorReadLine();
                    cmdProcess.WaitForExit();
                    cmdProcess.CancelErrorRead();
                    cmdProcess.CancelOutputRead();
                    ExitCode = cmdProcess.ExitCode;
                    File.Delete(tempfile);
                    if (ExitCode == 0)
                    {
                        Invoke(new Action(() =>
                        {
                            DialogResult = DialogResult.OK;
                            Close();
                        }));
                    }
                    else
                    {
                        if (showerr)
                        {
                            Invoke(new Action(() =>
                            {
                                ControlBox = true;
                            }));
                        }
                        else
                        {
                            Invoke(new Action(() =>
                            {
                                Close();
                            }));
                        }
                    }
                }
                catch (Exception EX)
                {
                    TXQ.Utils.Tool.LOG.ERROR($"CMD命令运行异常:{EX.Message}");
                }

            });
        }
        //运行完成退出代码 默认-2 窗口异常关闭
        private int ExitCode = -2;
        //运行错误是否显示错误
        private readonly bool showerr = true;

        private static Process cmdProcess;

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            if (cmdProcess.HasExited == false)
            {
                cmdProcess.Close();
            }
            if (ExitCode == 0)
            {
                DialogResult = DialogResult.Yes;
            }
            else
            {
                DialogResult = DialogResult.No;
            }
        }





        private void Output(object sender, DataReceivedEventArgs e)
        {
            if (e.Data != null)
            {
                LogAppend(Color.Black, e.Data);
                TXQ.Utils.Tool.LOG.INFO(e.Data);
            }
        }

        private void Error(object sender, DataReceivedEventArgs e)
        {
            if (e.Data != null)
            {
                LogAppend(Color.Red, e.Data);
                TXQ.Utils.Tool.LOG.ERROR(e.Data);
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void LogAppend(Color color, string text)
        {
            if (this.IsHandleCreated)
            {
                Invoke(new Action(() =>
                {
                    richTextBox1.AppendText("\r\n");
                    richTextBox1.SelectionColor = color;
                    richTextBox1.AppendText(text);
                    richTextBox1.Select(richTextBox1.TextLength, 0);
                    richTextBox1.ScrollToCaret();
                }));
            }
        }

    }
}
