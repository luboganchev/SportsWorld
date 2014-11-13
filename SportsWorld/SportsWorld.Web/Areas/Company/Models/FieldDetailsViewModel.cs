namespace SportsWorld.Web.Areas.Company.Models
{
    using AutoMapper;
    using SportsWorld.Models;
    using SportsWorld.Web.Infrastructure.Mapping;
    using System.Collections.Generic;
    using System.Linq;

    public class FieldDetailsViewModel : BaseFieldViewModel
    {
        public FieldDetailsViewModel()
        {
            Comments = new HashSet<Comment>();
            GameEvents = new HashSet<GameEvent>();
        }

        public string Description { get; set; }

        public decimal TotalEarned { get; set; }

        public ICollection<Comment> Comments { get; set; }

        public float FieldRating { get; set; }

        public ICollection<GameEvent> GameEvents { get; set; }

        public override void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Field, FieldDetailsViewModel>()
                .ForMember(x => x.CityName, opt => opt.MapFrom(m => m.City.Name))
                .ForMember(x => x.CompanyName, opt => opt.MapFrom(m => m.Company.Name))
                .ForMember(x => x.CountryName, opt => opt.MapFrom(m => m.City.Country.Name))
                .ForMember(x => x.ImageType, opt => opt.MapFrom(m => m.Image.Type))
                .ForMember(x => x.ImageData, opt => opt.MapFrom(m => m.Image.Data))
                .ForMember(x => x.FieldRating, opt => opt.MapFrom(m => m.FieldRatings.Count > 0 ? m.FieldRatings.Average(r => r.Value) : 0));
        }
    }
}