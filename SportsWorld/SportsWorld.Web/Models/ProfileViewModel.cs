namespace SportsWorld.Web.Models
{
    using AutoMapper;
    using SportsWorld.Models;
    using SportsWorld.Web.Areas.User.Models;
    using SportsWorld.Web.Infrastructure.Mapping;
    using SportsWorld.Web.Utils;
    using System.Collections.Generic;

    public class ProfileViewModel : IHaveCustomMappings
    {
        public ProfileViewModel()
        {
            this.PaymentInfo = new PaymentDataViewModel();
        }

        private string image;

        public string Id { get; set; }

        public string FirstName { get; set; }

        public string Username { get; set; }

        public string LastName { get; set; }

        public int CountryID { get; set; }

        public string CountryName { get; set; }

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

        public string ImageType { get; set; }

        public byte[] ImageData { get; set; }

        public IEnumerable<Country> Countries { get; set; }

        public IEnumerable<JoinedGamesViewModel> JoinedGames { get; set; }

        public PaymentDataViewModel PaymentInfo { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<AppUser, ProfileViewModel>()
                .ForMember(x => x.Image, opt => opt.Ignore())
                .ForMember(x => x.CountryName, opt => opt.MapFrom(m => m.Country.Name));
        }
    }
}