namespace SportsWorld.Web.Models
{
    using SportsWorld.Models;
    using SportsWorld.Web.Infrastructure.Filters;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web;

    public abstract class BaseRegistarViewModel
    {
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

        [FileSize(1048576)]
        [DataType(DataType.Upload)]
        [Display(Name = "Avatar")]
        public HttpPostedFileWrapper Avatar { get; set; }

        [Required]
        [Display(Name = "Country")]
        public int CountryID { get; set; }

        public IEnumerable<Country> Countries { get; set; }

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

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}