using System;
using System.Drawing.Printing;
using System.Windows.Forms;
using TXQ.Utils.Tool;

namespace TXQ.Utils.Controls
{
    public class TPrinterComboBox : ComboBox
    {
        public TPrinterComboBox()
        {
            InitializeComponent();
        }
        private static void InitializeComponent()
        {
            //  this.DropDownStyle = ComboBoxStyle.DropDownList;
        }
        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            ReLoadPrinterList();
        }

        private void ReLoadPrinterList()
        {
            this.Items.Clear();
            System.Collections.Generic.List<System.Management.ManagementObject> printerlist = TXQ.Utils.Tool.PC.GetWMIObjList("Win32_Printer");
            foreach (System.Management.ManagementObject item in printerlist)
            {
                this.Items.Add(item["Name"].ToString());
            }
            this.Items.Add("加载打印机列表");
            if (this.FindForm() != null)
            {
                string printer = Global.INI.Read(this.FindForm().GetType().FullName, this.Name, DefaultPrinter);
                this.Text = printer;
            }
        }

        protected override void OnControlAdded(ControlEventArgs e)
        {
            base.OnControlAdded(e);

        }
        public static string DefaultPrinter => new PrintDocument().PrinterSettings.PrinterName;
        protected override void OnSelectedIndexChanged(EventArgs e)
        {
            base.OnSelectedIndexChanged(e);
            if (this.Text == "加载打印机列表")
            {
                ReLoadPrinterList();
                return;
            }

            if (this.FindForm() != null)
            {
                Global.INI.Write(this.FindForm().GetType().FullName, this.Name, this.Text);
            }

        }


    }
}
