using System;

namespace TXQ.Utils.WinAPI
{
    public class PnPEntity
    {
        public ushort Availability { get; set; }
        public string Caption { get; set; }
        public string ClassGuid { get; set; }
        public string[] CompatibleID { get; set; }
        public uint ConfigManagerErrorCode { get; set; }
        public bool ConfigManagerUserConfig { get; set; }
        public string CreationClassName { get; set; }
        public string Description { get; set; }
        public string DeviceID { get; set; }
        public bool ErrorCleared { get; set; }
        public string ErrorDescription { get; set; }
        public string[] HardwareID { get; set; }
        public DateTime InstallDate { get; set; }
        public bool LastErrorCode { get; set; }
        public string Manufacturer { get; set; }
        public string Name { get; set; }
        public string PNPClass { get; set; }
        public string PNPDeviceID { get; set; }
        public ushort[] PowerManagementCapabilities { get; set; }
        public bool PowerManagementSupported { get; set; }
        public bool Present { get; set; }
        public string Service { get; set; }
        public string Status { get; set; }
        public ushort StatusInfo { get; set; }
        public string SystemCreationClassName { get; set; }
        public string SystemName { get; set; }

        public void Init()
        {
            System.Management.ManagementObject item = Tool.PC.SearchWMI("SELECT * FROM Win32_PnPEntity")[0];
            Service = Convert.ToString(item["Service"]);
            Availability = Convert.ToUInt16(item["Availability"]);
            SystemName = Convert.ToString(item["SystemName"]);
            Status = Convert.ToString(item["Status"]);
            Manufacturer = Convert.ToString(item["Manufacturer"]);
            StatusInfo = Convert.ToUInt16(item["StatusInfo"]);
            SystemCreationClassName = Convert.ToString(item["SystemCreationClassName"]);
            PowerManagementSupported = Convert.ToBoolean(item["PowerManagementSupported"]);
            Caption = Convert.ToString(item["Caption"]);
            ClassGuid = Convert.ToString(item["ClassGuid"]);
            CreationClassName = Convert.ToString(item["CreationClassName"]);
            DeviceID = Convert.ToString(item["DeviceID"]);
            Description = Convert.ToString(item["Description"]);
            ErrorCleared = Convert.ToBoolean(item["ErrorCleared"]);
            ErrorDescription = Convert.ToString(item["ErrorDescription"]);
            InstallDate = Convert.ToDateTime(item["InstallDate"]);
            LastErrorCode = Convert.ToBoolean(item["LastErrorCode"]);
            Name = Convert.ToString(item["Name"]);
            PNPClass = Convert.ToString(item["PNPClass"]);
            PNPDeviceID = Convert.ToString(item["PNPDeviceID"]);
            Present = Convert.ToBoolean(item["Present"]);
            HardwareID = (string[])item["HardwareID"];
            PowerManagementCapabilities = (ushort[])item["PowerManagementCapabilities"];
            ConfigManagerErrorCode = (uint)item["ConfigManagerErrorCode"];
            CompatibleID = (string[])item["CompatibleID"];
            ConfigManagerUserConfig = Convert.ToBoolean(item["ConfigManagerUserConfig"]);

        }

        public string PNPClassName
        {
            get
            {
                switch (PNPClass)
                {
                    case "Keyboard":
                        return "鼠标和其他指针设备";
                    case "HidClass":
                        return "鼠标和其他指针设备";
                    case "System":
                        return "系统设备";
                    case "PrintQueue":
                        return "打印队列";
                    case "Monitor":
                        return "监视器";
                    case "Net":
                        return "网络适配器";
                    case "Bluetooth":
                        return "蓝牙";
                    case "Volume":
                        return "卷";
                    case "SoftwareComponent":
                        return "软件组件";
                    case "Mouse":
                        return "鼠标和其他指针设备";
                    case "SCSIAdapter":
                        return "存储控制器";
                    default:
                        return PNPClass;



                }
            }
        }


    }
}


