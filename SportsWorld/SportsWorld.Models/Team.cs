namespace SportsWorld.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Team
    {
        public Team()
        {
            TeamMembers = new HashSet<TeamMember>();
            GameEvents = new HashSet<GameEvent>();
        }

        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public DateTime DateCreated { get; set; }

        public int FounderID { get; set; }

        public int Wins { get; set; }

        public int Draws { get; set; }

        public int Losses { get; set; }

        public int? LogoID { get; set; }

        public virtual Image Image { get; set; }

        public virtual ICollection<TeamMember> TeamMembers { get; set; }

        public virtual ICollection<GameEvent> GameEvents { get; set; }
    }
}
