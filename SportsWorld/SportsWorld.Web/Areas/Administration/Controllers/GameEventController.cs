namespace SportsWorld.Web.Areas.Administration.Controllers
{
    using AutoMapper.QueryableExtensions;
    using Kendo.Mvc.UI;
    using SportsWorld.Data;
    using SportsWorld.Models;
    using SportsWorld.Web.Areas.Administration.Models;
    using System.Collections;
    using System.Web.Mvc;

    public class GameEventController : AdminKendoGridController
    {
        public GameEventController(ISportsWorldData data)
            : base(data)
        {
        }

        public ActionResult Index()
        {
            return View();
        }

        protected override IEnumerable GetData()
        {
            return this.data.GameEvents.All()
                .Project()
                .To<GameEventViewModel>();
        }

        protected override T GetById<T>(object id)
        {
            return this.data.GameEvents.Find(id) as T;
        }

        [HttpPost]
        public ActionResult Create([DataSourceRequest]DataSourceRequest request, GameEventViewModel model)
        {
            var dbModel = base.Create<GameEvent>(model);
            if (dbModel != null) model.ID = dbModel.ID;
            return this.GridOperation(model, request);
        }

        [HttpPost]
        public ActionResult Update([DataSourceRequest]DataSourceRequest request, GameEventViewModel model)
        {
            base.Update<GameEvent, GameEventViewModel>(model, model.ID);
            return this.GridOperation(model, request);
        }

        [HttpPost]
        public ActionResult Destroy([DataSourceRequest]DataSourceRequest request, GameEventViewModel model)
        {
            if (model != null && ModelState.IsValid)
            {
                var gameEvent = this.data.GameEvents.Find(model.ID);

                foreach (var transaction in gameEvent.Transactions)
                {
                    this.data.Transactions.Delete(transaction.ID);
                }
                this.data.SaveChanges();

                foreach (var participant in gameEvent.Participants)
                {
                    this.data.Participants.Delete(participant.GameEventID);
                }
                this.data.SaveChanges();

                this.data.GameEvents.Delete(model.ID);
                this.data.SaveChanges();
            }

            return this.GridOperation(model, request);
        }
    }
}