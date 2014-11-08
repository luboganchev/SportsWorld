namespace SportsWorld.Web.Utils
{
    using SportsWorld.Models;
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
    }
}