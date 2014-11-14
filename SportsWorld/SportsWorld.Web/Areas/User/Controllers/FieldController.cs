namespace SportsWorld.Web.Areas.User.Controllers
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using SportsWorld.Data;
    using SportsWorld.Web.Areas.User.Models;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Microsoft.AspNet.Identity;
    public class FieldController : BaseUserController
    {
        public FieldController(ISportsWorldData data)
            : base(data)
        {
        }

        public ActionResult GetAll()
        {
            var allFields = this.data.Fields.All()
                .Project()
                .To<FieldViewModel>()
                .ToArray();

            return View(allFields);
        }

        public ActionResult Details(int id = -1)
        {
            var model = this.data.Fields.Find(id);

            if (model == null)
            {
                return this.RedirectToAction("GetAll");
            }

            var viewModel = Mapper.Map<FieldUserDetailsViewModel>(model);

            var currentUser = this.User.Identity.GetUserId();
            var userVote = model.FieldRatings.FirstOrDefault(rating => rating.UserID == currentUser);
            if(userVote!=null)
            {
                viewModel.UserVote = userVote.Value;
            }

            return View(viewModel);
        }
    }
}