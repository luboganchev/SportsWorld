namespace SportsWorld.Web.Areas.Administration.Controllers
{
    using Kendo.Mvc.UI;
    using SportsWorld.Data;
    using SportsWorld.Models;
    using SportsWorld.Web.Areas.Administration.Models;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using AutoMapper.QueryableExtensions;

    public class CityController : AdminKendoGridController
    {
        public CityController(ISportsWorldData data)
            : base(data)
        {
        }

        public ActionResult Index()
        {
            return View();
        }

        protected override IEnumerable GetData()
        {
            return this.data.Users.All()
                .Project()
                .To<CityViewModel>();
        }

        protected override T GetById<T>(object id)
        {
            return this.data.Users.Find(id) as T;
        }

        [HttpPost]
        public ActionResult Create([DataSourceRequest]DataSourceRequest request, CityViewModel model)
        {
            var dbModel = base.Create<City>(model);
            if (dbModel != null) model.ID = dbModel.ID;
            return this.GridOperation(model, request);
        }

        [HttpPost]
        public ActionResult Update([DataSourceRequest]DataSourceRequest request, CityViewModel model)
        {
            base.Update<City, CityViewModel>(model, model.ID);
            return this.GridOperation(model, request);
        }

        [HttpPost]
        public ActionResult Destroy([DataSourceRequest]DataSourceRequest request, CityViewModel model)
        {
            if (model != null && ModelState.IsValid)
            {
                this.data.Users.Delete(model.ID);
                this.data.SaveChanges();
            }

            return this.GridOperation(model, request);
        }
    }
}