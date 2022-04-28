using System;
using System.Collections.Generic;

namespace TXQ.Utils.Tool
{
    public static class EXString
    {
        /// 指示指定的字符串是 null 还是 System.String.Empty 字符串 还是空白字符。 
        /// </summary>
        /// <param name="str"></param>
        /// <returns> </returns>
        public static bool EXIsNullOrEmptyOrWhiteSpace(this string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return true;
            }
            else if (string.IsNullOrWhiteSpace(str))
            {
                return true;
            }
            return false;
        }

        public static List<T> EXToList<T>(this string str, char split, Converter<string, T> convertHandler)
        {
            if (string.IsNullOrEmpty(str))
            {
                return new List<T>();
            }
            else
            {
                string[] arr = str.Split(split);
                T[] Tarr = Array.ConvertAll(arr, convertHandler);
                return new List<T>(Tarr);
            }
        }
        ///<summary>
        ///生成随机字符串 //转载请注明来自 http://www.uzhanbao.com
        ///</summary>
        ///<param name="length">目标字符串的长度</param>
        ///<param name="useNum">是否包含数字，1=包含，默认为包含</param>
        ///<param name="useLow">是否包含小写字母，1=包含，默认为包含</param>
        ///<param name="useUpp">是否包含大写字母，1=包含，默认为包含</param>
        ///<param name="useSpe">是否包含特殊字符，1=包含，默认为不包含</param>
        ///<param name="custom">要包含的自定义字符，直接输入要包含的字符列表</param>
        ///<returns>指定长度的随机字符串</returns>
        public static string GetRandString(int length, bool useNum, bool useLow, bool useUpp, bool useSpe, string custom)
        {
            byte[] b = new byte[4];
            new System.Security.Cryptography.RNGCryptoServiceProvider().GetBytes(b);
            Random r = new Random(BitConverter.ToInt32(b, 0));
            string s = null, str = custom;
            if (useNum == true) { str += "0123456789"; }
            if (useLow == true) { str += "abcdefghijklmnopqrstuvwxyz"; }
            if (useUpp == true) { str += "ABCDEFGHIJKLMNOPQRSTUVWXYZ"; }
            if (useSpe == true) { str += "!\"#$%&'()*+,-./:;<=>?@[\\]^_`{|}~"; }
            for (int i = 0; i < length; i++)
            {
                s += str.Substring(r.Next(0, str.Length - 1), 1);
            }
            return s;
        }
    }
}
