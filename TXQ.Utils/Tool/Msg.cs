using System.Windows.Forms;

namespace TXQ.Utils.Tool
{
    public static class Msg
    {

        /// <summary>
        /// 弹出文本输入框
        /// </summary>
        /// <param name="strText">返回内容</param>
        /// <param name="text">标题</param>
        /// <param name="tboxtext">文本框内容</param>
        /// <returns></returns>
        public static bool InputBox(out string strText, string text = null, string tboxtext = null)
        {
            string strTemp = string.Empty;

            using (Controls.InputBox inputBox = new Controls.InputBox(text, tboxtext)
            {
                Text = text,
                TextHandler = (str) => { strTemp = str; }
            })
            {
                DialogResult result = inputBox.ShowDialog();
                strText = strTemp;
                if (result == DialogResult.OK) { return true; }
                else
                {
                    return false;
                }
            }
        }
    }
}
