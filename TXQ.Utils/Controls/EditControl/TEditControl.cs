using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
namespace TXQ.Utils.Controls
{
    public partial class TEditControl : UserControl
    {
        public TEditControl()
        {
            InitializeComponent();
        }
        public TEditControl(Dictionary<string, object> dic)
        {
            InitializeComponent();
            LoadControls(dic);
        }
        private TableLayoutPanel tableLayoutPanel1;
        public void LoadControls(Dictionary<string, object> dic)
        {
            _Dictionary = dic;
            if (tableLayoutPanel1 == null)
            {
                tableLayoutPanel1 = new TableLayoutPanel()
                {
                    ColumnCount = 2,
                    RowCount = dic.Count + 1,
                    Dock = DockStyle.Fill,
                    AutoSize = true,
                };
            }
            else
            {
                tableLayoutPanel1.Controls.Clear();
            }
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            Controls.Add(tableLayoutPanel1);
            int i = 0;
            foreach (KeyValuePair<string, object> ITEM in dic)
            {
                Label LAB = new Label()
                {
                    Name = "LAB" + ITEM.Key,
                    Text = ITEM.Key,
                    Dock = DockStyle.Fill,
                    AutoSize = true,
                };
                LAB.TextAlign = ContentAlignment.MiddleCenter;
                TTextBox INPUT = new TTextBox()
                {
                    Name = "TXT" + ITEM.Key,
                    Text = ITEM.Value.ToString(),
                    Dock = DockStyle.Fill
                };
                tableLayoutPanel1.Controls.Add(LAB, 0, i);
                tableLayoutPanel1.Controls.Add(INPUT, 1, i);
                i++;
            }
            tableLayoutPanel1.Controls.Add(new Label()
            {
                Dock = DockStyle.Fill,
                AutoSize = true,
                TextAlign = System.Drawing.ContentAlignment.MiddleRight
            }, 0, i);
        }
        private Dictionary<string, object> _Dictionary = new Dictionary<string, object>();
        public Dictionary<string, object> GetDictionary()
        {
            Dictionary<string, object> keys = new Dictionary<string, object>();
            foreach (KeyValuePair<string, object> item in _Dictionary)
            {
                keys.Add(item.Key, tableLayoutPanel1.Controls[$"TXT{item.Key}"].Text);
            }
            return keys;
        }
        public void SetDictionary(Dictionary<string, object> value)
        {
            StringBuilder str = new StringBuilder();
            foreach (KeyValuePair<string, object> item in value)
            {
                try
                {
                    Control INPUT = tableLayoutPanel1.Controls[$"TXT{item.Key}"];
                    if (INPUT == null)
                    {
                        throw new Exception($"找不到指定的键值    {item.Key}");
                    }
                    else
                    {
                        INPUT.Text = item.Value.ToString();
                    }
                }
                catch (System.Exception EX)
                {
                    str.Append(EX.Message + Environment.NewLine);
                }
            }
            if (str.Length > 1)
            {
                MessageBox.Show(str.ToString(), "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
