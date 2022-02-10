using Microsoft.VisualStudio.TestTools.UnitTesting;
using TXQ.Utils.Tool;

namespace TXQ.Utils.Test
{
    [TestClass]
    public class JsonTest
    {
        [TestMethod]
        public void Json()
        {
            TXQ.Utils.Tool.PC.PCINFO.Init();
            Assert.AreEqual(TXQ.Utils.Tool.PC.PCINFO.EXToJSON(), TXQ.Utils.Tool.PC.PCINFO.EXToJSON().EXJsonToType<Model.PCINFO>().EXToJSON());
            System.Console.WriteLine(TXQ.Utils.Tool.PC.PCINFO.EXToJSON());

        }
    }
}