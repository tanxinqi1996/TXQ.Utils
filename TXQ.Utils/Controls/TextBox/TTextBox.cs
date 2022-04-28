using System;
using System.Drawing;
using System.Windows.Forms;
using TXQ.Utils.Tool;

namespace TXQ.Utils.Controls
{
    public class TTextBox : System.Windows.Forms.TextBox
    {
        public TTextBox()
        {
            InitializeComponent();
        }
        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.ResumeLayout(false);

        }

        private bool _AutoSave = false;

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            if (_AutoSave)
            {
                if (this.FindForm() != null)
                {
                    this.Text = ExIni.Read(this.FindForm().GetType().FullName, this.Name, this.Text);
                }
                this.TextChanged += new System.EventHandler(this.Change);
            }
        }
        private void Change(object sender, EventArgs e)
        {
            if (_AutoSave)
            {
                if (this.FindForm() != null)
                {
                    ExIni.Write(this.FindForm().GetType().FullName, this.Name, this.Text);
                }
            }
        }
        private string hint;
        private Color WaterColor = System.Drawing.SystemColors.GrayText;
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if (m.Msg == 0xf && string.IsNullOrEmpty(this.Text) && !string.IsNullOrEmpty(this.WaterMarkText))
            {
                using (Graphics g = this.CreateGraphics())
                {
                    SizeF sizeF = g.MeasureString(this.WaterMarkText, this.Font);
                    Point TextPonit = new Point((int)(this.Width / 2 - sizeF.Width / 2), (int)(this.Height / 2 - sizeF.Height / 2));
                    TextRenderer.DrawText(g, this.WaterMarkText, this.Font, TextPonit, this.WaterMarkColor, this.BackColor);
                }
            }
        }
        /// <summary>
        /// 水印文字
        /// </summary>
        public string WaterMarkText
        {
            get => hint;
            set
            {
                hint = value;
                this.Invalidate();
            }
        }
        /// <summary>
        /// 水印文字颜色
        /// </summary>
        public Color WaterMarkColor
        {
            get => WaterColor;
            set
            {
                WaterColor = value;
                Invalidate();
            }
        }

        /// <summary>
        /// 自动保存值
        /// </summary>
        public bool AutoSave
        {
            get => _AutoSave;
            set => _AutoSave = value;
        }
    }
}
