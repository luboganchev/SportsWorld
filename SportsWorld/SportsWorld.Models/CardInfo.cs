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

        public EnumCardTypes Type { get; set; }

        [Required]
        [Range(1000000000000000, 9999999999999999)]
        public long Number { get; set; }

        [Column(TypeName = "date")]
        public DateTime DateValid { get; set; }

        [Required]
        [Range(100, 9999)]
        public int CVV { get; set; }

        [Required]
        [StringLength(128)]
        public string UserID { get; set; }
    }
}
