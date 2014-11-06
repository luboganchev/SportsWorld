namespace SportsWorld.Data.Migrations
{
    using FakeData;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Newtonsoft.Json;
    using SportsWorld.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using System.IO;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<SportsWorldDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(SportsWorldDbContext context)
        {
            // Check if database is already seeded
            if (context.Users.Any())
            {
                return;
            }

            var roleStore = new RoleStore<IdentityRole>(context);
            var roleManager = new RoleManager<IdentityRole>(roleStore);

            // Roles
            var roles = Settings.Default.Roles.Split(',');
            var adminRole = roles[0];
            var userRole = roles[1];
            var companyRole = roles[2];
            foreach (var role in roles)
            {
                roleManager.Create(new IdentityRole { Name = role });
            }

            var userStore = new UserStore<AppUser>(context);
            var userManager = new UserManager<AppUser>(userStore);

            // Images
            var defaultImage = new Image { Path = "~/Images/default.png" };
            var adminImage = new Image { Path = "~/Images/admin.jpg" };
            context.Images.AddOrUpdate(
                defaultImage,
                adminImage
            );

            context.SaveChanges();

            // Countries
            var json = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + @"\App_Data\countries.json");
            var countries = JsonConvert.DeserializeObject<IEnumerable<Country>>(json);

            foreach (var country in countries)
            {
                context.Countries.Add(country);
            }

            // Users
            var countryBulgaria = context.Countries.Local.Single(item => item.Name == "Bulgaria");
            var userAdmin = new AppUser
            {
                UserName = "admin@admin",
                Email = "admin@admin",
                FirstName = NameData.GetFirstName(),
                LastName = NameData.GetSurname(),
                Country = countryBulgaria,
                Image = adminImage
            };
            userManager.Create(userAdmin, userAdmin.UserName);
            userManager.AddToRole(userAdmin.Id, adminRole);

            for (int userIndex = 0; userIndex < 100; userIndex++)
            {
                bool isSuccess = false;

                while (!isSuccess)
                {
                    var firstName = NameData.GetFirstName();
                    var lastName = NameData.GetSurname();
                    var userName = firstName + "@" + lastName;

                    Country country = null;
                    while (country == null)
                    {
                        var countryName = PlaceData.GetCountry();
                        country = context.Countries.Local.SingleOrDefault(item => item.Name == countryName);
                    }

                    var user = new AppUser
                    {
                        UserName = userName,
                        Email = userName,
                        FirstName = firstName,
                        LastName = lastName,
                        Country = country,
                        Image = new Image() { Path = string.Format("~/Images/Users/{0}.jpg", userIndex + 1) }
                    };
                    isSuccess = userManager.Create(user, userName).Succeeded;
                    if (isSuccess)
                    {
                        userManager.AddToRole(user.Id, userRole);
                    }
                }
            }

            //// Beer Categories
            //var categoryDark = new Category { Name = "Dark" };
            //var categoryLight = new Category { Name = "Light" };
            //var categoryFruit = new Category { Name = "Fruit" };
            //var categoryReserve = new Category { Name = "Reserve" };
            //var categoryPremium = new Category { Name = "Premium" };
            //var categoryOther = new Category { Name = "Other" };
            //context.Categories.AddOrUpdate(categoryDark, categoryLight, categoryFruit, categoryReserve, categoryPremium, categoryOther);

            //// Beer Brands
            //var brandKamenitza = new Brand { Name = "Kamenitza", Country = countryBulgaria, Established = new DateTime(1876, 1, 1) };
            //var brandZagorka = new Brand { Name = "Zagorka", Country = countryBulgaria, Established = new DateTime(1902, 1, 1) };
            //var brandShumensko = new Brand { Name = "Shumensko", Country = countryBulgaria, Established = new DateTime(1882, 1, 1) };
            //var brandStaropramen = new Brand
            //{
            //    Name = "Staropramen",
            //    Country = context.Countries.Local.Single(item => item.Name == "Czech Republic"),
            //    Established = new DateTime(1869, 1, 1)
            //};
            //var brandCarlsberg = new Brand
            //{
            //    Name = "Carlsberg",
            //    Country = context.Countries.Local.Single(item => item.Name == "Denmark"),
            //    Established = new DateTime(1847, 1, 1)
            //};
            //var brandTuborg = new Brand
            //{
            //    Name = "Tuborg Brewery",
            //    Country = context.Countries.Local.Single(item => item.Name == "Denmark"),
            //    Established = new DateTime(1873, 1, 1)
            //};
            //context.Brands.AddOrUpdate(brandKamenitza, brandZagorka, brandShumensko, brandStaropramen, brandCarlsberg, brandTuborg);

            //// Beers
            //context.Beers.AddOrUpdate(
            //    new Beer
            //    {
            //        Name = categoryDark.Name,
            //        Brand = brandShumensko,
            //        AlchoholPercentage = 5.5f,
            //        Category = categoryDark,
            //        Creator = userAdmin,
            //        CreatedOn = DateTime.Now,
            //        Images = new[] { new Image() { Path = "~/Images/Beers/Shumensko_Dark.jpg" } }
            //    },
            //    new Beer
            //    {
            //        Name = categoryLight.Name,
            //        Brand = brandShumensko,
            //        AlchoholPercentage = 4.3f,
            //        Category = categoryLight,
            //        Creator = userAdmin,
            //        CreatedOn = DateTime.Now,
            //        Images = new[] { new Image() { Path = "~/Images/Beers/Shumensko_Light.jpg" } }
            //    },
            //    new Beer
            //    {
            //        Name = categoryPremium.Name,
            //        Brand = brandShumensko,
            //        AlchoholPercentage = 4.6f,
            //        Category = categoryPremium,
            //        Creator = userAdmin,
            //        CreatedOn = DateTime.Now,
            //        Images = new[] { new Image() { Path = "~/Images/Beers/Shumensko_Premium.jpg" } }
            //    },
            //    new Beer
            //    {
            //        Name = "Twist",
            //        Brand = brandShumensko,
            //        AlchoholPercentage = 2f,
            //        Category = categoryFruit,
            //        Creator = userAdmin,
            //        CreatedOn = DateTime.Now,
            //        Images = new[] { new Image() { Path = "~/Images/Beers/Shumensko_Twist.jpg" } }
            //    }
            //);

            //// Events
            //var eventDenver = new Event()
            //{
            //    Title = "Denver Beerfest",
            //    Creator = userAdmin,
            //    Date = DateTime.Now.AddDays(10),
            //    Image = new Image() { Path = "~/Images/Events/Denver.jpg" },
            //    Location = "Denver"
            //};
            //eventDenver.UsersEvents = context.Users.Local.Take(30).Select(user => new UsersEvents() { Event = eventDenver, User = user }).ToArray();

            //var eventBelgrade = new Event()
            //{
            //    Title = "Belgrade Beerfest",
            //    Creator = context.Users.Local.Skip(10).First(),
            //    Date = DateTime.Now.AddDays(15),
            //    Image = new Image() { Path = "~/Images/Events/Belgrade.jpg" },
            //    Location = "Belgrade"
            //};
            //eventBelgrade.UsersEvents = context.Users.Local.Skip(30).Take(30).Select(user => new UsersEvents() { Event = eventBelgrade, User = user }).ToArray();

            //var eventPalmBeachNewTimes = new Event()
            //{
            //    Title = "Palm Beach New Times Beerfest",
            //    Creator = context.Users.Local.Skip(60).First(),
            //    Date = DateTime.Now.AddDays(15),
            //    Image = new Image() { Path = "~/Images/Events/PalmBeachNewTimes.jpg" },
            //    Location = "Palm Beach"
            //};
            //eventPalmBeachNewTimes.UsersEvents = context.Users.Local.Skip(60).Take(30).Select(user => new UsersEvents() { Event = eventPalmBeachNewTimes, User = user }).ToArray();

            //var eventTheAustralian = new Event()
            //{
            //    Title = "The Australian Beerfest",
            //    Creator = context.Users.Local.Skip(13).First(),
            //    Date = DateTime.Now.AddDays(15),
            //    Image = new Image() { Path = "~/Images/Events/TheAustralian.jpg" },
            //    Location = "Sydney"
            //};
            //eventTheAustralian.UsersEvents = context.Users.Local.Skip(90).Select(user => new UsersEvents() { Event = eventTheAustralian, User = user }).ToArray();

            //context.Events.AddOrUpdate(eventDenver, eventBelgrade, eventPalmBeachNewTimes, eventTheAustralian);

            //context.SaveChanges();
        }
    }
}
