using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TXQ.Utils.Test
{
    [TestClass]
    public class RandonTest
    {
        [TestMethod]
        public void RandonTest1()
        {
            System.Console.WriteLine(TXQ.Utils.Tool.EXRandom.CreateNum(5));
            System.Console.WriteLine(TXQ.Utils.Tool.EXRandom.CreateSmallAbc(10));
            System.Console.WriteLine(TXQ.Utils.Tool.EXRandom.CreateBigAbc(10));
            System.Console.WriteLine(TXQ.Utils.Tool.EXRandom.CreateString(10,true,true,true));

        }
    }
}