using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace TXQ.Utils.Tool
{
    /// <summary>
    /// 快捷键
    /// </summary>
    public static class HotKeys
    {
        //引入系统API
        [DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, int modifiers, Keys vk);
        [DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);


        private static int keyid = 10;     //区分不同的快捷键
        private static readonly Dictionary<int, HotKeyCallBackHanlder> keymap = new Dictionary<int, HotKeyCallBackHanlder>();   //每一个key对于一个处理函数
        public delegate void HotKeyCallBackHanlder();

        /// <summary>
        /// Alt/Ctrl/Shift/Win组合键值
        /// </summary>
        public enum HotkeyModifiers
        {
            Alt = 1,
            Control = 2,
            Shift = 4,
            Win = 8
        }
        /// <summary>
        /// 注册快捷键
        /// </summary>
        /// <param name="hWnd">this.handel</param>
        /// <param name="modifiers"></param>
        /// <param name="vk">按键</param>
        /// <param name="callBack">快捷键调用的函数</param>
        public static void Regist(IntPtr hWnd, int modifiers, Keys vk, HotKeyCallBackHanlder callBack)
        {
            int id = keyid++;
            if (!RegisterHotKey(hWnd, id, modifiers, vk))
            {
                throw new Exception("注册失败");
            }
            keymap[id] = callBack;
        }

        /// <summary>
        /// 注销快捷键
        /// </summary>
        /// <param name="hWnd">this.handel</param>
        /// <param name="callBack">快捷键调用的函数</param>
        public static void UnRegist(IntPtr hWnd, HotKeyCallBackHanlder callBack)
        {
            foreach (KeyValuePair<int, HotKeyCallBackHanlder> var in keymap)
            {
                if (var.Value == callBack)
                {
                    if (!UnregisterHotKey(hWnd, var.Key))
                    {
                        throw new Exception("注册失败");
                    }

                    return;
                }
            }
        }

        /* protected override void WndProc(ref Message m)
        {
            Core.Win32API.HotKeys.ProcessHotKey(m);
            base.WndProc(ref m);
        } */
        /// <summary>
        /// 使用上面的方法在窗体中传递参数到这里
        /// </summary>
        /// <param name="m"></param>
        public static void ProcessHotKey(Message m)
        {
            if (m.Msg == 0x312)
            {
                int id = m.WParam.ToInt32();
                if (keymap.TryGetValue(id, out HotKeyCallBackHanlder callback))
                {
                    callback();
                }
            }
        }
    }
}
