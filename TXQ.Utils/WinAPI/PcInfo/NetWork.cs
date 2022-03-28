using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TXQ.Utils.WinAPI.PcInfo
{

    public static class NetWork
    {
        public static List<Model.Win32_NetworkAdapterConfiguration> NetworkConfigs
        {
            get
            {
                List<Model.Win32_NetworkAdapterConfiguration> list = new List<Model.Win32_NetworkAdapterConfiguration>();

                foreach (var item in WinAPI.Wmic.SearchWMI("SELECT * FROM Win32_NetworkAdapterConfiguration WHERE MACAddress!= NULL"))
                {



                    var cfg = new Model.Win32_NetworkAdapterConfiguration
                    {
                        MacAdress = Convert.ToString(item["MACAddress"]),
                        Caption = Convert.ToString(item["Caption"]),
                        Description = Convert.ToString(item["Description"]),
                        IPSubnet = (string[])(item["IPSubnet"]),
                        IPAddress = (string[])(item["IPAddress"]),
                        IPEnabled = Convert.ToBoolean(item["IPEnabled"])
                    };
                    cfg.Description = Convert.ToString(item["Description"]);
                    cfg.DHCPEnabled = Convert.ToBoolean(item["DHCPEnabled"]);
                    cfg.DHCPServer = Convert.ToString(item["DHCPServer"]);
                    cfg.Index = Convert.ToInt32(item["Index"]);

                    list.Add(cfg);
                }
                return list;
            }

        }
    }


}
