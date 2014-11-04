namespace SportsWorld.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class FieldRating
    {
        public int ID { get; set; }

        public int Value { get; set; }

        public int FieldID { get; set; }

        public int UserID { get; set; }

        public DateTime RateOn { get; set; }

        public virtual Field Field { get; set; }
    }
}
