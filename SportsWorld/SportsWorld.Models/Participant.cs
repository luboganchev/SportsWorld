namespace SportsWorld.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Participant
    {
        [Key]
        [Column(Order = 0)]
        public int GameEventID { get; set; }

        [Key]
        [Column(Order = 1)]
        public string UserID { get; set; }

        public virtual GameEvent GameEvent { get; set; }
    }
}
