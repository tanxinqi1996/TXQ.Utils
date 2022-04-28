using System;
using System.Text;

namespace TXQ.Utils.Tool
{
    public static class EXRandom
    {
        /// <summary>
        /// 生成随机数
        /// </summary>
        /// <param name="num">长度</param>
        /// <returns>随机数</returns>
        public static int CreateNum(int num)
        {
            Random random = new Random(Guid.NewGuid().GetHashCode());
            StringBuilder str = new StringBuilder();
            for (int i = 0; i < num; i++)
            {
                if (i == 0)
                {
                    str.Append(random.Next(1, 10).ToString());
                }
                else
                {
                    str.Append(random.Next(10).ToString());

                }
            }
            return Convert.ToInt32(str.ToString());
        }

        /// <summary>
        /// 生成随机大写字母
        /// </summary>
        /// <param name="num">长度</param>
        /// <returns><随机大写字母/returns>
        public static string CreateBigAbc(int num)
        {
            StringBuilder str = new StringBuilder();
            Random random = new Random(Guid.NewGuid().GetHashCode());
            for (int i = 0; i < num; i++)
            {
                //A-Z的 ASCII值为65-90
                str.Append(Convert.ToChar(random.Next(65, 91)).ToString());
            }
            return str.ToString();
        }

        /// <summary>
        /// 生成随机小写字母
        /// </summary>
        /// <param name="num">长度</param>
        /// <returns>随机小写字母</returns>
        public static string CreateSmallAbc(int num)
        {
            //a-z的 ASCII值为97-122
            StringBuilder str = new StringBuilder();
            Random random = new Random(Guid.NewGuid().GetHashCode());
            for (int i = 0; i < num; i++)
            {
                //A-Z的 ASCII值为65-90
                str.Append(Convert.ToChar(random.Next(97, 123)).ToString());
            }
            return str.ToString();
        }


        public static string CreateString(int Lenth, bool Use0_9, bool Usea_c, bool UseA_Z)
        {
            StringBuilder str = new StringBuilder();
            Random random = new Random(Guid.NewGuid().GetHashCode());

            return str.ToString();
        }

    }
}
