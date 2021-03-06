using System;
using System.Windows.Forms;
using TXQ.Utils.Tool;

namespace TXQ.Utils.Controls.ConfigControl
{
    public class TNumericUpDown : NumericUpDown
    {
        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            if (_AutoSave)
            {
                if (this.FindForm() != null)
                {
                    this.Value = ExIni.Read(this.FindForm().GetType().FullName, this.Name, this.Value);
                }
                this.ValueChanged += new System.EventHandler(this.Change);
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
        private bool _AutoSave = true;
        public bool AutoSave
        {
            get => _AutoSave;
            set => _AutoSave = value;
        }
    }
}
