using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Forms;
using TXQ.Utils.Tool;

namespace TXQ.Utils.Tool
{

    public static class TThread
    {
        /// <summary>
        /// 在单独的线程中运行，并显示加载动画，调用窗体需要使用invoke
        /// </summary>
        /// <param name="form">窗体</param>
        /// <param name="func">运行内容 调用窗体需要使用invoke</param>
        public static void RunEx(Form form, Action action, Action CllBack = null)
        {
            var LoadingForm = new TXQ.Utils.Interior.Loading
            {
                StartPosition = FormStartPosition.CenterParent,
            };
            Task.Run(() =>
            {
                try
                {
                    action();
                    if (CllBack != null)
                    {
                        if (!form.IsDisposed && !LoadingForm.IsDisposed)
                        {
                            form.Invoke(new Action(() =>
                            {
                                CllBack();
                            }));
                        }
                    }
                }
                catch (Exception ex)
                {
                    LOG.ERROR(form.Text +ex.Message);
                    throw;
                }
                finally
                {
                    if (!form.IsDisposed && !LoadingForm.IsDisposed)
                    {
                        form.Invoke(new Action(() =>
                        {
                            LoadingForm.Dispose();
                        }));
                    }
                }
            });

            LoadingForm.ShowDialog();
        }

        public static void Sleep(int sec, string TASK = null)
        {
            LOG.INFO($"System.Threading.Thread.Sleep   {sec}s\t{TASK}");
            System.Threading.Thread.Sleep(sec * 1000);
        }


        public static void CheckDaulRun()
        {

            Process currentProcess = Process.GetCurrentProcess();
            foreach (Process p in Process.GetProcesses())
            {
                if (p.ProcessName == currentProcess.ProcessName && p.Id != currentProcess.Id)
                {
                    Environment.Exit(0);
                }
            }
        }
    }
}

