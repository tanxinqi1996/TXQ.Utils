using Microsoft.VisualStudio.TestTools.UnitTesting;
using TXQ.Utils.Tool;

namespace TXQ.Utils.Test
{
    [TestClass]
    public class Base64Test
    {
        [TestMethod]
        public void Base64Test1()
        {
            var file = System.IO.Path.GetTempFileName();
            //    System.IO.File.WriteAllText(file,)

            System.Console.WriteLine(new System.IO.FileInfo(@"D:\1.btw").ExGetSha256());
            System.Console.WriteLine("AAAAAA".EXGetSha256());

        }
    }
}