using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TXQ.Utils.Tool;

namespace DEMO
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

   
            //Task.Run(() =>
            //{

            //    TXQ.Utils.SDK.HWiNFO.Init();

            //    var CPUPACKAGE = TXQ.Utils.SDK.HWiNFO.AllSensors.Where(X => X.SensorType == TXQ.Utils.SDK.HWiNFO.SensorType.SENSOR_TYPE_TEMP /*&& X.Model.Contains("CPU")*/).ToList();

            //    tPagerControl1.DataGridView.DataSource = CPUPACKAGE;

            //    while (true)
            //    {                  
            //        tPagerControl1.DataGridView.DataSource = CPUPACKAGE;

            //        foreach (var ITEM in CPUPACKAGE)
            //        {
            //            // LOG.INFO(ITEM.EXToJSON());
            //            LOG.INFO(ITEM.Model + "\t\t\t\t\t" + ITEM.Value);



            //            ITEM.ReInit();
            //        }
            //       Task.Delay(1000);

            //    }

            //});
        }


        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            TXQ.Utils.SDK.HWiNFO.Init();

            var CPUPACKAGE = TXQ.Utils.SDK.HWiNFO.AllSensors.Where(X => X.SensorType == TXQ.Utils.SDK.HWiNFO.SensorType.SENSOR_TYPE_TEMP /*&& X.Model.Contains("CPU")*/).ToList();
            LOG.INFO(CPUPACKAGE.EXToJSON());
           dataGridView1.DataSource = CPUPACKAGE;
        }
    }
}
