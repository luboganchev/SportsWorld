namespace SportsWorld.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CardInfo")]
    public partial class CardInfo
    {
        public int ID { get; set; }

        public int Type { get; set; }

        [Required]
        [StringLength(50)]
        public string Number { get; set; }

        [Column(TypeName = "date")]
        public DateTime DateValid { get; set; }

        [Required]
        [StringLength(10)]
        public string CVV { get; set; }

        public int UserID { get; set; }
    }
}
