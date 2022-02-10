using Microsoft.Dism;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TXQ.Utils.Tool;

namespace TXQ.Utils.WinAPI
{
    public static class DriverTool
    {
        /// <summary>
        /// 获取驱动信息 路径必须是完整路径
        /// </summary>
        /// <param name="InfPath">完整的INF文件路径</param>
        /// <returns></returns>
        public static List<Driver> GetDriverInfo(string InfPath)
        {
            DismApi.Initialize(DismLogLevel.LogErrors);
            List<Driver> DATA = new List<Driver>();
            var ini = new Tool.INI(InfPath);
            string DriverVer = ini.Read("Version", "DriverVer", "01/01/2000,1.1.1.1");


            DateTime date = Convert.ToDateTime(DriverVer.Split(',')[0]);
            //,号分割时间和版本号，在按照;分割版本号后面的注释
            string version = DriverVer.Split(',')[1].Split(';')[0].Trim();
            string infname = InfPath.Split('\\').Last();
            string dir = new FileInfo(InfPath).DirectoryName.Replace(Application.StartupPath+"\\",null);
            //;分割注释
            string Class = ini.Read("Version", "Class", "").Split(';')[0].Trim();
            var DRVINFO = DismApi.GetDriverInfo(DismApi.OpenOfflineSession(DismApi.DISM_ONLINE_IMAGE), InfPath);
            foreach (var ITEM in DRVINFO)
            {
                DATA.Add(new Driver()
                {
                    HardwareDescription = ITEM.HardwareDescription,
                    HardwareId = ITEM.HardwareId,
                    ManufacturerName = ITEM.ManufacturerName,
                    InfName = infname,
                    Version = version,
                    Dictionary = dir,
                    Date = date,
                    Class = Class
                });
            }
            return DATA;

        }



        public static void GetDriverInfoDism(string InfPath)
        {
            Driver driver = new Driver();
            string str = TXQ.Utils.Tool.CMD.RunCMDGetStdout($"DISM /ONLINE /GET-DRIVERINFO /DRIVER:\"{InfPath}\" /ENGLISH");
            foreach (var item in str.Split('\n'))
            {


            }

        }
    }
}
