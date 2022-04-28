using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Runtime.InteropServices;

namespace TXQ.Utils.Tool
{
    public static class PC
    {
        /// <summary>
        /// 修改计算机名
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        public static int SetComputerName(string Name)
        {
            System.Diagnostics.Process P = new System.Diagnostics.Process()
            {
                StartInfo = new System.Diagnostics.ProcessStartInfo()
                {
                    FileName = "cmd.exe",
                    Arguments = $@"/c wmic computersystem where name='%computername%' call rename '{Name}'",
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            };
            P.Start();
            P.WaitForExit();
            LOG.INFO($"修改计算机名:\t{Name}");
            return P.ExitCode;
        }
        public static Model.PCINFO PCINFO = new Model.PCINFO();

        public static string CompusterName => System.Environment.MachineName;


        /// <summary>
        /// 获取WMI信息
        /// </summary>
        /// <param name="WMI">WMI查询字符串</param>
        /// <returns></returns>
        public static List<ManagementObject> GetWMIObjList(string WMI)
        {
            List<ManagementObject> List = new List<ManagementObject>();
            foreach (ManagementObject queryObj in new ManagementClass(WMI).GetInstances())
            {
                List.Add(queryObj);
            }
            return List;
        }
        public static List<ManagementObject> SearchWMI(string SELECTSTR, string NAMESPACE = "root\\CIMV2")
        {
            List<ManagementObject> List = new List<ManagementObject>();
            ManagementObjectCollection DATA = new ManagementObjectSearcher(NAMESPACE, SELECTSTR).Get();
            foreach (ManagementObject queryObj in DATA)
            {
                List.Add(queryObj);
            }
            return List;
        }
        public static List<string> GetInsSoftWare()
        {
            List<string> infos = new List<string>();
            //读取系统目录32位注册表
            RegistryKey REG = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Default).OpenSubKey(@"SOFTWARE\WOW6432Node\Microsoft\Windows\CurrentVersion\Uninstall", false);
            foreach (string ITEM in REG.GetSubKeyNames())
            {
                RegistryKey DATA = REG.OpenSubKey(ITEM, false);
                List<string> VALUES = DATA.GetValueNames().ToList();
                //此项决定是否在系统中显示此软件
                if (VALUES.Contains("SystemComponent"))
                {
                    if (DATA.GetValue("SystemComponent").ToString() == "1")
                    {
                        continue;
                    }
                }
                if (VALUES.Contains("DisplayName"))
                {
                    infos.Add(DATA.GetValue("DisplayName").ToString());
                }
            }
            //读取系统目录64位注册表
            REG = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Default).OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall", false);
            foreach (string ITEM in REG.GetSubKeyNames())
            {
                RegistryKey DATA = REG.OpenSubKey(ITEM, false);
                List<string> VALUES = DATA.GetValueNames().ToList();
                //此项决定是否在系统中显示此软件
                if (VALUES.Contains("SystemComponent"))
                {
                    if (DATA.GetValue("SystemComponent").ToString() == "1")
                    {
                        continue;
                    }
                }
                if (VALUES.Contains("DisplayName"))
                {
                    infos.Add(DATA.GetValue("DisplayName").ToString());
                }
            }
            //读取用户目录32位注册表
            REG = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Default).OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall", false);
            foreach (string ITEM in REG.GetSubKeyNames())
            {
                RegistryKey DATA = REG.OpenSubKey(ITEM, false);
                List<string> VALUES = DATA.GetValueNames().ToList();
                //此项决定是否在系统中显示此软件
                if (VALUES.Contains("SystemComponent"))
                {
                    if (DATA.GetValue("SystemComponent").ToString() == "1")
                    {
                        continue;
                    }
                }
                if (VALUES.Contains("DisplayName"))
                {
                    infos.Add(DATA.GetValue("DisplayName").ToString());
                }
            }

            return infos;

        }



        /// <summary>
        /// 添加开机启动项
        /// </summary>
        /// <param name="Name">启动项名称</param>
        /// <param name="Path">启动程序 默认为当前程序</param>
        public static void AddAutoRun(string Name, string Path = null)
        {
            if (Path == null)
            {
                Path = System.Windows.Forms.Application.ExecutablePath;
            }
            Microsoft.Win32.RegistryKey rk2 = Microsoft.Win32.Registry.LocalMachine.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run");
            rk2.SetValue(Name, Path);
            rk2.Close();
        }

        /// <summary>
        /// 删除开机启动项
        /// </summary>
        /// <param name="Name">启动项名称</param>
        public static void DeleteAutoRun(string Name)
        {
            Microsoft.Win32.RegistryKey rk2 = Microsoft.Win32.Registry.LocalMachine.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run");
            rk2.DeleteValue(Name, false);
            rk2.Close();
        }

        #region 修正输入法问题

        [DllImport("user32.dll")]
        private static extern bool PostMessage(int hhwnd, uint msg, IntPtr wparam, IntPtr lparam);

        [DllImport("user32.dll")]
        private static extern IntPtr LoadKeyboardLayout(string pwszKLID, uint Flags);


        //调用此方法时，将屏蔽中文输入法(操作系统级别，即使使用快捷键ctrl+shift也还原不回中文输入法)
        //  00000409 英文
        //  00000804 中文
        public static void SetInputLanguange(string en_US = "00000409")
        {
            PostMessage(0xffff, 0x0050, IntPtr.Zero, LoadKeyboardLayout(en_US, 1));
        }
        #endregion 修正输入法问题
        [DllImport("newdev.dll")]
        public static extern bool DiInstallDriverA(string hwndParent, string InfPath, int Flags, bool NeedReboot);




        [DllImport("Setupapi.dll", EntryPoint = "InstallHinfSection", CallingConvention = CallingConvention.StdCall)]
        public static extern void InstallHinfSection(
            [In] IntPtr hwnd,
            [In] IntPtr ModuleHandle,
            [In, MarshalAs(UnmanagedType.LPWStr)] string CmdLineBuffer,
            int nCmdShow);

        #region 获取电池电量，255为没有电池


        public static int GetBatteryLifePercent()
        {
            SystemPowerStatus status = new SystemPowerStatus();

            if (GetSystemPowerStatus(ref status))
            {
                return status.BatteryLifePercent;
            }
            return 255;

        }
        [DllImport("Kernel32.dll")]
        private static extern bool GetSystemPowerStatus(ref SystemPowerStatus systemPowerStatus);

        public struct SystemPowerStatus
        {  // 顺序不可更改
            public ACLineStatus_ ACLineStatus;  // 交流电源状态
            public BatteryFlag_ BatteryFlag;  // 电池充电状态
            public byte BatteryLifePercent;  // 剩余电量的百分比。该成员的值可以在0到100的范围内，如果状态未知，则可以是255
            public SystemStatusFlag_ SystemStatusFlag;  //  省电模式
            public int BatteryLifeTime;  //  剩余电池寿命的秒数。如果未知剩余秒数或设备连接到交流电源，则为–1
            public int BatteryFullLifeTime;  // 充满电时的电池寿命秒数。如果未知电池的完整寿命或设备连接到交流电源，则为–1。
        }

        public enum ACLineStatus_ : byte
        {
            Offline = 0,
            Online = 1,  // 
            UnknowStatus = 255  // 未知
        }

        public enum BatteryFlag_ : byte
        {  // 虽然是枚举，但可以有多个值
            Middle = 0,  // 电池未充电并且电池容量介于高电量和低电量之间
            High = 1,  // 电池电量超过66％
            Low = 2,  // 电池电量不足33％
            Critical = 4,  // 电池电量不足百分之五
            Charging = 8,  // 	充电中
            NoSystemBattery = 128,  // 无系统电池
            UnknowStatus = 255  // 无法读取电池标志信息
        }

        public enum SystemStatusFlag_ : byte
        {
            Off = 0,  //  节电功能已关闭
            On = 1  //  节电功能已打开，节省电池。尽可能节约能源
        }

        #endregion

        #region
        [DllImport("shell32.dll")]
        private static extern int SHEmptyRecycleBin(IntPtr handle, string root, int falgs);
        internal const int SHERB_NOCONFIRMATION = 0x000001;
        internal const int SHERB_NOPROGRESSUI = 0x000002;
        internal const int SHERB_NOSOUND = 0x000004;


        /// <summary>
        /// 清空回收站
        /// </summary>
        /// <param name="form">窗体</param>
        public static void ClearRecycleBin(System.Windows.Forms.Form form)
        {
            SHEmptyRecycleBin(form.Handle, "", SHERB_NOCONFIRMATION + SHERB_NOPROGRESSUI + SHERB_NOSOUND);
        }
        #endregion

        public static bool StartUPWithX => Environment.SystemDirectory.StartsWith("X");

    }
}
