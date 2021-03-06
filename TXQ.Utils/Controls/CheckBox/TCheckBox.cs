using System;
using TXQ.Utils.Tool;

namespace TXQ.Utils.Controls
{
    public class TCheckBox : System.Windows.Forms.CheckBox
    {
        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            if (_AutoSave)
            {
                if (this.FindForm() != null)
                {
                    this.Checked = ExIni.Read(this.FindForm().GetType().FullName, this.Name, true);
                }
                this.CheckedChanged += new System.EventHandler(this.Change);
            }

        }
        private void Change(object sender, EventArgs e)
        {
            if (_AutoSave)
            {
                if (this.FindForm() != null)
                {
                    ExIni.Write(this.FindForm().GetType().FullName, this.Name, this.Checked);
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
