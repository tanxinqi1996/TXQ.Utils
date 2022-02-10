using System.Runtime.InteropServices;
using System.Windows.Forms;
namespace TXQ.Utils.Tool
{
    public static class KeyBoard
    {
        [DllImport("user32.dll")]
        public static extern short GetKeyState(int keyCode);
        [DllImport("user32.dll", EntryPoint = "keybd_event", SetLastError = true)]
        public static extern void Keybd_event(Keys bVk, byte bScan, uint dwFlags, uint dwExtraInfo);
        /// <summary>
        /// 打开小键盘
        /// </summary>
        public static void EnableNumLock()
        {
            bool NumLock = (((ushort)GetKeyState(0x90)) & 0xffff) == 1;
            if (NumLock == false)
            {
                Keybd_event(Keys.NumLock, 0, 0, 0);
            };
        }
    }
}

