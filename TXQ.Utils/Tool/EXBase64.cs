using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TXQ.Utils.Tool
{
    public static class EXBase64
    {
        /// <summary>
        /// string to base64
        /// </summary>
        /// <param name="str">string</param>
        /// <returns></returns>
        public static string EXStrToBase64(this string str)
        {
            byte[] data = Encoding.UTF8.GetBytes(str);
            return Convert.ToBase64String(data);
        }
        /// <summary>
        /// base64 to string
        /// </summary>
        /// <param name="Base64">base64 string</param>
        /// <returns>string</returns>
        public static string EXBase64ToStr(this string Base64)
        {
            byte[] data = Convert.FromBase64String(Base64);
            return System.Text.Encoding.UTF8.GetString(data);

        }
        /// <summary>
        /// 图片转base64
        /// </summary>
        /// <param name="Image">图片</param>
        /// <returns>base64 string</returns>
        public static string EXImageToBase64(this Image Image)
        {
            MemoryStream memoryStream = new MemoryStream();
            Image.Save(memoryStream, Image.RawFormat);
            byte[] imageBytes = memoryStream.ToArray();
            return Convert.ToBase64String(imageBytes);
        }
        /// <summary>
        /// base64转string
        /// </summary>
        /// <param name="base64String">base64String</param>
        /// <returns>图片</returns>
        public static Image EXBase64ToImage(string base64String)
        {
            byte[] imageBytes = Convert.FromBase64String(base64String);
            MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);
            ms.Write(imageBytes, 0, imageBytes.Length);
            return Image.FromStream(ms, true);
        }

        /// <summary>
        /// 获取文件的Base64字符串
        /// </summary>
        /// <param name="filename">文件路径</param>
        /// <returns>Base64字符串</returns>
        public static string EXGetBase64(string filename)
        {
            var filestream = new FileStream(filename, FileMode.Open);
            byte[] bt = new byte[filestream.Length];
            filestream.Read(bt, 0, bt.Length);
            var base64Str = Convert.ToBase64String(bt);
            return base64Str;
        }

        /// <summary>
        /// Base64字符串转文件
        /// </summary>
        /// <param name="Base64str"><Base64str/param>
        /// <param name="filename">文件路径</param>
        public static void EXBase64ToFile(this string Base64str,string filename)
        {
            byte[] b = Convert.FromBase64String(Base64str);
            File.WriteAllBytes(filename, b);
        }

        /// <summary>
        /// 获取文件的Base64字符串
        /// </summary>
        /// <param name="fileInfo"></param>
        /// <returns>字符串</returns>
        public static string EXGetBase64(this FileInfo fileInfo)
        {
            return EXGetBase64(fileInfo.FullName);
        }


    }
}
