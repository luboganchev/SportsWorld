namespace SportsWorld.Web.Areas.Administration.Controllers
{
    using AutoMapper.QueryableExtensions;
    using Kendo.Mvc.UI;
    using SportsWorld.Data;
    using SportsWorld.Models;
    using SportsWorld.Web.Areas.Administration.Models;
    using System.Collections;
    using System.Web.Mvc;

    public class MessageController : AdminKendoGridController
    {
        public MessageController(ISportsWorldData data)
            : base(data)
        {
        }

        public ActionResult Index()
        {
            return View();
        }

        protected override IEnumerable GetData()
        {
            return this.data.Messages.All()
                .Project()
                .To<MessageViewModel>();
        }

        protected override T GetById<T>(object id)
        {
            return this.data.Messages.Find(id) as T;
        }

        [HttpPost]
        public ActionResult Create([DataSourceRequest]DataSourceRequest request, MessageViewModel model)
        {
            var dbModel = base.Create<Message>(model);
            if (dbModel != null) model.ID = dbModel.ID;
            return this.GridOperation(model, request);
        }

        [HttpPost]
        public ActionResult Update([DataSourceRequest]DataSourceRequest request, MessageViewModel model)
        {
            base.Update<Message, MessageViewModel>(model, model.ID);
            return this.GridOperation(model, request);
        }

        [HttpPost]
        public ActionResult Destroy([DataSourceRequest]DataSourceRequest request, MessageViewModel model)
        {
            if (model != null && ModelState.IsValid)
            {
                this.data.Messages.Delete(model.ID);
                this.data.SaveChanges();
            }

            return this.GridOperation(model, request);
        }
    }
}