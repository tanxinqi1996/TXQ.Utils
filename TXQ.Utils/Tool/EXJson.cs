using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace TXQ.Utils.Tool
{
    public static class EXJson
    {
        /// <summary>
        /// 把对象转换为JSON字符串
        /// </summary>
        /// <param name="o">对象</param>
        /// <returns>JSON字符串</returns>
        public static string EXToJSON(this object o, bool convert = false)
        {
            string json = JsonConvert.SerializeObject(o);
            if (convert)
            {
                return EXFormatJson(json);
            }
            else
            {
                return json;
            }
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
                return JsonConvert.DeserializeObject<T>(input);
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
        public static string EXFormatJson(string str)
        {
            //格式化json字符串
            JsonSerializer serializer = new JsonSerializer();
            TextReader tr = new StringReader(str);
            JsonTextReader jtr = new JsonTextReader(tr);
            object obj = serializer.Deserialize(jtr);
            if (obj != null)
            {
                StringWriter textWriter = new StringWriter();
                JsonTextWriter jsonWriter = new JsonTextWriter(textWriter)
                {
                    Formatting = Formatting.Indented,
                    Indentation = 4,
                    IndentChar = ' '
                };
                serializer.Serialize(jsonWriter, obj);
                return textWriter.ToString();
            }
            else
            {
                return str;
            }
        }
        /// <summary>
        /// 字典转json字符串
        /// </summary>
        /// <param name="myDic"></param>
        /// <returns></returns>
        public static string EXDictionaryToJson(this Dictionary<string, string> myDic)
        {
            string jsonStr = JsonConvert.SerializeObject(myDic);
            return jsonStr;
        }
        /// <summary>
        /// json转字典
        /// </summary>
        /// <param name="jsonStr"></param>
        /// <returns></returns>
        public static Dictionary<string, object> EXJsonToDictionary(this string jsonStr)
        {
            Dictionary<string, object> dic = JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonStr);
            return dic;
        }
    }
}
