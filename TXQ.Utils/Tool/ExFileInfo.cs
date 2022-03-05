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
        /// 获取文件的Byte
        /// </summary>
        /// <param name="fileInfo"></param>
        /// <returns></returns>
        public static byte[] EXGetByte(this FileInfo fileInfo)
        {
            using (FileStream fs = new FileStream(fileInfo.FullName, FileMode.OpenOrCreate, FileAccess.ReadWrite))
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
