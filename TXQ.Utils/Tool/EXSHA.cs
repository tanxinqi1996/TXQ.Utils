using System.IO;
using System.Security.Cryptography;
using System.Text;

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
            byte[] hash = SHA1.Create().ComputeHash(input);
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                builder.Append(hash[i].ToString("x2"));
            }
            return builder.ToString();
        }


        public static string ExGetSha1(this FileInfo file)
        {
            using FileStream fileStream = File.OpenRead(file.FullName);
            byte[] hash = SHA1.Create().ComputeHash(fileStream);
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                builder.Append(hash[i].ToString("x2"));
            }
            return builder.ToString();
        }

        /// <summary>
        /// 获取Sha256Hash
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string EXGetSha256(this string input)
        {
            return Encoding.UTF8.GetBytes(input).EXGetSha256();
        }

        /// <summary>
        /// 获取Sha1
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string EXGetSha256(this byte[] input)
        {
            byte[] hash = SHA256.Create().ComputeHash(input);
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                builder.Append(hash[i].ToString("x2"));
            }
            return builder.ToString();
        }

        public static string ExGetSha256(this FileInfo file)
        {
            using FileStream fileStream = File.OpenRead(file.FullName);
            byte[] hash = SHA256.Create().ComputeHash(fileStream);
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                builder.Append(hash[i].ToString("x2"));
            }
            return builder.ToString();
        }


    }
}
