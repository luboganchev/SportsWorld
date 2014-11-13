namespace SportsWorld.Web.Areas.Company.Models
{
    using AutoMapper;
    using SportsWorld.Models;
    using SportsWorld.Web.Infrastructure.Mapping;
    using SportsWorld.Web.Utils;
    using System;

    public abstract class BaseFieldViewModel: IHaveCustomMappings
    {
        private string image;

        public int ID { get; set; }

        public string Name { get; set; }

        public DateTime DateCreated { get; set; }

        public EnumFieldTypes Type { get; set; }

        public string CityName { get; set; }

        public int Capacity { get; set; }

        public decimal PricePerHour { get; set; }

        public string CompanyName { get; set; }

        public string Image
        {
            get
            {
                if (this.image == null && this.ImageType != null && this.ImageData != null)
                {
                    this.image = DataModelsHelper.GetBase64(this.ImageType, this.ImageData);
                }

                return this.image;
            }
        }

        public string CountryName { get; set; }

        public string ImageType { get; set; }

        public byte[] ImageData { get; set; }

        public virtual void CreateMappings(IConfiguration configuration)
        {
        }
    }
}