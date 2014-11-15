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
        [DataType(DataType.CreditCard)]
        public long Number { get; set; }

        [Required]
        public DateTime DateValid { get; set; }

        [Required]
        [Range(3, 4)]
        public int CVV { get; set; }
    }
}