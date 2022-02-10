using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TXQ.Utils.Tool;

namespace TXQ.Utils.Test
{
    [TestClass]
    public class AESTest
    {
        [TestMethod]

        public void TestGet()
        {
            var STR = Guid.NewGuid().ToString();
            var KEY = Guid.NewGuid().ToString();
            var AESSTR = TXQ.Utils.Tool.EXAES.AesEncrypt(STR, KEY);
            Console.WriteLine("Success");

            Assert.IsTrue(Tool.EXAES.AesDecrypt(AESSTR, KEY) == STR);
        }
    }
}
