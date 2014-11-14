namespace SportsWorld.Web.Models
{
    using System.ComponentModel.DataAnnotations;

    public class CommentViewModel
    {
        [Required]
        [StringLength(1000)]
        public string Message { get; set; }
    }
}