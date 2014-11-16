namespace SportsWorld.Web.Areas.Administration.Models
{
    using SportsWorld.Models;
    using SportsWorld.Web.Infrastructure.Mapping;
    using System.ComponentModel.DataAnnotations;

    public class CountryViewModel : IMapFrom<Country>
    {
        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }
    }
}