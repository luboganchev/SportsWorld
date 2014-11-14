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
    using Newtonsoft.Json;

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
            var newModel = new CreateEditFieldViewModel();
            return View(newModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateEditFieldViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var updatedViewModel = this.UpdateCreateEditViewModel(model);
                    if (updatedViewModel == null)
                    {
                        ModelState.AddModelError("", "Data for City is not correct should be in format: City, Country");
                        return View(model);
                    }

                    var newField = Mapper.Map<CreateEditFieldViewModel, Field>(updatedViewModel);
                    newField.CompanyID = this.CompanyID;
                    newField.DateCreated = DateTime.Now;
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

        public ActionResult Details(int id = -1)
        {
            var model = this.GetFieldData(id);
            if (model == null)
            {
                return this.RedirectToAction("GetMineFields");
            }

            var viewModel = Mapper.Map<FieldCompanyDetailsViewModel>(model);

            return View(viewModel);
        }

        public ActionResult Edit(int id)
        {
            var model = this.GetFieldData(id);
            if (model == null)
            {
                return this.RedirectToAction("GetMineFields");
            }

            var viewModel = Mapper.Map<CreateEditFieldViewModel>(model);

            return PartialView("_Edit", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CreateEditFieldViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var currentFieldFromDb = this.GetFieldData(model.ID);
                    if (currentFieldFromDb == null)
                    {
                        return Json(
                            new
                            {
                                Success = false,
                                Message = "You dont have permission to update this field."
                            });
                    }

                    var updatedViewModel = this.UpdateCreateEditViewModel(model);
                    if (updatedViewModel == null)
                    {
                        return Json(
                            new
                            {
                                Success = false,
                                Message = "Data for City is not correct should be in format: City, Country"
                            });
                    }

                    currentFieldFromDb = Mapper.Map<CreateEditFieldViewModel, Field>(updatedViewModel, currentFieldFromDb);
                    this.data.Fields.Update(currentFieldFromDb);

                    if (model.Image != null)
                    {
                        currentFieldFromDb.Image = DataModelsHelper.GetResizedImageInstance(model.Image);
                    }

                    this.data.SaveChanges();
                    var returnedModel = Mapper.Map<CreateEditFieldViewModel>(currentFieldFromDb);

                    return Json(new { Success = true, Message = "Success", data = returnedModel });
                }
                catch (InvalidOperationException exception)
                {
                    ModelState.AddModelError("", exception.Message);

                    return Json(new { Success = false, Message = exception.Message });
                }
            }

            return Json(new { Success = false, Message = "Submit data is not valid." });
        }

        private CreateEditFieldViewModel UpdateCreateEditViewModel(CreateEditFieldViewModel model)
        {
            string[] givenCityData = model.CityInfo.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            if (givenCityData.Length < 2)
            {
                return null;
            }

            var givenCity = givenCityData[0].Trim();
            var givenCountry = givenCityData[1].Trim();
            City existingCity = this.data.Cities.All()
                .FirstOrDefault(city => city.Name.ToLower() == givenCity.ToLower()
                && city.Country.Name.ToLower() == givenCountry.ToLower());

            if (existingCity == null)
            {
                var currentCountry = this.data.Countries.All()
                    .FirstOrDefault(country => country.Name.Equals(givenCountry, StringComparison.OrdinalIgnoreCase));
                int countryID = 0;

                if (currentCountry == null)
                {
                    return null;
                }
                else
                {
                    countryID = currentCountry.ID;
                }

                existingCity = new City
                {
                    Name = givenCity,
                    CountryID = countryID
                };

                this.data.Cities.Add(existingCity);
            }

            model.CityID = existingCity.ID;

            return model;
        }

        private Field GetFieldData(int id)
        {
            if (id == -1)
            {
                return null;
            }

            var model = this.data.Fields.Find(id);

            if (model == null)
            {
                return null;
            }

            if (model.CompanyID != this.CompanyID)
            {
                return null;
            }

            return model;
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