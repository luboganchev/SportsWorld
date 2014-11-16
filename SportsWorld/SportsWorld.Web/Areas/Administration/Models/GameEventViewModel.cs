namespace SportsWorld.Web.Areas.Administration.Models
{
    using SportsWorld.Models;
    using SportsWorld.Web.Infrastructure.Mapping;
    using System;
    using System.ComponentModel.DataAnnotations;

    public class GameEventViewModel : IMapFrom<GameEvent>
    {
        [Required]
        public int ID { get; set; }

        [Required]
        public int FieldID { get; set; }


        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        public DateTime EndTime { get; set; }

        [Required]
        public string CreatorID { get; set; }
    }
}