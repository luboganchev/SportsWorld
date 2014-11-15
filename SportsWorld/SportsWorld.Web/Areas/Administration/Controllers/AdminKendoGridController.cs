namespace SportsWorld.Web.Areas.Administration.Controllers
{
    using AutoMapper;
    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;
    using SportsWorld.Data;
    using SportsWorld.Web.Areas.Administration.Models;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Web.Mvc;

    public abstract class AdminKendoGridController : AdminController
    {
        public AdminKendoGridController(ISportsWorldData data)
            : base(data)
        {
        }

        protected abstract IEnumerable GetData();

        protected abstract T GetById<T>(object id) where T : class;

        [HttpPost]
        public ActionResult Read([DataSourceRequest]DataSourceRequest request)
        {
            var data = this.GetData()
                .ToDataSourceResult(request);

            return this.Json(data);
        }

        [NonAction]
        protected virtual T Create<T>(object model) where T : class
        {
            if (model != null && ModelState.IsValid)
            {
                var dbModel = Mapper.Map<T>(model);
                this.ChangeEntityStateAndSave(dbModel, EntityState.Added);
                return dbModel;
            }

            return null;
        }

        [NonAction]
        protected virtual void Update<TModel, TViewModel>(TViewModel model, object id)
            where TModel : class
            where TViewModel : class
        {
            if (model != null && ModelState.IsValid)
            {
                var dbModel = this.GetById<TModel>(id);
                Mapper.Map<TViewModel, TModel>(model, dbModel);
                this.ChangeEntityStateAndSave(dbModel, EntityState.Modified);
            }
        }

        protected JsonResult GridOperation<T>(T model, [DataSourceRequest]DataSourceRequest request)
        {
            return Json(new[] { model }.ToDataSourceResult(request, ModelState));
        }

        private void ChangeEntityStateAndSave(object dbModel, EntityState state)
        {
            var entry = this.data.Context.Entry(dbModel);
            entry.State = state;
            this.data.SaveChanges();
        }
    }
}