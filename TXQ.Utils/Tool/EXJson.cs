//using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace TXQ.Utils.Tool
{
    public static class EXJson
    {
        /// <summary>
        /// 把对象转换为JSON字符串
        /// </summary>
        /// <param name="object">对象</param>
        /// <param name="WriteIndented">使用整齐打印，默认为false</param>
        /// <returns></returns>
        public static string EXToJSON(this object Object, bool WriteIndented = false)
        {

            return JsonSerializer.Serialize(Object, new JsonSerializerOptions()
            {
                WriteIndented = WriteIndented,
                IncludeFields = true,
            });
        }
        /// <summary>
        /// 把Json文本转为实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="input"></param>
        /// <param name="PropertyNameCaseInsensitive">忽略大小写，默认为true</param>
        /// <returns>T</returns>
        public static T EXJsonToType<T>(this string input, bool PropertyNameCaseInsensitive = true)
        {
            return JsonSerializer.Deserialize<T>(input, new JsonSerializerOptions()
            {
                IncludeFields = true,
                PropertyNameCaseInsensitive = PropertyNameCaseInsensitive
            });

        }
    }
}
