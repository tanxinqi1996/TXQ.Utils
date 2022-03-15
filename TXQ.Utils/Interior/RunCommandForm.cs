using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
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
        /// <param name="SHOWERR">运行报错后是否显示在窗体上。为否是自动关闭窗口，不显示错误</param>
        public RunCommandForm(string CMD, bool SHOWERR)
        {
            InitializeComponent();
            ControlBox = false;
            Text = "正在执行命令...";
            command = CMD;
            showerr = SHOWERR;
        }
        private readonly string command;
        //运行完成退出代码 默认-2 窗口异常关闭
        private int exitcode = -2;
        //运行错误是否显示错误
        private readonly bool showerr = true;

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            RunCmd(command);
        }
        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            if (exitcode == 0)
            {
                DialogResult = DialogResult.Yes;
            }
            else
            {
                DialogResult = DialogResult.No;
            }
        }

        public void RunCmd(string CMD)
        {
            var OUTPUT = new DataReceivedEventHandler(Output);
            var ERRPUT = new DataReceivedEventHandler(Error);

            Task.Run(new Action(() =>
            {
                exitcode = TXQ.Utils.Tool.CMD.Run(CMD, OUTPUT, ERRPUT);
                if (exitcode == 0)
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
                            LogAppend(Color.Red, "\r\n\r\n命令执行失败");
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
            }));

        }


        private void Output(object sender, DataReceivedEventArgs e)
        {
            if (e.Data != null)
            {
                LogAppend(Color.Black, e.Data);
            }

        }

        private void Error(object sender, DataReceivedEventArgs e)
        {
            if (e.Data != null)
            {
                LogAppend(Color.Red, e.Data);
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void LogAppend(Color color, string text)
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
