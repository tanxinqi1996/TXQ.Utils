using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TXQ.Utils.Tool
{
    public static class EXBase64
    {
        public static string EXStrToBase64(this string str)
        {
            byte[] data = Encoding.UTF8.GetBytes(str);
            return Convert.ToBase64String(data);
        }

        public static string EXBase64ToStr(this string Base64)
        {
            byte[] data = Convert.FromBase64String(Base64);
            return System.Text.Encoding.UTF8.GetString(data);

        }
    }
}
