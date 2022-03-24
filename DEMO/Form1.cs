﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DEMO
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            TXQ.Utils.Tool.PC.PCINFO.Init();
            var D = TXQ.Utils.Tool.PC.PCINFO.Disk;
            tPagerControl1.DrawControl(D.Count, D);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            tPagerControl1.DataGridView.Columns[0].HeaderText = "aa";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            TXQ.Utils.Tool.PC.PCINFO.Init();
            var D = TXQ.Utils.Tool.PC.PCINFO.Disk;
            tPagerControl1.DrawControl(D.Count,D);

        }
    }
}
