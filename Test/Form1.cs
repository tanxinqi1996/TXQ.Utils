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
           TXQ.Utils.Tool.CMD.SystemCmd("ECHO ������sad��������\r\npause");
        }
    }
}