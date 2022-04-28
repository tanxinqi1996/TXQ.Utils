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

            tPagerControl1.DataGridView.DataSource = TXQ.Utils.Tool.PC.PCINFO.Memory;
            ExIni.Write("AA", "AA", DateTime.Now);
            LOG.INFO(ExIni.Read("AA", "AA", DateTime.Now).ToString());
        }


        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
        }
    }
}
