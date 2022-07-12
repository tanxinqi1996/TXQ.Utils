using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TXQ.Utils.WinAPI
{
    public static class SmartCtl
    {
        static SmartCtl()
        {
            string path = Environment.CurrentDirectory + "\\Lib\\smartctl.exe";
            string dir = Environment.CurrentDirectory + "\\Lib";
            if (System.IO.File.Exists(path) == false)
            {
                if (System.IO.Directory.Exists(dir) == false)
                {
                    System.IO.Directory.CreateDirectory(dir);
                }
                FileStream FS = new FileStream(path, FileMode.CreateNew);
                FS.Write(TXQ.Utils.Properties.Resources.smartctl, 0, TXQ.Utils.Properties.Resources.smartctl.Length);
                FS.Close();
            }
        }
        private static List<string> GetAllDisks()
        {
            List<string> list = new List<string>();
            try
            {
                (int CODE, string str) = Tool.CMD.RunCMD(Environment.CurrentDirectory + "/Lib/smartctl", "--scan -j");
                JObject properties = JObject.Parse(str);
                properties.SelectToken("devices").ToArray();
                foreach (JToken item in properties.SelectToken("devices").ToArray())
                {
                    list.Add(item["name"].ToString());
                }
                return list;
            }
            catch (Exception ex)
            {
                TXQ.Utils.Tool.LOG.ERROR(ex.Message);
                return list;
            }
        }

        public static List<Model.SmartInfo> GetAllSmartInfos()
        {
            List<Model.SmartInfo> smartInfos = new List<Model.SmartInfo>();
            foreach (string item in GetAllDisks())
            {
                string reading = null;
                try
                {
                    (int CODE, string str) = Tool.CMD.RunCMD(Environment.CurrentDirectory + "/Lib/smartctl", @$"-a {item} -j");
                    reading = "json";
                    JObject json = JObject.Parse(str);
                    Model.SmartInfo smartInfo = new Model.SmartInfo();
                    reading = "power_cycle_count";
                    smartInfo.PowerCycleCount = (int)json.SelectToken("power_cycle_count");
                    reading = "power_on_time";
                    smartInfo.PowerOnHours = (int)json.SelectToken("power_on_time").SelectToken("hours");
                    reading = "model_name";
                    smartInfo.ModelName = (string)json.SelectToken("model_name");
                    smartInfos.Add(smartInfo);

                }
                catch (Exception ex)
                {
                    Tool.LOG.ERROR($"Reading {reading} Fail\r\n{ex.Message}");
                    continue;
                }
            }
            return smartInfos;
        }

    }
}
