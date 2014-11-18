namespace SportsWorld.Web.Areas.User.Models
{
    using SportsWorld.Models;
    using System.Collections.Generic;

    public class ScheduleViewModel
    {
        public int FieldID { get; set; }

        public IEnumerable<GameEvent> GameEvents { get; set; }
    }
}