namespace SportsWorld.Web.Areas.Company.Controllers
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using SportsWorld.Data;
    using SportsWorld.Models;
    using SportsWorld.Web.Areas.Company.Models;
    using System;
    using System.Web.Mvc;
    using System.Linq;
    using SportsWorld.Web.Utils;
    using Microsoft.AspNet.Identity;

    public class FieldController : BaseCompanyController
    {
        private int currentCompanyID;

        public FieldController(ISportsWorldData data)
            : base(data)
        {
        }

        public int CompanyID
        {
            get
            {
                if (this.currentCompanyID == 0)
                {
                    this.currentCompanyID = SetCurrentCompanyId();
                }

                return this.currentCompanyID;
            }
        }

        public ActionResult GetMineFields()
        {
            var model = this.data.Fields.All()
                .Where(field => field.CompanyID == this.CompanyID)
                .Project()
                .To<MineFieldGridViewModel>()
                .ToArray();

            return View(model);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateFieldViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string[] givenCityData = model.CityInfo.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    var givenCity = givenCityData[0].Trim();
                    var givenCountry = givenCityData[1].Trim();
                    City existingCity = this.data.Cities.All()
                        .FirstOrDefault(city => city.Name.ToLower() == givenCity.ToLower()
                        && city.Country.Name.ToLower() == givenCountry.ToLower());

                    if (existingCity == null)
                    {
                        var countryID = this.data.Countries.All()
                            .FirstOrDefault(country => country.Name.Equals(givenCountry, StringComparison.OrdinalIgnoreCase))
                            .ID;

                        existingCity = new City
                        {
                            Name = givenCity,
                            CountryID = countryID
                        };

                        this.data.Cities.Add(existingCity);
                    }

                    model.CityID = existingCity.ID;
                    var newField = Mapper.Map<CreateFieldViewModel, Field>(model);

                    newField.DateCreated = DateTime.Now;

                    newField.CompanyID = this.CompanyID;
                    this.data.Fields.Add(newField);

                    if (model.Image != null)
                    {
                        newField.Image = DataModelsHelper.GetResizedImageInstance(model.Image);
                    }

                    this.data.SaveChanges();

                    return RedirectToAction("GetMineFields");
                }
                catch (InvalidOperationException exception)
                {
                    ModelState.AddModelError("", exception.Message);

                    return View(model);
                }
            }

            return View(model);
        }

        private int SetCurrentCompanyId()
        {
            var currentUser = User.Identity.GetUserId();
            var currentCompany = this.data.Companies.All()
                .FirstOrDefault(company => company.FounderID == currentUser);

            return currentCompany.ID;
        }
    }
}