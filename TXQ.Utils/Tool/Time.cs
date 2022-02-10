using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace TXQ.Utils.Tool
{
    public static class Time
    {
        /// <summary>
        /// 获取当前周是本年中第几周
        /// </summary>
        public static string WeekOfYear => new GregorianCalendar().GetWeekOfYear(DateTime.Now, CalendarWeekRule.FirstDay, DayOfWeek.Monday).ToString("00");

        /// <summary>
        /// 获取当前年份(2位)+周数(2位)
        /// </summary>
        public static string WeekAndYear => DateTime.Now.Year.ToString().Substring(2, 2) + new GregorianCalendar().GetWeekOfYear(DateTime.Now, CalendarWeekRule.FirstDay, DayOfWeek.Monday).ToString("00");



        [DllImport("Kernel32.dll")]
        private static extern bool SetSystemTime(ref TXQ.Utils.Model.Time lpSystemTime);


        /// <summary>
        /// 设置时间
        /// </summary>
        /// <returns></returns>
        public static bool SetSystemTime(DateTime dt)
        {
            var st = new TXQ.Utils.Model.Time()
            {
                year = (short)dt.Year,
                month = (short)dt.Month,
                dayOfWeek = (short)dt.DayOfWeek,
                day = (short)dt.Day,
                hour = (short)dt.Hour,
                minute = (short)dt.Minute,
                second = (short)dt.Second,
                milliseconds = (short)dt.Millisecond
            };
            return SetSystemTime(ref st);
        }


        /// <summary>
        /// 秒转TimeSpan
        /// </summary>
        /// <param name="SEC"></param>
        /// <returns></returns>
        public static TimeSpan ConvertToTimeSpan(int SEC)
        {
            TimeSpan ts = new TimeSpan(0, 0, SEC);
            return ts;
        }
    }
}
