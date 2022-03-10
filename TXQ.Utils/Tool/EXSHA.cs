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
            return inputBytes.EXGetSha1();
        }

        /// <summary>
        /// 获取Sha1
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string EXGetSha1(this byte[] input)
        {
            byte[] result = SHA1.Create().ComputeHash(input);
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
            return inputBytes.EXGetSha256();
        }

        /// <summary>
        /// 获取Sha1
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string EXGetSha256(this byte[] input)
        {
            byte[] result = SHA256.Create().ComputeHash(input);
            StringBuilder sBuilder = new StringBuilder();
            foreach (var ITEM in result)
            {
                sBuilder.Append(ITEM.ToString("x2"));
            }
            return sBuilder.ToString().ToUpper();
        }


    }
}
