namespace SportsWorld.Web.Areas.Administration.Models
{
    using AutoMapper;
    using SportsWorld.Models;
    using SportsWorld.Web.Infrastructure.Filters;
    using SportsWorld.Web.Infrastructure.Mapping;
    using System.ComponentModel.DataAnnotations;
    using System.Web;
    using System.Collections.Generic;
    using System;

    public class FieldViewModel: IHaveCustomMappings
    {
        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Field for")]
        public EnumFieldTypes Type { get; set; }

        public int CityID { get; set; }

        [Required]
        [StringLength(50)]
        public string Description { get; set; }

        [Required]
        [Range(1, 100)]
        [Display(Name = "Capacity (people)")]
        public int Capacity { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [Display(Name = "Price per hour ($)")]
        public decimal PricePerHour { get; set; }

        [Required]
        public int CompanyID { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Field, FieldViewModel>()
                .ForMember(x => x.CityID, opt => opt.MapFrom(m => m.CityID));

            configuration.CreateMap<FieldViewModel, Field>()
                .ForMember(x => x.CityID, opt => opt.MapFrom(m => m.CityID))
                .ForMember(x => x.DateCreated, opt => opt.UseValue(DateTime.Now));
        }
    }
}