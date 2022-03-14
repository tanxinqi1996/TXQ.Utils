using System;
using System.Runtime.InteropServices;
using System.Text;

namespace TXQ.Utils.Tool
{
    public static class ExIni
    {
        /// <summary>
        /// 默认配置文件路径
        /// </summary>
        public static string ConfigFile => System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName + ".ini";

        /// <summary>
        /// 读取INI文件值
        /// </summary>
        /// <param name="section">节点[]</param>
        /// <param name="key">键=</param>
        /// <param name="def">读取失败的默认值</param>
        /// <param name="save">读取时保存</param>
        /// <param name="path">配置文件路径，null则使用默认值</param>
        /// <returns>读取到的值</returns>
        public static string Read(string section, string key, string def, bool save = false, string path = null)
        {
            StringBuilder sb = new StringBuilder(1024);
            GetPrivateProfileString(section, key, def, sb, 1024, path ?? ConfigFile);
            string R = sb.ToString();
            if (save)
            {
                Write(section, key, R.ToString());
            }
            return R;
        }


        /// <summary>
        /// 读取INI文件值
        /// </summary>
        /// <param name="section">节点[]</param>
        /// <param name="key">键=</param>
        /// <param name="def">读取失败的默认值</param>
        /// <param name="save">读取时保存</param>
        /// <param name="path">配置文件路径，null则使用默认值</param>
        /// <returns>读取到的值</returns>
        public static DateTime Read(string section, string key, DateTime def, bool save = false, string path = null)
        {
            StringBuilder sb = new StringBuilder(1024);
            GetPrivateProfileString(section, key, def.ToString("yyyy-MM-dd HH:mm:ss"), sb, 1024, path ?? ConfigFile);
            try
            {
                int R = Convert.ToInt32(sb.ToString());
                if (save)
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

        /// <summary>
        /// 读取INI文件值
        /// </summary>
        /// <param name="section">节点[]</param>
        /// <param name="key">键=</param>
        /// <param name="def">读取失败的默认值</param>
        /// <param name="save">读取时保存</param>
        /// <param name="path">配置文件路径，null则使用默认值</param>
        /// <returns>读取到的值</returns>
        public static int Read(string section, string key, int def, bool save = false, string path = null)
        {
            StringBuilder sb = new StringBuilder(1024);
            GetPrivateProfileString(section, key, def.ToString(), sb, 1024, path ?? ConfigFile);
            try
            {
                int R = Convert.ToInt32(sb.ToString());
                if (save)
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
        /// 读取INI文件值
        /// </summary>
        /// <param name="section">节点[]</param>
        /// <param name="key">键=</param>
        /// <param name="def">读取失败的默认值</param>
        /// <param name="save">读取时保存</param>
        /// <param name="path">配置文件路径，null则使用默认值</param>
        /// <returns>读取到的值</returns>
        public static bool Read(string section, string key, bool def, bool save = false, string path = null)
        {
            StringBuilder sb = new StringBuilder(1024);
            GetPrivateProfileString(section, key, def.ToString(), sb, 1024, path ?? ConfigFile);
            try
            {
                int R = Convert.ToInt32(sb.ToString());
                if (save)
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

        /// <summary>
        /// 读取INI文件值
        /// </summary>
        /// <param name="section">节点[]</param>
        /// <param name="key">键=</param>
        /// <param name="def">读取失败的默认值</param>
        /// <param name="save">读取时保存</param>
        /// <param name="path">配置文件路径，null则使用默认值</param>
        /// <returns>读取到的值</returns>
        public static decimal Read(string section, string key, decimal def, bool save = false, string path = null)
        {
            StringBuilder sb = new StringBuilder(1024);
            GetPrivateProfileString(section, key, def.ToString(), sb, 1024, path ?? ConfigFile);
            try
            {
                var R = Convert.ToDecimal(sb.ToString());
                if (save)
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
        /// 写入INI文件值
        /// </summary>
        /// <param name="section">节点[]</param>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="path">配置文件路径，null则使用默认值</param>
        /// <returns>成功或失败</returns>
        public static int Write(string section, string key, string value, string path = null)
        {
            return WritePrivateProfileString(section, key, value, path ?? ConfigFile);
        }
        /// <summary>
        /// 写入INI文件值
        /// </summary>
        /// <param name="section">节点[]</param>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="path">配置文件路径，null则使用默认值</param>
        /// <returns>成功或失败</returns>
        public static int Write(string section, string key, int value, string path = null)
        {
            return WritePrivateProfileString(section, key, value.ToString(), path ?? ConfigFile);
        }
        /// <summary>
        /// 写入INI文件值
        /// </summary>
        /// <param name="section">节点[]</param>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="path">配置文件路径，null则使用默认值</param>
        /// <returns>成功或失败</returns>
        public static int Write(string section, string key, decimal value, string path = null)
        {
            return WritePrivateProfileString(section, key, value.ToString(), path ?? ConfigFile);
        }

        /// <summary>
        /// 写入INI文件值
        /// </summary>
        /// <param name="section">节点[]</param>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="path">配置文件路径，null则使用默认值</param>
        /// <returns>成功或失败</returns>
        public static int Write(string section, string key, DateTime value, string path = null)
        {
            return WritePrivateProfileString(section, key, value.ToString("yyyy-MM-dd HH:mm:ss"), path ?? ConfigFile);
        }

        /// <summary>
        /// 写入INI文件值
        /// </summary>
        /// <param name="section">节点[]</param>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="path">配置文件路径，null则使用默认值</param>
        /// <returns>成功或失败</returns>
        public static int Write(string section, string key, bool value, string path = null)
        {
            if (value)
            {
                return Write(section, key, 1, path ?? ConfigFile);
            }
            else
            {
                return Write(section, key, 0, path ?? ConfigFile);
            }
        }


        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string lpAppName, string lpKeyName, string lpDefault, StringBuilder lpReturnedString, int nSize, string lpFileName);
        [DllImport("kernel32")]
        private static extern int WritePrivateProfileString(string lpAppName, string lpKeyName, string lpString, string lpFileName);
    }
}
