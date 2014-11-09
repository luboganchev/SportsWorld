namespace SportsWorld.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Field
    {
        public Field()
        {
            Comments = new HashSet<Comment>();
            FieldRatings = new HashSet<FieldRating>();
            GameEvents = new HashSet<GameEvent>();
        }

        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public DateTime DateCreated { get; set; }

        public EnumFieldTypes Type { get; set; }

        public int CityID { get; set; }

        [Required]
        [StringLength(50)]
        public string Description { get; set; }

        public int Capacity { get; set; }

        [Column(TypeName = "money")]
        public decimal PricePerHour { get; set; }

        public int CompanyID { get; set; }

        public int? ImageID { get; set; }

        public virtual City City { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public virtual Company Company { get; set; }

        public virtual ICollection<FieldRating> FieldRatings { get; set; }

        public virtual Image Image { get; set; }

        public virtual ICollection<GameEvent> GameEvents { get; set; }
    }
}
