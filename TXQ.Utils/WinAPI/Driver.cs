using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TXQ.Utils.Tool;

namespace TXQ.Utils.WinAPI
{
    public class Driver
    {
        public string HardwareDescription { get; set; }
        public string HardwareId { get; set; }
        public string ManufacturerName { get; set; }
        public DateTime Date { get; set; }
        public string Version { get; set; }
        public string InfName { get; set; }
        public string Dictionary { get; set; }
        public string Class { get; set; }

        public string GUID => (Dictionary + HardwareId + InfName).EXStrToMD5();


    }
}
