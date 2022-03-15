using System.Diagnostics;
using System.Text;

namespace Test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
           TXQ.Utils.Tool.CMD.SystemCmd("ECHO 奥赛的sad撒多撒多\r\npause");
        }
    }
}