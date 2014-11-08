namespace SportsWorld.Web.Utils
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.IO;
    using System.Linq;
    using System.Web;

    public static class HttpPostedFileWrapperExtensions
    {
        public static byte[] GetByteArray(this HttpPostedFileWrapper wrapper)
        {
            byte[] fileData = null;
            using (var binaryReader = new BinaryReader(wrapper.InputStream))
            {
                fileData = binaryReader.ReadBytes(wrapper.ContentLength);
            }

            return fileData;
        }

        public static byte[] GetResizedImage(this HttpPostedFileWrapper wrapper, int width = 800, int height = 600)
        {
            if (!wrapper.IsImage())
            {
                throw new InvalidOperationException("The given file is not an image!");
            }

            var currentImage = Image.FromStream(wrapper.InputStream);

            Bitmap newImage = new Bitmap(width, height);
            using (Graphics graphicsData = Graphics.FromImage(newImage))
            {
                graphicsData.SmoothingMode = SmoothingMode.HighQuality;
                graphicsData.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphicsData.PixelOffsetMode = PixelOffsetMode.HighQuality;
                graphicsData.DrawImage(currentImage, new Rectangle(0, 0, width, height));
            }

            ImageConverter converter = new ImageConverter();
            byte[] result = (byte[])converter.ConvertTo(newImage, typeof(byte[]));

            return result;
        }

        private static bool IsImage(this HttpPostedFileWrapper wrapper)
        {
            if (wrapper.ContentType.Contains("image"))
            {
                return true;
            }

            string[] formats = new string[] 
            { 
                ".jpg", 
                ".png", 
                ".gif", 
                ".jpeg" 
            };

            return formats.Any(item => wrapper.FileName.EndsWith(item, StringComparison.OrdinalIgnoreCase));
        }
    }
}