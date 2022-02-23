//using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

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
        /// <param name="PropertyNameCaseInsensitive">忽略大小写，默认为true</param>
        /// <returns>T</returns>
        public static T EXJsonToType<T>(this string input, bool PropertyNameCaseInsensitive = true)
        {
            return JsonConvert.DeserializeObject<T>(input);

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
    }
}
