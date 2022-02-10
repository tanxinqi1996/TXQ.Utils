using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace TXQ.Utils.Tool
{
    public static class EXSHA
    {
        /// <summary>
        /// 获取Sha1
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string EXGetSha1(this string input)
        {
            byte[] inputBytes = Encoding.UTF8.GetBytes(input);
            byte[] result = new SHA1CryptoServiceProvider().ComputeHash(inputBytes);
            StringBuilder sBuilder = new StringBuilder();
            foreach (var ITEM in result)
            {
                sBuilder.Append(ITEM.ToString("x2"));
            }
            return sBuilder.ToString().ToUpper();
        }

        /// <summary>
        /// 获取Sha256Hash
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string EXGetSha256(this string input)
        {
            byte[] inputBytes = Encoding.UTF8.GetBytes(input);
            byte[] result = new SHA256CryptoServiceProvider().ComputeHash(inputBytes);
            StringBuilder sBuilder = new StringBuilder();
            foreach (var ITEM in result)
            {
                sBuilder.Append(ITEM.ToString("x2"));
            }
            return sBuilder.ToString().ToUpper();
        }

    }
}
