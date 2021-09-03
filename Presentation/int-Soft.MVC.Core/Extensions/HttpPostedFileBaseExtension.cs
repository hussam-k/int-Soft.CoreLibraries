using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Web;
using intSoft.MVC.Core.Common;

namespace intSoft.MVC.Core.Extensions
{
    public static class HttpPostedFileBaseExtension
    {
        public static byte[] ToByteArray(this HttpPostedFileBase file)
        {
            if (file == null) return new byte[] { };
            var content = new byte[file.ContentLength];
            file.InputStream.Read(content, 0, file.ContentLength);
            return content;
        }

        public static string ToBase64StringImage(this byte[] byteArray, string fileExtension = DefaultValuesBase.DefaultImageExtension)
        {
            if (byteArray == null)
                return "";

            var mimeType = string.Format(DefaultValuesBase.MIMEImageFormat, fileExtension);
            var base64 = Convert.ToBase64String(byteArray);

            return string.Format("data:{0};base64,{1}", mimeType, base64);
        }

        public static byte[] GetThumbnailImage(this byte[] byteArray, int width = 50, int height = 50)
        {
            if (byteArray == null) return null;
            using (var inputStream = new MemoryStream(byteArray))
            {
                var image = Image.FromStream(inputStream);
                var thumb = image.GetThumbnailImage(width, height, () => false, IntPtr.Zero);
                using (var outputStream = new MemoryStream())
                {
                    thumb.Save(outputStream, ImageFormat.Jpeg);
                    return outputStream.ToArray();
                }
            }
        }

        public static string GetThumbnailBase64(this byte[] byteArray, int width = 50, int height = 50)
        {
            return byteArray == null ? "" : byteArray.GetThumbnailImage(width, height).ToBase64StringImage();
        }
    }
}