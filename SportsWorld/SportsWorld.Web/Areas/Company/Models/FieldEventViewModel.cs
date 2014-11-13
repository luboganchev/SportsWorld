namespace SportsWorld.Web.Areas.Company.Models
{
    using AutoMapper;
    using Kendo.Mvc.UI;
    using SportsWorld.Models;
    using SportsWorld.Web.Infrastructure.Mapping;
    using System;

    public class FieldEventViewModel : ISchedulerEvent, IHaveCustomMappings
    {
        public int ID { get; set; }

        public string Description { get; set; }

        public DateTime End { get; set; }

        public string EndTimezone { get; set; }

        public bool IsAllDay { get; set; }

        public string RecurrenceException { get; set; }

        public string RecurrenceRule { get; set; }

        public DateTime Start { get; set; }

        public string StartTimezone { get; set; }

        public string Title { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<FieldEventViewModel, GameEvent>()
                .ForMember(x => x.EndTime, opt => opt.MapFrom(m => m.End))
                .ForMember(x => x.StartTime, opt => opt.MapFrom(m => m.Start));

            configuration.CreateMap<GameEvent, FieldEventViewModel>()
                .ForMember(x => x.End, opt => opt.MapFrom(m => m.EndTime))
                .ForMember(x => x.Start, opt => opt.MapFrom(m => m.StartTime));
        }
    }
}