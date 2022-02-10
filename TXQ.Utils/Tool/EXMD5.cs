using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TXQ.Utils.Tool
{
    public static class EXMD5
    {
        /// <summary>
        /// string 转 MD5
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string EXStrToMD5(this string str)
        {
            StringBuilder sb = new StringBuilder();
            System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] source = md5.ComputeHash(Encoding.UTF8.GetBytes(str));
            for (int i = 0; i < source.Length; i++)
            {
                sb.Append(source[i].ToString("x2"));
            }
            return sb.ToString();
        }
    }
}
