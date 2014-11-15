namespace SportsWorld.Web.Areas.User.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web;

    public class GameEventDataModel
    {
        [Required]
        public int FieldID { get; set; }

        [Required]
        [Range(1, 365)]
        public int DayOfYear { get; set; }

        [Required]
        [Range(0, 23)]
        public int StartHour { get; set; }
    }
}