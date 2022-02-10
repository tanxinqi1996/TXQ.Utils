using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace TXQ.Utils.Tool
{
    public static class HTTP
    {
        /// <summary>
        /// HTTP GET
        /// </summary>
        /// <param name="url">URL</param>
        /// <param name="header">请求头</param>
        /// <returns></returns>
        public static async Task<string> Get(string url, Dictionary<string, string> header = null)
        {
            HttpClient client = new HttpClient();
            if (header != null)
            {
                client.DefaultRequestHeaders.Clear();
                foreach (var item in header)
                {
                    client.DefaultRequestHeaders.Add(item.Key, item.Value);
                }
            }
            HttpResponseMessage response = client.GetAsync(url).Result;
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }


        /// <summary>
        /// HTTP POST
        /// </summary>
        /// <param name="url">URL</param>
        /// <param name="jsonbody">JSON Body</param>
        /// <param name="header">请求头</param>
        /// <returns></returns>
        public static async Task<string> Post(string url, string jsonbody, Dictionary<string, string> header = null)
        {
            HttpClient client = new HttpClient();
            if (header != null)
            {
                client.DefaultRequestHeaders.Clear();
                foreach (var item in header)
                {
                    client.DefaultRequestHeaders.Add(item.Key, item.Value);
                }
            }
            HttpContent content = new StringContent(jsonbody);
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            HttpResponseMessage response = client.PostAsync(url, content).Result;
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();

        }
    }

}
