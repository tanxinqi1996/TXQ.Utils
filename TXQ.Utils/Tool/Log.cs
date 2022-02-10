using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace TXQ.Utils.Tool
{
    public static class LOG
    {
        //日志时间格式
        public static string LogTimeType = "yyyy-MM-dd HH:mm:ss:fff";
        //日志保存位置
        public static string LogFileFullName => LogFilePath + LogFileName;
        private static string LogFilePath => $"{Environment.CurrentDirectory}/Log/";
        private static string LogFileName => $"{DateTime.Now:yyyyMMdd}.log";
        //日志级别
        public static LogLevel LogLevel = LogLevel.INFO;
        //日志输出文本框
        public static RichTextBox LogRichTextBox;

        public static void DEBUG(string msg)
        {
            Write($" -DEBUG- ", msg, LogLevel.DEBUG);
        }

        public static void INFO(string msg)
        {
            Write($" -INFO -", msg, LogLevel.INFO);
        }

        public static void WARNING(string msg)
        {
            Write($" -WARN - ", msg, LogLevel.WARRING);
        }

        public static void ERROR(string msg)
        {
            Write($" -ERROR- ", msg, LogLevel.ERROR);
        }



        [MethodImpl(MethodImplOptions.Synchronized)]
        private static void Write(string Title, string Content, LogLevel Level)
        {
            //如果日志等级过低不记录日志
            if (Level >= LogLevel)
            {
                string log = $"{DateTime.Now.ToString(LogTimeType)}{Title}{Content}{Environment.NewLine}";
                try
                {
                    System.Diagnostics.Debug.WriteLine(log.Trim());
                    //写入文本框
                    WriteTextBox(log.Trim(), Level);
                    //写入控制台
                    switch (Level)
                    {
                        case LogLevel.ERROR:
                            TConsole.WriteLine(log.Trim(), ConsoleColor.Red);
                            break;
                        case LogLevel.WARRING:
                            TConsole.WriteLine(log.Trim(), ConsoleColor.Yellow);
                            break;
                        case LogLevel.INFO:
                            TConsole.WriteLine(log.Trim(), ConsoleColor.Green);
                            break;
                        case LogLevel.DEBUG:
                            TConsole.WriteLine(log.Trim(), ConsoleColor.Cyan);
                            break;
                    }
                    //写入文件
                    WriteFile(log);
                }
                catch
                {

                }
            }

        }
        private static void WriteFile(string log)
        {
            if (Directory.Exists(LogFilePath) == false)
            {
                Directory.CreateDirectory(LogFilePath);
            }
            File.AppendAllText(LogFileFullName, log);
        }

        private static void WriteTextBox(string Content, LogLevel Level)
        {
            if (LogRichTextBox != null)
            {
                Color color = Color.Black;
                switch (Level)
                {
                    case LogLevel.ERROR:
                        color = Color.Red;
                        break;
                    case LogLevel.WARRING:
                        color = Color.DarkOrange;
                        break;
                    case LogLevel.INFO:
                        color = Color.Green;
                        break;
                    case LogLevel.DEBUG:
                        color = Color.Purple;
                        break;
                }
                LogRichTextBox.AppendColorText(Content, color);
                if (LogRichTextBox.Lines.Length > 1000)
                {
                    LogRichTextBox.Text.Remove(0, LogRichTextBox.Lines[0].Length);
                }
            }

        }


        [DllImport("kernel32.dll")]
        public static extern bool AllocConsole();


        [DllImport("kernel32.dll")]
        public static extern bool FreeConsole();
    }
    public enum LogLevel
    {
        ALL = 0,
        DEBUG = 1,
        INFO = 2,
        WARRING = 3,
        ERROR = 4,
        NONE = 5
    }
}
