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

            var (stdout, exitcode) = TXQ.Utils.Tool.ExProcess.Run("ping.exe", "qq.com");
            Console.WriteLine(stdout);
            Console.WriteLine($"code:{exitcode}");
        }
    }
}
