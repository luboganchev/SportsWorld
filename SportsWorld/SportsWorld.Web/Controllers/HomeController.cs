using SportsWorld.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace SportsWorld.Web.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(ISportsWorldData data)
            : base(data)
        {

        }

        public ActionResult Index()
        {
            if (this.User.IsInRole("companyAgent"))
            {
                return this.RedirectToAction("GetMineFields", "Field", new { Area = "Company" });
            }

            if (this.User.IsInRole("user"))
            {
                return this.RedirectToAction("GetAll", "Field", new { Area = "User" });
            }

            if (this.User.IsInRole("admin"))
            {
                return this.RedirectToAction("Index", "Field", new { Area = "User" });
            }

            return View();
        }
    }
}