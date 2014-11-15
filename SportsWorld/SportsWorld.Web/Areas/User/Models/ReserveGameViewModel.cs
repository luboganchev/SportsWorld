namespace SportsWorld.Web.Areas.User.Models
{
    using SportsWorld.Models;
    using System;
    using System.ComponentModel.DataAnnotations;

    public class ReserveGameViewModel
    {
        [Required]
        public int FieldID { get; set; }

        public Field Field { get; set; }

        [Required]
        [UIHint("DateTimeWithHours")]
        public DateTime StartTime { get; set; }

        [Required]
        [UIHint("DateTimeWithHours")]
        public DateTime EndTime { get; set; }

        [Required]
        public EnumCardTypes Type { get; set; }

        [Required]
        [Range(1000000000000000, 9999999999999999, ErrorMessage="Credit card number should have at least 16 digits")]
        [DataType(DataType.CreditCard)]
        public long Number { get; set; }

        [Required]
        public DateTime DateValid { get; set; }

        [Required]
        [Range(100, 9999)]
        public int CVV { get; set; }
    }
}