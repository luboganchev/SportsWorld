namespace SportsWorld.Web.Areas.Administration.Controllers
{
    using AutoMapper.QueryableExtensions;
    using Kendo.Mvc.UI;
    using SportsWorld.Data;
    using SportsWorld.Models;
    using SportsWorld.Web.Areas.Administration.Models;
    using System.Collections;
    using System.Web.Mvc;

    public class CountryController : AdminKendoGridController
    {
        public CountryController(ISportsWorldData data)
            : base(data)
        {
        }

        public ActionResult Index()
        {
            return View();
        }

        protected override IEnumerable GetData()
        {
            return this.data.Countries.All()
                .Project()
                .To<CountryViewModel>();
        }

        protected override T GetById<T>(object id)
        {
            return this.data.Countries.Find(id) as T;
        }

        [HttpPost]
        public ActionResult Create([DataSourceRequest]DataSourceRequest request, CountryViewModel model)
        {
            var dbModel = base.Create<Country>(model);
            if (dbModel != null) model.ID = dbModel.ID;
            return this.GridOperation(model, request);
        }

        [HttpPost]
        public ActionResult Update([DataSourceRequest]DataSourceRequest request, CountryViewModel model)
        {
            base.Update<Country, CountryViewModel>(model, model.ID);
            return this.GridOperation(model, request);
        }

        [HttpPost]
        public ActionResult Destroy([DataSourceRequest]DataSourceRequest request, CountryViewModel model)
        {
            if (model != null && ModelState.IsValid)
            {
                this.data.Countries.Delete(model.ID);
                this.data.SaveChanges();
            }

            return this.GridOperation(model, request);
        }
    }
}