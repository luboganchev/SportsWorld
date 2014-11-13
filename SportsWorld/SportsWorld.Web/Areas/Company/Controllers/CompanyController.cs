namespace SportsWorld.Web.Areas.Company.Controllers
{
    using AutoMapper;
    using Microsoft.AspNet.Identity;
    using SportsWorld.Data;
    using SportsWorld.Models;
    using SportsWorld.Web.Areas.Company.Models;
    using SportsWorld.Web.Utils;
    using System;
    using System.Linq;
    using System.Web.Mvc;

    public class CompanyController : BaseCompanyController
    {
        public CompanyController(ISportsWorldData data)
            : base(data)
        {
        }

        public ActionResult Index()
        {
            var currentCompany = GetCurrentCompany();
            var model = Mapper.Map<CompanyDetailsViewModel>(currentCompany);

            var allCountries = this.data.Countries.All().ToArray();
            model.Countries = allCountries;

            return View(model);
        }

        public ActionResult Edit(CompanyDetailsViewModel model)
        {
            if (ModelState.IsValid)
            {
                var currentCompany = GetCurrentCompany();

                currentCompany.CountryID = model.CountryID;
                currentCompany.Name = model.Name;
                currentCompany.EstablishedYear = model.EstablishedYear;

                try
                {
                    if (model.CompanyLogo != null)
                    {
                        currentCompany.Image = DataModelsHelper.GetResizedImageInstance(model.CompanyLogo);
                    }
                }
                catch (InvalidOperationException exception)
                {
                    ModelState.AddModelError("", exception.Message);

                    return RedirectToAction("Index", model);
                }

                this.data.Companies.Update(currentCompany);
                this.data.SaveChanges();
            }

            return RedirectToAction("Index", model);
        }

        private Company GetCurrentCompany()
        {
            var currentUser = this.User.Identity.GetUserId();
            var currentCompany = this.data.Companies.All()
                .FirstOrDefault(company => company.FounderID == currentUser);

            return currentCompany;
        }
    }
}