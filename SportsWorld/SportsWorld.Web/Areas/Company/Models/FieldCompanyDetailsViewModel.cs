﻿namespace SportsWorld.Web.Areas.Company.Models
{
    using AutoMapper;
    using SportsWorld.Models;
    using SportsWorld.Web.Models;
    using System.Collections.Generic;
    using System.Linq;

    public class FieldCompanyDetailsViewModel : FieldDetailsViewModel
    {
        public FieldCompanyDetailsViewModel()
        {
            GameEvents = new HashSet<FieldEventViewModel>();
        }

        public decimal TotalEarned { get; set; }

        public ICollection<FieldEventViewModel> GameEvents { get; set; }

        public override void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Field, FieldCompanyDetailsViewModel>()
                .ForMember(x => x.CityName, opt => opt.MapFrom(m => m.City.Name))
                .ForMember(x => x.CompanyName, opt => opt.MapFrom(m => m.Company.Name))
                .ForMember(x => x.CountryName, opt => opt.MapFrom(m => m.City.Country.Name))
                .ForMember(x => x.ImageType, opt => opt.MapFrom(m => m.Image.Type))
                .ForMember(x => x.ImageData, opt => opt.MapFrom(m => m.Image.Data))
                .ForMember(x => x.TotalEarned, opt => opt.MapFrom(m => (m.GameEvents.Count * m.PricePerHour)))
                .ForMember(x => x.Comments, opt => opt.MapFrom(m => m.Comments.OrderByDescending(c => c.PostedOn)))
                .ForMember(x => x.FieldRating, opt => opt.MapFrom(m => m.FieldRatings.Count > 0 ? m.FieldRatings.Average(r => r.Value) : 0))
                .ForMember(x => x.GameEvents, opt => opt.MapFrom(
                    m => m.GameEvents
                        .Where(ev => ev.FieldID == m.ID)
                        .Select(ev =>
                        new FieldEventViewModel
                        {
                            ID = ev.ID,
                            Start = ev.StartTime,
                            End = ev.EndTime,
                            Title = ev.Creator.UserName,
                            Description = "Reserved"
                        })
                    )
                 );
        }
    }
}