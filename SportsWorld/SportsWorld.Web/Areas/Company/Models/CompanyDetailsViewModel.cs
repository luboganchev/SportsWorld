namespace SportsWorld.Web.Areas.Company.Models
{
    using AutoMapper;
    using SportsWorld.Models;
    using SportsWorld.Web.Infrastructure.Filters;
    using SportsWorld.Web.Infrastructure.Mapping;
    using SportsWorld.Web.Utils;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web;

    public class CompanyDetailsViewModel : IHaveCustomMappings
    {
        private string image;

        public int ID { get; set; }

        [Required]
        [MinLength(2)]
        [Display(Name = "Company name")]
        public string Name { get; set; }

        public int UniqueNumber { get; set; }

        [Required]
        [Display(Name = "Country HQ")]
        public int CountryID { get; set; }

        [Required]
        [Range(1800, 2200)]
        [Display(Name = "Est. year")]
        public int EstablishedYear { get; set; }

        [FileSize(1048576)]
        [DataType(DataType.Upload)]
        [Display(Name = "Company logo")]
        public HttpPostedFileWrapper CompanyLogo { get; set; }

        public string CountryName { get; set; }

        public decimal TotalRevenue { get; set; }

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

        public string ImageType { get; set; }

        public byte[] ImageData { get; set; }

        public int FieldsCount { get; set; }

        public IEnumerable<Country> Countries { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Company, CompanyDetailsViewModel>()
                .ForMember(x => x.Image, opt => opt.Ignore())
                .ForMember(x => x.FieldsCount, opt => opt.MapFrom(m => m.Fields.Count))
                .ForMember(x => x.CountryName, opt => opt.MapFrom(m => m.Country.Name));

            configuration.CreateMap<CompanyDetailsViewModel, Company>()
                .ForMember(x => x.Image, opt => opt.Ignore());
        }
    }
}