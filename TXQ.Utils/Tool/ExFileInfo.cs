using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TXQ.Utils.Tool
{
    public static class ExFileInfo
    {
        /// <summary>
        /// 使用递归的方式获取目录下的所有文件,包含子目录
        /// </summary>
        /// <param name="directoryInfo">目录</param>
        /// <returns>所有文件</returns>
        public static byte[] EXGetByte(this FileInfo fileInfo)
        {
            using (FileStream fs = new FileStream(fileInfo.FullName, FileMode.Open, FileAccess.Read))
            {
                byte[] buffur = new byte[fs.Length];
                using (BinaryWriter bw = new BinaryWriter(fs))
                {
                    bw.Write(buffur);
                    bw.Close();
                }
                return buffur;
            }

        }
    }
}
