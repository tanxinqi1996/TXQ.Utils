﻿using System;

namespace TXQ.Utils.Tool
{
    /// <summary>RC4对称加密算法</summary>
    /// <remarks>
    /// RC4于1987年提出，和DES算法一样，是一种对称加密算法，也就是说使用的密钥为单钥（或称为私钥）。
    /// 但不同于DES的是，RC4不是对明文进行分组处理，而是字节流的方式依次加密明文中的每一个字节，解密的时候也是依次对密文中的每一个字节进行解密。
    /// 
    /// RC4算法的特点是算法简单，运行速度快，而且密钥长度是可变的，可变范围为1-256字节(8-2048比特)，
    /// 在如今技术支持的前提下，当密钥长度为128比特时，用暴力法搜索密钥已经不太可行，所以可以预见RC4的密钥范围任然可以在今后相当长的时间里抵御暴力搜索密钥的攻击。
    /// 实际上，如今也没有找到对于128bit密钥长度的RC4加密算法的有效攻击方法。
    /// </remarks>
    public static class ExRC4
    {
        /// <summary>加密</summary>
        /// <param name="data">数据</param>
        /// <param name="pass">密码</param>
        /// <returns></returns>
        public static byte[] Encrypt(byte[] data, byte[] pass)
        {
            if (data == null || data.Length == 0)
            {
                return Array.Empty<byte>();
            }

            if (pass == null || pass.Length == 0)
            {
                return data;
            }

            byte[] output = new byte[data.Length];
            int i = 0;
            int j = 0;
            byte[] box = GetKey(pass, 256);
            // 加密  
            for (int k = 0; k < data.Length; k++)
            {
                i = (i + 1) % box.Length;
                j = (j + box[i]) % box.Length;
                byte temp = box[i];
                box[i] = box[j];
                box[j] = temp;
                byte a = data[k];
                byte b = box[(box[i] + box[j]) % box.Length];
                output[k] = (byte)(a ^ b);
            }

            return output;
        }

        /// <summary>打乱密码</summary>  
        /// <param name="pass">密码</param>  
        /// <param name="len">密码箱长度</param>  
        /// <returns>打乱后的密码</returns>  
        private static byte[] GetKey(byte[] pass, int len)
        {
            byte[] box = new byte[len];
            for (int i = 0; i < len; i++)
            {
                box[i] = (byte)i;
            }
            int j = 0;
            for (int i = 0; i < len; i++)
            {
                j = (j + box[i] + pass[i % pass.Length]) % len;
                byte temp = box[i];
                box[i] = box[j];
                box[j] = temp;
            }
            return box;
        }
    }
}
