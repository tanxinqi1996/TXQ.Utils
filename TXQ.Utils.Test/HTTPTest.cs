using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace TXQ.Utils.Test
{
    [TestClass]
    public class HTTPTest
    {
        [TestMethod]
        public void TestGet()
        {
            var text = TXQ.Utils.Tool.HTTP.Get("http://mes.ipason.com/oauthserver/oauth/token?username=tanxinqi&password=Tan1996.&client_id=MIClient&client_secret=secret&grant_type=password");
            Console.WriteLine(text.Result);
        }

        [TestMethod]
        public void TestPost()
        {

            // var text = TXQ.Utils.Tool.HTTP.Post("http://mes.ipason.com/blazerproduction/test/log", "{\"hardwareHealthStatus\":\"\",\"hardwareInformation\":\" \",\"secretKey\":\"\",\"sn\":\"2012236240356\",\"softwareInformation\":\"\",\"testTime\":\"2022-02-09 15:33:06\",\"mo\":\"2012236240356\"}", new System.Collections.Generic.Dictionary<string, string>() { { "Authorization", "Bearer 00ec1394-3182-4f93-a482-9600d523a0f4" } });

            // Console.WriteLine(text.Result);
        }
    }
}
