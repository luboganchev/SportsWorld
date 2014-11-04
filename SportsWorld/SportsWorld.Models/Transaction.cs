namespace SportsWorld.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Transaction")]
    public partial class Transaction
    {
        public int ID { get; set; }

        public int UserID { get; set; }

        public int GameEventID { get; set; }

        public int ApprovalStatus { get; set; }

        public virtual GameEvent GameEvent { get; set; }
    }
}
