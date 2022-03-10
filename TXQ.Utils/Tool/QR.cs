using System;
using System.Drawing;
using ZXing;

namespace TXQ.Utils.Tool
{
    public static class QR
    {
        /// <summary>
        /// QR图片解码
        /// </summary>
        /// <param name="barcodeBitmap"></param>
        /// <returns></returns>
        public static string DecodeQrCode(Bitmap barcodeBitmap)
        {
            var reader = new BarcodeReader();
            //    reader.Options.CharacterSet = "UTF-8";
            Result result = reader.Decode(barcodeBitmap);
            return result?.Text;
        }

        /// <summary>
        /// QR图片解码
        /// </summary>
        /// <param name="img"></param>
        /// <returns></returns>
        public static string DecodeQrCode(Image img)
        {
            var reader = new BarcodeReader();
            //      reader.Options.CharacterSet = "UTF-8";
            Result result = reader.Decode(new Bitmap(img));
            return result?.Text;
        }

        /// <summary>
        /// QR图片解码 从剪切板，可能会抛出异常，请使用TRY CATCH捕获
        /// </summary>
        /// <returns></returns>
        public static string DecodeQrCodeFromClipboard()
        {
            Image img = System.Windows.Forms.Clipboard.GetImage();
            if (img == null)
            {
                throw new Exception("没有找到要识别的图片");
            }

            string result = QR.DecodeQrCode(img);
            if (result == null)
            {
                throw new Exception("没有识别到二维码");
            }

            return result;
        }

    }
}
