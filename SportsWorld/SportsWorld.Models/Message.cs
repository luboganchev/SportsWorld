namespace SportsWorld.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Message
    {
        public int ID { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        [StringLength(128)]
        public string SenderID { get; set; }

        [Required]
        [StringLength(128)]
        public string RecipientID { get; set; }
    }
}
