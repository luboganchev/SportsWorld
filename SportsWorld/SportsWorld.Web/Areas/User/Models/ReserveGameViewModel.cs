namespace SportsWorld.Web.Areas.User.Models
{
    using SportsWorld.Models;
    using System;

    public class ReserveGameViewModel
    {
        public Field Field { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }
    }
}