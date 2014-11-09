namespace SportsWorld.Web.Areas.Company.Models
{
    using AutoMapper;
    using SportsWorld.Models;
    using SportsWorld.Web.Infrastructure.Mapping;
    using System;

    public class MineFieldGridViewModel : IHaveCustomMappings
    {
        private string image;

        public int ID { get; set; }

        public string Name { get; set; }

        public DateTime DateCreated { get; set; }

        public EnumFieldTypes Type { get; set; }

        public string CityName { get; set; }

        public int CommentsCount { get; set; }

        public int Capacity { get; set; }

        public decimal PricePerHour { get; set; }

        public string CompanyName { get; set; }

        public string Image
        {
            get
            {
                if (this.image == null && this.ImageType != null && this.ImageData != null)
                {
                    this.image = GetBase64(this.ImageType, this.ImageData);
                }

                return this.image;
            }
        }

        public string CountryName { get; set; }

        public string ImageType { get; set; }

        public byte[] ImageData { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Field, MineFieldGridViewModel>()
                .ForMember(x => x.CityName, opt => opt.MapFrom(m => m.City.Name))
                .ForMember(x => x.CompanyName, opt => opt.MapFrom(m => m.Company.Name))
                .ForMember(x => x.CountryName, opt => opt.MapFrom(m => m.City.Country.Name))
                .ForMember(x => x.ImageType, opt => opt.MapFrom(m => m.Image.Type))
                .ForMember(x => x.ImageData, opt => opt.MapFrom(m => m.Image.Data))
                .ForMember(x => x.CommentsCount, opt => opt.MapFrom(m => m.Comments.Count));
        }

        private string GetBase64(string imageType, byte[] imageData)
        {
            var binaryContent = Convert.ToBase64String(imageData);
            var imageBase64 = string.Format("data:{0};base64,{1}", imageType, binaryContent);

            return imageBase64;
        }
    }
}