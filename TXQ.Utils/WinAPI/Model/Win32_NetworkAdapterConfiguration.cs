namespace TXQ.Utils.WinAPI.Model
{
    public class Win32_NetworkAdapterConfiguration
    {

        /// <summary>
        /// 网卡名称 例如: [00000001] Intel(R) Wi-Fi 6 AX200 160MHz
        /// </summary>
        public string Caption { get; set; }
        /// <summary>
        /// 网卡名称 例如: Intel(R) Wi-Fi 6 AX200 160MHz
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// DHCP状态 例如: True False
        /// </summary>
        public bool DHCPEnabled { get; set; }

        /// <summary>
        /// DHCP服务器地址 列如：192.168.2.1 
        /// </summary>
        public string DHCPServer { get; set; }

        /// <summary>
        /// 序列号
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        /// IP地址 可能有多个IP地址 列如  {"192.168.110.3", "fe80::3930:aa0f:d6f2:7047"} 
        /// </summary>
        public string[] IPAddress { get; set; }


        /// <summary>
        /// 是否存在IP ,可以使用此项筛选不存在IP的网卡
        /// </summary>
        public bool IPEnabled { set; get; }


        /// <summary>
        /// 子网掩码  列如 {"255.255.240.0", "64"}  
        /// </summary>
        public string[] IPSubnet { set; get; }


        /// <summary>
        /// MACAddress MAC地址
        /// </summary>
        public string MacAdress { get; set; }


    }

}
