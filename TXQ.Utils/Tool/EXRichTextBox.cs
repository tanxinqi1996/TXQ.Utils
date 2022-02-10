using System;
using System.Drawing;
using System.Windows.Forms;

namespace TXQ.Utils.Tool
{
    public static class EXRichTextBox
    {
        public static void AppendColorText(this RichTextBox rtb, string Text, Color color, bool AddNewLine = true)
        {
            rtb.Invoke(new EventHandler(delegate
            {
                rtb.SelectionStart = rtb.Text.Length;//设置插入符位置为文本框末
                rtb.SelectionColor = color;//设置文本颜色
                rtb.AppendText(Text);//输出文本，换行
                if (AddNewLine)
                    rtb.AppendText(Environment.NewLine);//换行
                rtb.ScrollToEnd();
            }));
        }

        public static void ScrollToEnd(this RichTextBox rtb)
        {
            rtb.Invoke(new EventHandler(delegate
            {
                rtb.SelectionStart = rtb.TextLength; //Set the current caret position at the end
                rtb.ScrollToCaret(); //Now scroll it automatically
            }));
        }
    }
}
