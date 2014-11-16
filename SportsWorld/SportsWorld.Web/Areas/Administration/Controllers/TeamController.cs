namespace SportsWorld.Web.Areas.Administration.Controllers
{
    using AutoMapper.QueryableExtensions;
    using Kendo.Mvc.UI;
    using SportsWorld.Data;
    using SportsWorld.Models;
    using SportsWorld.Web.Areas.Administration.Models;
    using System.Collections;
    using System.Web.Mvc;

    public class TeamController : AdminKendoGridController
    {
        public TeamController(ISportsWorldData data)
            : base(data)
        {
        }

        public ActionResult Index()
        {
            return View();
        }

        protected override IEnumerable GetData()
        {
            return this.data.Teams.All()
                .Project()
                .To<TeamViewModel>();
        }

        protected override T GetById<T>(object id)
        {
            return this.data.Teams.Find(id) as T;
        }

        [HttpPost]
        public ActionResult Create([DataSourceRequest]DataSourceRequest request, TeamViewModel model)
        {
            var dbModel = base.Create<Team>(model);
            if (dbModel != null) model.ID = dbModel.ID;
            return this.GridOperation(model, request);
        }

        [HttpPost]
        public ActionResult Update([DataSourceRequest]DataSourceRequest request, TeamViewModel model)
        {
            base.Update<Team, TeamViewModel>(model, model.ID);
            return this.GridOperation(model, request);
        }

        [HttpPost]
        public ActionResult Destroy([DataSourceRequest]DataSourceRequest request, TeamViewModel model)
        {
            if (model != null && ModelState.IsValid)
            {
                this.data.Teams.Delete(model.ID);
                this.data.SaveChanges();
            }

            return this.GridOperation(model, request);
        }
    }
}