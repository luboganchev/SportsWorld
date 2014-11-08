namespace SportsWorld.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Company
    {
        public Company()
        {
            Fields = new HashSet<Field>();
        }

        public int ID { get; set; }

        [Required]
        [Index("IX_CompanyName", IsUnique = true)]
        [StringLength(50)]
        public string Name { get; set; }

        public int CountryID { get; set; }

        [Index("IX_CompanyUniqueNumber", IsUnique = true)]
        public int UniqueNumber { get; set; }

        public int EstablishedYear { get; set; }

        [Column(TypeName = "money")]
        public decimal TotalRevenue { get; set; }

        public int? ImageID { get; set; }

        [Required]
        [StringLength(128)]
        public string FounderID { get; set; }

        public virtual Image Image { get; set; }

        public virtual ICollection<Field> Fields { get; set; }
    }
}
