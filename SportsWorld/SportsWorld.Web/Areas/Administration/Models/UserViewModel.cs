namespace SportsWorld.Web.Areas.Administration.Models
{
    using SportsWorld.Models;
    using SportsWorld.Web.Infrastructure.Mapping;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class UserViewModel : IMapFrom<AppUser>
    {
        public string ID { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [MinLength(2)]
        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [Required]
        [MinLength(2)]
        [DataType(DataType.Text)]
        [Display(Name = "Last name")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Country")]
        public int CountryID { get; set; }

        [Required]
        [MinLength(2)]
        [DataType(DataType.Text)]
        [Display(Name = "Username")]
        public string Username { get; set; }

        [Required]
        [MinLength(5)]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}