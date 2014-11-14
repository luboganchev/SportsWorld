namespace SportsWorld.Web.Areas.User.Models
{
    using AutoMapper;
    using SportsWorld.Models;
    using SportsWorld.Web.Infrastructure.Mapping;
    using SportsWorld.Web.Utils;
    using System.Linq;

    public class FieldViewModel : IHaveCustomMappings
    {
        private string image;

        public int ID { get; set; }

        public string CityName { get; set; }

        public string CountryName { get; set; }

        public string Name { get; set; }

        public float Rating { get; set; }

        public EnumFieldTypes Type { get; set; }

        public decimal PricePerHour { get; set; }

        public string Image
        {
            get
            {
                if (this.image == null && this.ImageType != null && this.ImageData != null)
                {
                    this.image = DataModelsHelper.GetBase64(this.ImageType, this.ImageData);
                }

                return this.image;
            }
        }

        public int Capacity { get; set; }

        public string ImageType { get; set; }

        public byte[] ImageData { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Field, FieldViewModel>()
                .ForMember(x => x.Image, opt => opt.Ignore())
                .ForMember(x => x.ImageType, opt => opt.MapFrom(m => m.Image.Type))
                .ForMember(x => x.ImageData, opt => opt.MapFrom(m => m.Image.Data))
                .ForMember(x => x.Rating, opt => opt.MapFrom(m => m.FieldRatings.Count > 0 ? m.FieldRatings.Average(r => (float)r.Value) : 0))
                .ForMember(x => x.CityName, opt => opt.MapFrom(m => m.City.Name))
                .ForMember(x => x.CountryName, opt => opt.MapFrom(m => m.City.Country.Name));
        }
    }
}