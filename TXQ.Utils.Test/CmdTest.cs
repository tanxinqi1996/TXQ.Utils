using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TXQ.Utils.Test
{
    [TestClass]
    public class CmdTest
    {
        [TestMethod]

        public void TestGet()
        {
            TXQ.Utils.Tool.CMD.Run("ECHO 哈哈\r\npause");
        }
    }
}
