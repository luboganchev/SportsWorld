namespace SportsWorld.Web.Areas.Administration.Models
{
    using SportsWorld.Models;
    using SportsWorld.Web.Infrastructure.Mapping;
    using System;
    using System.ComponentModel.DataAnnotations;

    public class TeamViewModel : IMapFrom<Team>
    {
        [Required]
        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        public DateTime DateCreated { get; set; }

        [Required]
        public string FounderID { get; set; }

        [Required]
        public int Wins { get; set; }

        [Required]
        public int Draws { get; set; }

        [Required]
        public int Losses { get; set; }
    }
}