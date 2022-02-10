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
        /// <param name="IncludeFields">使用整齐打印，默认为false</param>
        /// <returns></returns>
        public static string EXToJSON(this object Object, bool IncludeFields = false)
        {
            var options = new JsonSerializerOptions()
            {
                WriteIndented = IncludeFields,
                IncludeFields = true
            };
            return JsonSerializer.Serialize(Object, options);
        }
        /// <summary>
        /// 把Json文本转为实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="input"></param>
        /// <returns></returns>
        public static T EXJsonToType<T>(this string input, bool SHOWERR = false)
        {
            try
            {
                var options = new JsonSerializerOptions()
                {
                    IncludeFields = true,
                };
                //return JsonConvert.DeserializeObject<T>(input);
                return JsonSerializer.Deserialize<T>(input, options);
            }
            catch (Exception EX)
            {
                if (SHOWERR)
                {
                    throw EX;
                }
                return default;
            }
        }

        /// <summary>
        /// json转字典
        /// </summary>
        /// <param name="jsonStr"></param>
        /// <returns></returns>
        public static Dictionary<string, object> EXJsonToDictionary(this string jsonStr)
        {
            var options = new JsonSerializerOptions()
            {
                IncludeFields = true,
            };
            return JsonSerializer.Deserialize<Dictionary<string, object>>(jsonStr, options);
        }
    }
}
