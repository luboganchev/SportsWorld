namespace SportsWorld.Web.Utils
{
    using SportsWorld.Models;
    using System;
    using System.Web;

    public static class DataModelsHelper
    {
        public static Image GetResizedImageInstance(HttpPostedFileWrapper wrapper)
        {
            var contentType = wrapper.ContentType;
            var data = wrapper.GetResizedImage();
            var image = new Image
            {
                Data = data,
                Type = contentType
            };

            return image;
        }

        public static string GetBase64(string imageType, byte[] imageData)
        {
            var binaryContent = Convert.ToBase64String(imageData);
            var imageBase64 = string.Format("data:{0};base64,{1}", imageType, binaryContent);

            return imageBase64;
        }
    }
}