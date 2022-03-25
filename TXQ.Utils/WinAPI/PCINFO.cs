using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Management;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;
using TXQ.Utils.Tool;

namespace TXQ.Utils.WinAPI
{
    public static class PCINFO
    {
        public static List<string> DVD;
        public static List<MonitorInfo> Monitor;
        public static BaseboardInfo Baseboard;
        public static SystemInfo System;
        public static List<NetWorkInfo> NetWork;
        public static List<DiskInfo> Disk;
        public static List<MemoryInfo> Memory;
        public static CpuInfo CPU;
        public static List<GPUInfo> GPU;
        public static BiosInfo Bios;
        static PCINFO()
        {
            InitDiskInfo();
            InitDVDInfo();
            InitMonitorInfo();
            InitBaseboardInfo();
            InitSystemInfo();
            InitNetWorkInfo();
            InitMemoryInfo();
            InitGPUInfo();
        }
        private static void InitDVDInfo()
        {
            List<string> DVD = new List<string>();
            foreach (ManagementObject queryObj in TXQ.Utils.Tool.PC.GetWMIObjList("Win32_CDROMDrive"))
            {
                string Name = Convert.ToString(queryObj["Caption"]).Trim();
                DVD.Add(Name);
            }
            PCINFO.DVD = DVD;
        }
        private static void InitMonitorInfo()
        {
            List<MonitorInfo> Class = new List<MonitorInfo>();
            foreach (Screen item in Screen.AllScreens)
            {
                MonitorInfo screen = new MonitorInfo()
                {
                    Scale = Scale,
                    Primary = item.Primary
                };
                screen.Height = (int)(item.Bounds.Height * screen.Scale);
                screen.Width = (int)(item.Bounds.Width * screen.Scale);
                Class.Add(screen);
            }
            Monitor = Class;
        }
        private static void InitBaseboardInfo()
        {
            BaseboardInfo item = new BaseboardInfo();
            foreach (ManagementObject queryObj in TXQ.Utils.Tool.PC.GetWMIObjList("WIN32_baseboard"))
            {
                item.Product = Convert.ToString(queryObj["Product"]);
                item.Manufacturer = Convert.ToString(queryObj["Manufacturer"]);
                item.SerialNumber = Convert.ToString(queryObj["SerialNumber"]);
            }
            Baseboard = item;
        }
        private static void InitSystemInfo()
        {
            SystemInfo item = new SystemInfo();
            foreach (ManagementObject queryObj in TXQ.Utils.Tool.PC.GetWMIObjList("Win32_OperatingSystem"))
            {
                item.Installdate = ManagementDateTimeConverter.ToDateTime((string)queryObj["InstallDate"]);
                item.LastBootUpTime = ManagementDateTimeConverter.ToDateTime((string)queryObj["LastBootUpTime"]);
                item.MUILanguages = Convert.ToString(queryObj["MUILanguages"]).Trim();
                item.Caption = Convert.ToString(queryObj["Caption"]).Trim();
                item.Version = Convert.ToString(queryObj["Version"]).Trim();
                item.SystemDevice = Convert.ToString(queryObj["SystemDevice"]).Trim();
                item.SystemDrive = Convert.ToString(queryObj["SystemDrive"]).Trim();
                item.OperatingSystemSKU = Convert.ToString(queryObj["OperatingSystemSKU"]).Trim();
                if (queryObj["MUILanguages"] == null)
                {
                    item.MUILanguages = Convert.ToString(queryObj["MUILanguages"]).Trim();
                }
                else
                {
                    string[] arrMUILanguages = (string[])(queryObj["MUILanguages"]);
                    foreach (string arrValue in arrMUILanguages)
                    {
                        item.MUILanguages = arrValue;
                    }
                }
            }
            foreach (ManagementObject queryObj in TXQ.Utils.Tool.PC.GetWMIObjList("WIN32_COMPUTERSYSTEMPRODUCT"))
            {
                item.IdentifyingNumber = Convert.ToString(queryObj["IdentifyingNumber"]).Trim();
                item.UUID = Convert.ToString(queryObj["UUID"]).Trim();
                item.Description = Convert.ToString(queryObj["Description"]).Trim();
            }
            if (item.Version.StartsWith("5.0"))
            {
                item.SysVer = "Win2000";
            }
            else if (item.Version.StartsWith("5.1"))
            {
                item.SysVer = "WinXP";
            }
            else if (item.Version.StartsWith("6.0"))
            {
                item.SysVer = "WinVisita";
            }
            else if (item.Version.StartsWith("6.1"))
            {
                item.SysVer = "Win7";
            }
            else if (item.Version.StartsWith("6.2"))
            {
                item.SysVer = "Win8";
            }
            else if (item.Version.StartsWith("10.0.1"))
            {
                item.SysVer = "Win10";
            }
            else if (item.Version.StartsWith("10.0.2"))
            {
                item.SysVer = "Win11";
            }
            else
            {
                item.SysVer = item.Version;
                throw new Exception("Un Definition System Version");
            }
            if (Environment.Is64BitOperatingSystem == true)
            {
                item.SysVer += "x64";
            }
            else
            {
                item.SysVer += "x86";
            }
            System = item;
        }

        private static void InitNetWorkInfo()
        {
            List<NetWorkInfo> Class = new List<NetWorkInfo>();
            foreach (ManagementObject queryObj in TXQ.Utils.Tool.PC.GetWMIObjList("Win32_NetworkAdapter"))
            {
                NetWorkInfo item = new NetWorkInfo
                {
                    Name = Convert.ToString(queryObj["Name"]),
                    MACAddress = Convert.ToString(queryObj["MACAddress"]),
                    ServiceName = Convert.ToString(queryObj["ServiceName"]),
                    Speed = Convert.ToString(queryObj["Speed"]),
                    Manufacturer = Convert.ToString(queryObj["Manufacturer"]),
                    PhysicalAdapter = Convert.ToBoolean(queryObj["PhysicalAdapter"]),
                    PNPDeviceID = Convert.ToString(queryObj["PNPDeviceID"]),
                };
                Class.Add(item);
            }
            NetWork = Class;
        }
        private static void InitDiskInfo()
        {
            List<DiskInfo> Class = new List<DiskInfo>();
            foreach (ManagementObject queryObj in TXQ.Utils.Tool.PC.GetWMIObjList("Win32_DiskDrive"))
            {
                DiskInfo item = new DiskInfo
                {
                    Index = Convert.ToInt32(queryObj["Index"]),
                    Caption = Convert.ToString(queryObj["model"]).Trim(),
                    InterfaceType = Convert.ToString(queryObj["InterfaceType"]).Trim(),
                    Size = Convert.ToInt32(Convert.ToInt64(queryObj["size"]) / 1000 / 1000 / 1000),
                    Partitions = Convert.ToInt32(queryObj["Partitions"]),
                    Size_KB = Convert.ToInt64(queryObj["size"]),
                    SerialNumber = Convert.ToString(queryObj["SerialNumber"]),
                    FirmwareRevision = Convert.ToString(queryObj["FirmwareRevision"])
                };
                Class.Add(item);
            }
            Disk = Class;
        }
        private static void InitMemoryInfo()
        {
            List<MemoryInfo> Class = new List<MemoryInfo>();
            foreach (ManagementObject queryObj in TXQ.Utils.Tool.PC.GetWMIObjList("Win32_PhysicalMemory"))
            {
                MemoryInfo item = new MemoryInfo
                {
                    Manufacturer = Convert.ToString(queryObj["Manufacturer"]).Trim(),
                    Speed = Convert.ToInt32(queryObj["speed"]),
                    Size = Convert.ToInt32(Convert.ToInt64(queryObj["Capacity"]) / 1024 / 1024 / 1024),
                    SerialNumber = Convert.ToString(queryObj["SerialNumber"]).Trim(),
                    PartNumber = Convert.ToString(queryObj["PartNumber"]).Trim()
                };
                item.Model = $"{item.Manufacturer} {item.Speed} {item.Size}GB";
                Class.Add(item);
            }
            Memory = Class;
        }
        private static void InitCPUInfo()
        {
            foreach (ManagementObject queryObj in TXQ.Utils.Tool.PC.GetWMIObjList("Win32_Processor"))
            {
                CPU = new CpuInfo
                {
                    Name = Convert.ToString(queryObj["Name"]).Trim(),
                    SocketDesignation = Convert.ToString(queryObj["SocketDesignation"]).Trim(),
                    Caption = Convert.ToString(queryObj["Caption"]).Trim(),
                    ProcessorId = Convert.ToString(queryObj["ProcessorId"]).Trim()
                };
                return;
            }

        }
        public static void InitGPUInfo()
        {
            List<GPUInfo> Class = new List<GPUInfo>();

            foreach (ManagementObject queryObj in TXQ.Utils.Tool.PC.GetWMIObjList("Win32_VideoController"))
            {
                GPUInfo item = new GPUInfo
                {
                    Caption = Convert.ToString(queryObj["Caption"]).Trim(),
                    InfFilename = Convert.ToString(queryObj["InfFilename"]),
                };

                Class.Add(item);
            }
            GPU = Class;
        }


        #region Win32 API
        [DllImport("user32.dll")]
        private static extern IntPtr GetDC(IntPtr ptr);
        [DllImport("gdi32.dll")]
        private static extern int GetDeviceCaps(
        IntPtr hdc, // handle to DC
        int nIndex // index of capability
        );
        [DllImport("user32.dll", EntryPoint = "ReleaseDC")]
        private static extern IntPtr ReleaseDC(IntPtr hWnd, IntPtr hDc);
        #endregion
        private const int VERTRES = 10;
        private const int DESKTOPVERTRES = 117;
        /// <summary>
        /// 缩放百分比
        /// </summary>
        public static float Scale
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                IntPtr hdc = GetDC(IntPtr.Zero);
                float ScaleY = (float)GetDeviceCaps(hdc, DESKTOPVERTRES) / GetDeviceCaps(hdc, VERTRES);
                ReleaseDC(IntPtr.Zero, hdc);
                return ScaleY;
            }
        }
        public class CpuInfo
        {
            /// <summary>
            /// 型号
            /// </summary>
            public string Name { get; set; }
            /// <summary>
            /// 步进信息
            /// </summary>
            public string Caption { get; set; }
            /// <summary>
            /// 接口类型
            /// </summary>
            public string SocketDesignation { get; set; }
            /// <summary>
            /// CPUID
            /// </summary>
            public string ProcessorId { get; set; }

        }
        public class MemoryInfo
        {
            /// <summary>
            /// 名称 镁光威刚
            /// </summary>
            public string Manufacturer { get; set; }
            /// <summary>
            /// 频率
            /// </summary>
            public int Speed { get; set; }
            /// <summary>
            /// 容量
            /// </summary>
            public int Size { get; set; }
            /// <summary>
            /// 型号
            /// </summary>
            public string Model { get; set; }
            /// <summary>
            /// PN
            /// </summary>
            public string PartNumber { get; set; }
            /// <summary>
            /// SN
            /// </summary>
            public string SerialNumber { get; set; }
        }
        public class DiskInfo
        {
            /// <summary>
            /// 编号
            /// </summary>
            public int Index { get; set; }
            /// <summary>
            /// 名称
            /// </summary>
            public string Caption { get; set; }
            /// <summary>
            /// 容量
            /// </summary>
            public int Size { get; set; }
            /// <summary>
            /// 容量
            /// </summary>
            public long Size_KB { get; set; }
            /// <summary>
            /// 接口类型 USB SCSI
            /// </summary>
            public string InterfaceType { get; set; }
            /// <summary>
            /// 分区数量
            /// </summary>
            public int Partitions { get; set; }
            /// <summary>
            /// SN
            /// </summary>
            public string SerialNumber { get; set; }
            /// <summary>
            /// 固件版本
            /// </summary>
            public string FirmwareRevision { get; set; }
        }
        public class NetWorkInfo
        {
            /// <summary>
            /// 网卡名
            /// </summary>
            public string Name { get; set; }
            /// <summary>
            /// MAC 地址
            /// </summary>
            public string MACAddress { get; set; }
            /// <summary>
            /// 制造商
            /// </summary>
            public string Manufacturer { get; set; }
            /// <summary>
            /// 速率
            /// </summary>
            public string Speed { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string ServiceName { get; set; }
            /// <summary>
            /// 硬件ID
            /// </summary>
            public string PNPDeviceID { get; set; }
            /// <summary>
            /// 是否物理网卡
            /// </summary>
            public bool PhysicalAdapter { get; set; }
        }
        public class SystemInfo
        {
            /// <summary>
            /// 系统安装日期
            /// </summary>
            public DateTime Installdate { get; set; }
            /// <summary>
            /// 系统启动时间
            /// </summary>
            public DateTime LastBootUpTime { get; set; }
            /// <summary>
            /// 版本WIN7 WIN10
            /// </summary>
            public string Caption { get; set; }


            /// <summary>
            /// 系统盘
            /// </summary>
            public string SystemDevice { get; set; }
            /// <summary>
            /// 系统分区
            /// </summary>
            public string SystemDrive { get; set; }
            public string Description { get; set; }
            public string UUID { get; set; }
            public string IdentifyingNumber { get; set; }



            /// <summary>
            /// SKU 
            /// </summary>
            public string OperatingSystemSKU { get; set; }


            /// <summary>
            /// 版本号 
            /// </summary>

            public string Version { get; set; }

            /// <summary>
            /// 语言
            /// </summary>
            public string MUILanguages { get; set; }


            /// <summary>
            /// 系统版本 如  Win7x86  Win10 x64 
            /// </summary>
            public string SysVer { get; set; }
        }
        public class GPUInfo
        {
            /// <summary>
            /// 芯片组
            /// </summary>
            public string Caption { get; set; }


            /// <summary>
            /// InfFilename
            /// </summary>
            public string InfFilename { get; set; }

        }
        public class MonitorInfo
        {
            /// <summary>
            /// 显示器宽 
            /// </summary>
            public int Width { get; set; }
            /// <summary>
            /// 高
            /// </summary>
            public int Height { get; set; }
            /// <summary>
            /// 是否为主显示器
            /// </summary>
            public bool Primary { get; set; }

            /// <summary>
            /// 缩放比例
            /// </summary>
            public float Scale { get; set; }
        }
        public class BaseboardInfo
        {
            /// <summary>
            /// 主板名称
            /// </summary>
            public string Product { get; set; }
            /// <summary>
            /// 主板制造商
            /// </summary>
            public string Manufacturer { get; set; }
            /// <summary>
            /// 主板SN
            /// </summary>
            public string SerialNumber { get; set; }

            public string OA3Key
            {
                get
                {


                    try
                    {
                        string CDK = null;
                        using (ManagementClass searcher = new ManagementClass("SoftwareLicensingService"))
                        {
                            foreach (ManagementObject queryObj in searcher.GetInstances())
                            {
                                CDK = Convert.ToString(queryObj["OA3xOriginalProductKey"]);
                            }
                        }
                        //if (CDK.EXIsNullOrEmptyOrWhiteSpace() == false)
                        //{
                        //    return CDK;
                        //}
                        //var XML = global::System.IO.File.ReadAllText("C:\\Windows\\OA3.XML");
                        //var cDkey = EXXml.XmlEXToModel<Utils.Model.OA3CDkey>(XML);
                        //CDK = cDkey.Key.ProductKey;
                        return CDK;

                    }
                    catch
                    {
                        return null;
                    }


                }
            }
        }
        public class BiosInfo
        {
            /// <summary>
            /// 版本号
            /// </summary>
            public string Caption { get; set; }
            /// <summary>
            /// 制造商
            /// </summary>
            public string Manufacturer { get; set; }
            /// <summary>
            /// 版本
            /// </summary>
            public string SMBIOSBIOSVersion { get; set; }
            /// <summary>
            /// BIOS日期
            /// </summary>
            public string ReleaseDate { get; set; }
            /// <summary>
            /// 主板SN
            /// </summary>
            public string SerialNumber { get; set; }
            public string Version { get; set; }
        }
    }
}
