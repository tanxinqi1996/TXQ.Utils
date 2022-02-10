using System;
using System.Runtime.InteropServices;
using System.Text;

namespace TXQ.Utils.Tool
{
    public class INI
    {
        public INI(string configfile)
        {
            ConfigFile = configfile;
        }

        public string ConfigFile ;
        /// <summary>
        /// 读取INI文件值
        /// </summary>
        /// <param name="section">节点名</param>
        /// <param name="key">键</param>
        /// <param name="def">未取到值时返回的默认值</param>
        /// <param name="filePath">INI文件完整路径</param>
        /// <returns>读取的值</returns>
        public string Read(string section, string key, string def, bool SAVE = false)
        {
            StringBuilder sb = new StringBuilder(1024);
            GetPrivateProfileString(section, key, def, sb, 1024, ConfigFile);
            string R = sb.ToString();
            if (SAVE)
            {
                Write(section, key, R.ToString());
            }
            return R;
        }


        /// <summary>
        /// 读取INI文件值
        /// </summary>
        /// <param name="section">节点名</param>
        /// <param name="key">键</param>
        /// <param name="def">未取到值时返回的默认值</param>
        /// <param name="filePath">INI文件完整路径</param>
        /// <returns>读取的值</returns>
        public DateTime Read(string section, string key, DateTime def, bool SAVE = false)
        {
            StringBuilder sb = new StringBuilder(1024);
            GetPrivateProfileString(section, key, def.ToString("yyyy-MM-dd HH:mm:ss"), sb, 1024, ConfigFile);
            try
            {
                int R = Convert.ToInt32(sb.ToString());
                if (SAVE)
                {
                    Write(section, key, R.ToString());
                }
                return Convert.ToDateTime(R);
            }
            catch
            {
                return def;
            }
        }
        public int Read(string section, string key, int def, bool SAVE = false)
        {
            StringBuilder sb = new StringBuilder(1024);
            GetPrivateProfileString(section, key, def.ToString(), sb, 1024, ConfigFile);
            try
            {
                int R = Convert.ToInt32(sb.ToString());
                if (SAVE)
                {
                    Write(section, key, R.ToString());
                }
                return R;
            }
            catch
            {
                return def;
            }
        }

        public bool Read(string section, string key, bool def, bool SAVE = false)
        {
            StringBuilder sb = new StringBuilder(1024);
            GetPrivateProfileString(section, key, def.ToString(), sb, 1024, ConfigFile);
            try
            {
                int R = Convert.ToInt32(sb.ToString());
                if (SAVE)
                {
                    Write(section, key, R.ToString());
                }
                return R == 1;
            }
            catch
            {
                return def;
            }
        }


        public decimal Read(string section, string key, decimal def, bool SAVE = false)
        {
            StringBuilder sb = new StringBuilder(1024);
            GetPrivateProfileString(section, key, def.ToString(), sb, 1024, ConfigFile);
            try
            {
                var R = Convert.ToDecimal(sb.ToString());
                if (SAVE)
                {
                    Write(section, key, R.ToString());
                }
                return R;
            }
            catch
            {
                return def;
            }
        }

        /// <summary>
        /// 写INI文件值
        /// </summary>
        /// <param name="section">欲在其中写入的节点名称</param>
        /// <param name="key">欲设置的项名</param>
        /// <param name="value">要写入的新字符串</param>
        /// <param name="filePath">INI文件完整路径</param>
        /// <returns>非零表示成功，零表示失败</returns>
        public int Write(string section, string key, string value)
        {
            return WritePrivateProfileString(section, key, value, ConfigFile);
        }
        public int Write(string section, string key, int value)
        {
            return WritePrivateProfileString(section, key, value.ToString(), ConfigFile);
        }
        public int Write(string section, string key, decimal value)
        {
            return WritePrivateProfileString(section, key, value.ToString(), ConfigFile);
        }
        public int Write(string section, string key, DateTime value)
        {
            return WritePrivateProfileString(section, key, value.ToString("yyyy-MM-dd HH:mm:ss"), ConfigFile);
        }

        public int Write(string section, string key, bool value)
        {
            if (value)
            {
                return Write(section, key, 1);
            }
            else
            {
                return Write(section, key, 0);
            }
        }
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string lpAppName, string lpKeyName, string lpDefault, StringBuilder lpReturnedString, int nSize, string lpFileName);
        [DllImport("kernel32")]
        private static extern int WritePrivateProfileString(string lpAppName, string lpKeyName, string lpString, string lpFileName);
    }
}
