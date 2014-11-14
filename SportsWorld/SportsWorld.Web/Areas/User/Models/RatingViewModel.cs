namespace SportsWorld.Web.Areas.User.Models
{
    using System.ComponentModel.DataAnnotations;

    public class RatingViewModel
    {
        [Required]
        public int ID { get; set; }

        [Required]
        [Range(1, 5)]
        public int Rating { get; set; }
    }
}