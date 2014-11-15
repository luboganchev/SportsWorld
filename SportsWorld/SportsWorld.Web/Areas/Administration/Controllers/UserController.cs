using Kendo.Mvc.UI;
using SportsWorld.Data;
using SportsWorld.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace SportsWorld.Web.Areas.Administration.Controllers
{
    using AutoMapper.QueryableExtensions;
    using AutoMapper;
    using SportsWorld.Web.Areas.Administration.Models;

    public class UserController : AdminKendoGridController
    {
        public UserController(ISportsWorldData data)
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
                .To<UserViewModel>();
        }

        protected override T GetById<T>(object id)
        {
            return this.data.Users.Find(id) as T;
        }

        [HttpPost]
        public ActionResult Create([DataSourceRequest]DataSourceRequest request, UserViewModel model)
        {
            var dbModel = base.Create<AppUser>(model);
            if (dbModel != null) model.ID = dbModel.Id;
            return this.GridOperation(model, request);
        }

        [HttpPost]
        public ActionResult Update([DataSourceRequest]DataSourceRequest request, UserViewModel model)
        {
            base.Update<AppUser, UserViewModel>(model, model.ID);
            return this.GridOperation(model, request);
        }

        [HttpPost]
        public ActionResult Destroy([DataSourceRequest]DataSourceRequest request, UserViewModel model)
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