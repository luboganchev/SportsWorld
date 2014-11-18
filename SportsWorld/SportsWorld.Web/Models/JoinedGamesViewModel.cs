namespace SportsWorld.Web.Models
{
    using AutoMapper;
    using SportsWorld.Models;
    using SportsWorld.Web.Infrastructure.Mapping;
    using System;

    public class JoinedGamesViewModel : IHaveCustomMappings
    {
        public string FieldName { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<GameEvent, JoinedGamesViewModel>()
                .ForMember(x => x.FieldName, opt => opt.MapFrom(m => m.Field.Name));
        }
    }
}