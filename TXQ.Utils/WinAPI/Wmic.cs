using System;
using System.Collections.Generic;
using System.Management;

namespace TXQ.Utils.WinAPI
{
    public static class Wmic
    {
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
        public static List<WinAPI.PnPEntity> Win32_PnPEntity()
        {
            List<WinAPI.PnPEntity> list = new List<PnPEntity>();
            foreach (ManagementObject item in Tool.PC.SearchWMI("SELECT * FROM Win32_PnPEntity"))
            {
                PnPEntity DATA = new PnPEntity
                {
                    Service = Convert.ToString(item["Service"]),
                    Availability = Convert.ToUInt16(item["Availability"]),
                    SystemName = Convert.ToString(item["SystemName"]),
                    Status = Convert.ToString(item["Status"]),
                    Manufacturer = Convert.ToString(item["Manufacturer"]),
                    StatusInfo = Convert.ToUInt16(item["StatusInfo"]),
                    SystemCreationClassName = Convert.ToString(item["SystemCreationClassName"]),
                    PowerManagementSupported = Convert.ToBoolean(item["PowerManagementSupported"]),
                    Caption = Convert.ToString(item["Caption"]),
                    ClassGuid = Convert.ToString(item["ClassGuid"]),
                    CreationClassName = Convert.ToString(item["CreationClassName"]),
                    DeviceID = Convert.ToString(item["DeviceID"]),
                    Description = Convert.ToString(item["Description"]),
                    ErrorCleared = Convert.ToBoolean(item["ErrorCleared"]),
                    ErrorDescription = Convert.ToString(item["ErrorDescription"]),
                    InstallDate = Convert.ToDateTime(item["InstallDate"]),
                    LastErrorCode = Convert.ToBoolean(item["LastErrorCode"]),
                    Name = Convert.ToString(item["Name"]),
                    PNPClass = Convert.ToString(item["PNPClass"]),
                    PNPDeviceID = Convert.ToString(item["PNPDeviceID"]),
                    Present = Convert.ToBoolean(item["Present"]),
                    HardwareID = (string[])item["HardwareID"],
                    PowerManagementCapabilities = (ushort[])item["PowerManagementCapabilities"],
                    ConfigManagerErrorCode = (uint)item["ConfigManagerErrorCode"],
                    CompatibleID = (string[])item["CompatibleID"],
                    ConfigManagerUserConfig = Convert.ToBoolean(item["ConfigManagerUserConfig"])
                };
                list.Add(DATA);
            }

            return list;

        }
    }
}
