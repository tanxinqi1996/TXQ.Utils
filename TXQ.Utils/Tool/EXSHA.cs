using System;
using System.Collections.Generic;
using System.IO;
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
            using SHA1 SHA1 = SHA1Managed.Create();
            return Convert.ToBase64String(SHA1.ComputeHash(input));
        }

        /// <summary>
        /// 获取Sha256Hash
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string EXGetSha256(this string input)
        {
            byte[] SHA256Data = Encoding.UTF8.GetBytes(input);
            return SHA256Data.EXGetSha256();

        }

        /// <summary>
        /// 获取Sha1
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string EXGetSha256(this byte[] input)
        {
            using SHA256 SHA256 = SHA256Managed.Create();
            return Convert.ToBase64String(SHA256.ComputeHash(input));
        }

        public static string ExGetSha256(this FileInfo file)
        {
            using SHA256 SHA256 = SHA256Managed.Create();
            using FileStream fileStream = File.OpenRead(file.FullName);
            return Convert.ToBase64String(SHA256.ComputeHash(fileStream));
        }


    }
}
