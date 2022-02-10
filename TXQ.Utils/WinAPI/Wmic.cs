using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;

namespace TXQ.Utils.WinAPI
{
    public static class Wmic
    {
        public static List<ManagementObject> SearchWMI(string SELECTSTR, string NAMESPACE = "root\\CIMV2")
        {
            List<ManagementObject> List = new List<ManagementObject>();
            var DATA = new ManagementObjectSearcher(NAMESPACE, SELECTSTR).Get();
            foreach (ManagementObject queryObj in DATA)
            {
                List.Add(queryObj);
            }
            return List;
        }
        public static List<WinAPI.PnPEntity> Win32_PnPEntity()
        {
            List<WinAPI.PnPEntity> list = new List<PnPEntity>();
            foreach (var item in Tool.PC.SearchWMI("SELECT * FROM Win32_PnPEntity"))
            {
                var DATA = new PnPEntity();
                DATA.Service = Convert.ToString(item["Service"]);
                DATA.Availability = Convert.ToUInt16(item["Availability"]);
                DATA.SystemName = Convert.ToString(item["SystemName"]);
                DATA.Status = Convert.ToString(item["Status"]);
                DATA.Manufacturer = Convert.ToString(item["Manufacturer"]);
                DATA.StatusInfo = Convert.ToUInt16(item["StatusInfo"]);
                DATA.SystemCreationClassName = Convert.ToString(item["SystemCreationClassName"]);
                DATA.PowerManagementSupported = Convert.ToBoolean(item["PowerManagementSupported"]);
                DATA.Caption = Convert.ToString(item["Caption"]);
                DATA.ClassGuid = Convert.ToString(item["ClassGuid"]);
                DATA.CreationClassName = Convert.ToString(item["CreationClassName"]);
                DATA.DeviceID = Convert.ToString(item["DeviceID"]);
                DATA.Description = Convert.ToString(item["Description"]);
                DATA.ErrorCleared = Convert.ToBoolean(item["ErrorCleared"]);
                DATA.ErrorDescription = Convert.ToString(item["ErrorDescription"]);
                DATA.InstallDate = Convert.ToDateTime(item["InstallDate"]);
                DATA.LastErrorCode = Convert.ToBoolean(item["LastErrorCode"]);
                DATA.Name = Convert.ToString(item["Name"]);
                DATA.PNPClass = Convert.ToString(item["PNPClass"]);
                DATA.PNPDeviceID = Convert.ToString(item["PNPDeviceID"]);
                DATA.Present = Convert.ToBoolean(item["Present"]);
                DATA.HardwareID = (string[])item["HardwareID"];
                DATA.PowerManagementCapabilities = (ushort[])item["PowerManagementCapabilities"];
                DATA.ConfigManagerErrorCode = (uint)item["ConfigManagerErrorCode"];
                DATA.CompatibleID = (string[])item["CompatibleID"];
                DATA.ConfigManagerUserConfig = Convert.ToBoolean(item["ConfigManagerUserConfig"]);
                list.Add(DATA);
            }

            return list;

        }
    }
}
