namespace SportsWorld.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Comment
    {
        public int ID { get; set; }

        [Required]
        public string Content { get; set; }

        public DateTime PostedOn { get; set; }

        [Required]
        [StringLength(128)]
        public string AuthorID { get; set; }

        public virtual AppUser Author { get; set; }

        public int FieldID { get; set; }

        public virtual Field Field { get; set; }
    }
}
