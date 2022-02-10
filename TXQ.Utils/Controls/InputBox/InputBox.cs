using System;
using System.Windows.Forms;

namespace TXQ.Utils.Controls
{
    public partial class InputBox : Form
    {
        /// <summary>
        /// 文本输入框
        /// </summary>
        /// <param name="boxtext">输入框文本</param>
        /// <param name="textbox"></param>
        public InputBox(string boxtext, string textbox = null)
        {

            InitializeComponent();
            Text = boxtext;
            richTextBox1.Text = textbox;
        }
        public delegate void TextEventHandler(string strText);

        public TextEventHandler TextHandler;
        private void Btnok_Click(object sender, EventArgs e)
        {
            if (null != TextHandler)
            {
                TextHandler.Invoke(richTextBox1.Text);
                DialogResult = DialogResult.OK;
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
