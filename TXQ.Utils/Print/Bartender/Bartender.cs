using Seagull.BarTender.Print;
using System;
using System.Drawing;
using TXQ.Utils.Tool;

namespace TXQ.Utils.Print.Bartender
{
    public class Document
    {
        public Document(string filepath)
        {
            FilePath = filepath;
            if (!System.IO.File.Exists(filepath))
            {
                throw new Exception("找不到" + filepath);
            }
            Main.Init();
            doc = Main.BartenderEngine.Documents.Open(filepath);
        }
        private LabelFormatDocument doc;
        public void UpdateData(string oldStr, string newStr, bool ShowErr = true)
        {

            try
            {
                doc.SubStrings[oldStr].Value = newStr;
            }
            catch (Exception EX)
            {
                LOG.ERROR($"Bartender数据更新失败,Value:{oldStr}:{newStr};" + EX.Message);
                if (ShowErr)
                {
                    throw;
                }
            }

        }
        public void Print()
        {
            if (Copies < 1)
            {
                throw new Exception("打印份数不能小于1");
            }

            doc.Print();
        }

        public string Printer
        {
            get => doc.PrintSetup.PrinterName;
            set => doc.PrintSetup.PrinterName = value;
        }

        public int Copies
        {
            get => doc.PrintSetup.IdenticalCopiesOfLabel;
            set => doc.PrintSetup.IdenticalCopiesOfLabel = value;
        }

        public string XML()
        {
            return doc.SubStrings.XML;
        }
        public string FilePath;
        public Image ExportImg(int width = 1500, int height = 1500, string filename = null)
        {
            if (filename == null)
            {
                filename = System.IO.Path.GetTempFileName();
            }

            doc.ExportImageToFile(filename, ImageType.BMP, Seagull.BarTender.Print.ColorDepth.Mono, new Resolution(width, height), OverwriteOptions.Overwrite);
            Image image = Image.FromFile(filename);
            return image;
        }
    }
}
