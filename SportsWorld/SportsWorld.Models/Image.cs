namespace SportsWorld.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Image
    {
        public Image()
        {
            Companies = new HashSet<Company>();
            Fields = new HashSet<Field>();
            Teams = new HashSet<Team>();
        }

        public int ID { get; set; }

        [Required]
        public byte[] Data { get; set; }

        [Required]
        public string Type { get; set; }

        public virtual ICollection<Company> Companies { get; set; }

        public virtual ICollection<Field> Fields { get; set; }

        public virtual ICollection<Team> Teams { get; set; }
    }
}
