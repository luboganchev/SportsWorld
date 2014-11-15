namespace SportsWorld.Web.Areas.Administration.Controllers
{
    using Kendo.Mvc.UI;
    using SportsWorld.Data;
    using SportsWorld.Models;
    using SportsWorld.Web.Areas.Administration.Models;
    using System.Collections;
    using System.Web.Mvc;
    using Kendo.Mvc.Extensions;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using System.Collections.Generic;
    using System.Linq;

    public class FieldController : AdminKendoGridController
    {
        public FieldController(ISportsWorldData data)
            : base(data)
        {
        }

        public ActionResult Index()
        {
            return View();
        }

        protected override IEnumerable GetData()
        {
            return this.data.Fields.All()
                .Project()
                .To<FieldViewModel>();
        }

        protected override T GetById<T>(object id)
        {
            return this.data.Fields.Find(id) as T;
        }

        [HttpPost]
        public ActionResult Create([DataSourceRequest]DataSourceRequest request, FieldViewModel model)
        {
            var dbModel = base.Create<Field>(model);
            if (dbModel != null) model.ID = dbModel.ID;
            return this.GridOperation(model, request);
        }

        [HttpPost]
        public ActionResult Update([DataSourceRequest]DataSourceRequest request, FieldViewModel model)
        {
            base.Update<Field, FieldViewModel>(model, model.ID);
            return this.GridOperation(model, request);
        }

        [HttpPost]
        public ActionResult Destroy([DataSourceRequest]DataSourceRequest request, FieldViewModel model)
        {
            if (model != null && ModelState.IsValid)
            {
                var field = this.data.Fields.Find(model.ID);
                foreach (var rating in field.FieldRatings)
                {
                    this.data.FieldRatings.Delete(rating.ID);
                }

                this.data.SaveChanges();

                foreach (var comment in field.Comments)
                {
                    this.data.Comments.Delete(comment.ID);
                }

                this.data.SaveChanges();

                foreach (var currentEvent in field.GameEvents)
                {
                    foreach (var transaction in currentEvent.Transactions)
                    {
                        this.data.Transactions.Delete(transaction.ID);
                    }
                    this.data.SaveChanges();

                    this.data.GameEvents.Delete(currentEvent.ID);
                }

                this.data.SaveChanges();

                this.data.Fields.Delete(model.ID);
                this.data.SaveChanges();
            }

            return this.GridOperation(model, request);
        }
    }
}