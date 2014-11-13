﻿namespace SportsWorld.Web.Areas.Company.Models
{
    using AutoMapper;
    using SportsWorld.Models;
    using SportsWorld.Web.Infrastructure.Filters;
    using SportsWorld.Web.Infrastructure.Mapping;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web;

    public class CreateFieldViewModel : IHaveCustomMappings
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Field for")]
        public EnumFieldTypes Type { get; set; }

        public int CityID { get; set; }

        [Required]
        [Display(Name = "City location")]
        public string CityInfo { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        [StringLength(50)]
        public string Description { get; set; }

        [Required]
        [Range(1,100)]
        [Display(Name = "Capacity (people)")]
        public int Capacity { get; set; }
        
        [Required]
        [DataType(DataType.Currency)]
        [Display(Name = "Price per hour ($)")]
        public decimal PricePerHour { get; set; }

        [FileSize(1048576)]
        [DataType(DataType.Upload)]
        [Display(Name = "Field image")]
        public HttpPostedFileWrapper Image { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<CreateFieldViewModel, Field>()
                .ForMember(x => x.Image, opt => opt.Ignore());
        }
    }
}