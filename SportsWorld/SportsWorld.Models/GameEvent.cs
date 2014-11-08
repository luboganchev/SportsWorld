namespace SportsWorld.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("GameEvent")]
    public partial class GameEvent
    {
        public GameEvent()
        {
            Participants = new HashSet<Participant>();
            Transactions = new HashSet<Transaction>();
            Teams = new HashSet<Team>();
        }

        public int ID { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public int FieldID { get; set; }

        [Required]
        [StringLength(128)]
        public string CreatorID { get; set; }

        public virtual Field Field { get; set; }

        public virtual ICollection<Participant> Participants { get; set; }

        public virtual ICollection<Transaction> Transactions { get; set; }

        public virtual ICollection<Team> Teams { get; set; }
    }
}
