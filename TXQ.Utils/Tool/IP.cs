using System.Text;

namespace TXQ.Utils.Tool
{
    public static class IP
    {
        /// <summary>
        /// 太平洋网络IP地址查询Web接口
        /// </summary>
        /// <returns></returns>
        public static IPInfo GetLocalIPInfo()
        {
            using (System.Net.WebClient client = new System.Net.WebClient())
            {
                client.Encoding = Encoding.GetEncoding("GB2312");
                string str = client.DownloadString("http://whois.pconline.com.cn/ipJson.jsp").Trim();
                str = str.Replace("if(window.IPCallBack) {IPCallBack(", null);
                str = str.Replace(");}", null);
                return str.EXJsonToType<IPInfo>();
            }
        }
    }
    public class IPInfo
    {
        /// <summary>
        /// IP地址
        /// </summary>
        public string IP { get; set; }
        /// <summary>
        /// 省
        /// </summary>
        public string Pro { get; set; }
        /// <summary>
        /// 省 代码
        /// </summary>
        public string ProCode { get; set; }
        /// <summary>
        /// 城市 
        /// </summary>
        public string City { get; set; }
        /// <summary>
        /// 城市代码
        /// </summary>
        public string CityCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Region { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string RegionCode { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public string Addr { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string RegionNames { get; set; }
        /// <summary>
        /// 错误代码
        /// </summary>
        public string Err { get; set; }
    }
}
