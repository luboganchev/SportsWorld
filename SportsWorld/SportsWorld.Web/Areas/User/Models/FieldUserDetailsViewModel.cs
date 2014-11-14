namespace SportsWorld.Web.Areas.User.Models
{
    using AutoMapper;
    using SportsWorld.Models;
    using SportsWorld.Web.Infrastructure.Mapping;
    using SportsWorld.Web.Models;
    using System.Collections.Generic;
    using System.Linq;

    public class FieldUserDetailsViewModel : FieldDetailsViewModel, IHaveCustomMappings
    {
        public int VotesCount { get; set; }

        public int UserVote { get; set; }

        public ICollection<GameEvent> GameEvents { get; set; }

        public override void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Field, FieldUserDetailsViewModel>()
                .ForMember(x => x.CityName, opt => opt.MapFrom(m => m.City.Name))
                .ForMember(x => x.CompanyName, opt => opt.MapFrom(m => m.Company.Name))
                .ForMember(x => x.CountryName, opt => opt.MapFrom(m => m.City.Country.Name))
                .ForMember(x => x.ImageType, opt => opt.MapFrom(m => m.Image.Type))
                .ForMember(x => x.ImageData, opt => opt.MapFrom(m => m.Image.Data))
                .ForMember(x => x.Comments, opt => opt.MapFrom(m => m.Comments.OrderByDescending(c => c.PostedOn)))
                .ForMember(x => x.FieldRating, opt => opt.MapFrom(m => m.FieldRatings.Count > 0 ? m.FieldRatings.Average(r => r.Value) : 0))
                .ForMember(x => x.VotesCount, opt => opt.MapFrom(m => m.FieldRatings.Count));
        }
    }
}