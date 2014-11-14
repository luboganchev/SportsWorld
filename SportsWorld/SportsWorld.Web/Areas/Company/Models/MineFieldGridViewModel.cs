namespace SportsWorld.Web.Areas.Company.Models
{
    using AutoMapper;
    using SportsWorld.Models;
    using SportsWorld.Web.Models;

    public class MineFieldGridViewModel : BaseFieldViewModel
    {
        public int CommentsCount { get; set; }

        public override void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Field, MineFieldGridViewModel>()
                .ForMember(x => x.CityName, opt => opt.MapFrom(m => m.City.Name))
                .ForMember(x => x.CompanyName, opt => opt.MapFrom(m => m.Company.Name))
                .ForMember(x => x.CountryName, opt => opt.MapFrom(m => m.City.Country.Name))
                .ForMember(x => x.ImageType, opt => opt.MapFrom(m => m.Image.Type))
                .ForMember(x => x.ImageData, opt => opt.MapFrom(m => m.Image.Data))
                .ForMember(x => x.CommentsCount, opt => opt.MapFrom(m => m.Comments.Count));
        }
    }
}