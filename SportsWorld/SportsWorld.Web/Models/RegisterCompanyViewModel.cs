namespace SportsWorld.Web.Models
{
    using SportsWorld.Models;
    using SportsWorld.Web.Infrastructure.Filters;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web;

    public class RegisterCompanyViewModel : BaseRegistarViewModel
    {
        [Required]
        [MinLength(2)]
        [DataType(DataType.Text)]
        [Display(Name = "Company name")]
        public string CompanyName { get; set; }

        [Required]
        [Range(100000000, 999999999)]
        [Display(Name = "Unique number")]
        public int UniqueNumber { get; set; }

        [Required]
        [Display(Name = "Country HQ")]
        public int CompanyCountryID { get; set; }

        [Required]
        [Range(1800, 2200)]
        [Display(Name = "Est. year")]
        public int EstablishedYear { get; set; }

        [FileSize(1048576)]
        [DataType(DataType.Upload)]
        [Display(Name = "Company logo")]
        public HttpPostedFileWrapper CompanyLogo { get; set; }        
    }
}