using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TXQ.Utils.Test
{
    [TestClass]
    public class ZipTest
    {
        [TestMethod]
        public void ZipTest1()
        {
            int tempfilecount = 10;

            var dir = System.IO.Directory.CreateDirectory(System.IO.Path.GetTempPath()+ @"\"+System.Guid.NewGuid().ToString());
            for (int i = 0; i < tempfilecount; i++)
            {
                System.IO.File.WriteAllText($@"{dir.FullName}\{System.Guid.NewGuid()}", System.Guid.NewGuid().ToString());
            }
            Tool.EXZip.Zip(dir.FullName, dir.FullName + ".zip");
            dir.Delete(true);
            Tool.EXZip.UnZip(dir.FullName + ".zip", System.IO.Path.GetTempPath());
            Assert.AreEqual(dir.GetFiles().Length, tempfilecount);
        }
    }
}