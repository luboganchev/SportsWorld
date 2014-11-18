namespace SportsWorld.Web.Areas.User.Controllers
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Microsoft.AspNet.Identity;
    using SportsWorld.Data;
    using SportsWorld.Web.Areas.User.Models;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    public class FieldController : BaseUserController
    {
        private const int DefaultPageSize = 6;

        public FieldController(ISportsWorldData data)
            : base(data)
        {
        }

        public ActionResult GetAll(int page = 0)
        {
            var allFields = this.data.Fields.All()
                .OrderByDescending(field => field.DateCreated)
                .Skip(page * DefaultPageSize)
                .Take(DefaultPageSize)
                .Project()
                .To<FieldViewModel>()
                .ToArray();

            var model = GetAllFieldsViewModel(page, allFields);

            return View(model);
        }


        private PageableFieldViewModel GetAllFieldsViewModel(int page, IEnumerable<FieldViewModel> fields)
        {
            var allItemsCount = this.data.Fields.All()
                .Select(m => m.ID)
                .ToArray()
                .Count();

            int pagesCount = 1;
            if (allItemsCount % DefaultPageSize == 0)
            {
                pagesCount = allItemsCount / DefaultPageSize;
            }
            else
            {
                pagesCount = (allItemsCount / DefaultPageSize) + 1;
            }

            var model = new PageableFieldViewModel
            {
                CurrentPage = page,
                PagesCount = pagesCount,
                Fields = fields
            };

            return model;
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
            if (userVote != null)
            {
                viewModel.UserVote = userVote.Value;
            }

            return View(viewModel);
        }


    }
}