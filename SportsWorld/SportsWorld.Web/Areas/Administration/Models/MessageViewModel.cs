namespace SportsWorld.Web.Areas.Administration.Models
{
    using SportsWorld.Models;
    using SportsWorld.Web.Infrastructure.Mapping;
    using System.ComponentModel.DataAnnotations;
    

    public class MessageViewModel : IMapFrom<Message>
    {
        [Required]
        public int ID { get; set; }

        [Required]
        [StringLength(4000)]
        public string Content { get; set; }

        [Required]
        public string SenderID { get; set; }

        [Required]
        public string RecipientID { get; set; }
    }
}