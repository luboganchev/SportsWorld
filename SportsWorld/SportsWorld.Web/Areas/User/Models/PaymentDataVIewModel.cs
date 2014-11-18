namespace SportsWorld.Web.Areas.User.Models
{
    using SportsWorld.Models;
    using SportsWorld.Web.Infrastructure.Mapping;
    using System;
    using System.ComponentModel.DataAnnotations;

    public class PaymentDataViewModel : IMapFrom<CardInfo>
    {
        [Required]
        public EnumCardTypes Type { get; set; }

        [Required]
        [Range(1000000000000000, 9999999999999999, ErrorMessage = "Credit card number should have at least 16 digits")]
        [DataType(DataType.CreditCard)]
        public long Number { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateValid { get; set; }

        [Required]
        [Range(100, 9999)]
        public int CVV { get; set; }
    }
}