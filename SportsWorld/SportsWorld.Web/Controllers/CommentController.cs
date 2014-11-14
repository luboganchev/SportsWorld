namespace SportsWorld.Web.Controllers
{
    using Microsoft.AspNet.Identity;
    using SportsWorld.Data;
    using SportsWorld.Models;
    using SportsWorld.Web.Models;
    using System;
    using System.Linq;
    using System.Web.Mvc;

    public class CommentController : BaseController
    {
        public CommentController(ISportsWorldData data)
            :base(data)
        { 
        }

        [HttpPost]
        public ActionResult Send(CommentViewModel inputModel, int id = -1)
        {
            if (ModelState.IsValid) 
            {
                var currentUser = User.Identity.GetUserId();
                var currentCompany = this.data.Companies.All()
                    .FirstOrDefault(company => company.FounderID == currentUser);

                Field model = null;

                if (currentCompany != null)
                {
                    model = GetFieldData(id, currentCompany.ID);
                }
                else
                {
                    model = this.data.Fields.Find(id);
                }

                if (model == null)
                {
                    return Json(new { Message = "You cant post message for this field" });
                }

                if (string.IsNullOrEmpty(inputModel.Message))
                {
                    return Json(new { Message = "You cant send empty message!" });
                }

                var newComment = new Comment
                {
                    FieldID = model.ID,
                    PostedOn = DateTime.Now,
                    AuthorID = currentUser,
                    Content = inputModel.Message.Trim()
                };

                model.Comments.Add(newComment);
                this.data.SaveChanges();

                return Json(new { Success = "true", id = model.ID });
            }

            return Json(new { Message = "Message text is too much."});
        }

        private Field GetFieldData(int id, int companyID)
        {
            if (id == -1)
            {
                return null;
            }

            var model = this.data.Fields.Find(id);

            if (model == null)
            {
                return null;
            }

            if (model.CompanyID != companyID)
            {
                return null;
            }

            return model;
        }
    }
}