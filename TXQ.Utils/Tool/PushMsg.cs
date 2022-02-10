using System;

namespace TXQ.Utils.Tool
{
    public static class PushMsg
    {
        /// <summary>
        /// BARK推送，可能会抛出异常，请使用TRY CATCH捕获
        /// </summary>
        /// <param name="TITLE">标题</param>
        /// <param name="Content">正文</param>
        /// <returns></returns>
        public static string Bark(string TITLE, string Content, string KEY = "DynTpv9hXQCuAfr755A2qM")
        {
            string url = "https://api.day.app/DynTpv9hXQCuAfr755A2qM" + $"/{KEY}/{TITLE}/{Content}";
            return Tool.HTTP.Get(url).Result;
        }
    }
}
