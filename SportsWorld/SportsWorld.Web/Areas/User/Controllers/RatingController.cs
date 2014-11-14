namespace SportsWorld.Web.Areas.User.Controllers
{
    using SportsWorld.Data;
    using SportsWorld.Models;
    using SportsWorld.Web.Areas.User.Models;
    using System;
    using System.Linq;
    using System.Web.Mvc;
    using Microsoft.AspNet.Identity;

    public class RatingController : BaseUserController
    {
        public RatingController(ISportsWorldData data)
            : base(data)
        {
        }

        [HttpPost]
        public ActionResult Rate(RatingViewModel model)
        {
            if (ModelState.IsValid)
            {
                var field = this.data.Fields.Find(model.ID);
                if (field != null)
                {
                    var currentUserID = this.User.Identity.GetUserId();

                    var rateEntity = field.FieldRatings.FirstOrDefault(rate => rate.UserID == currentUserID);
                    if (rateEntity == null)
                    {
                        var newFieldRating = new FieldRating
                        {
                            FieldID = model.ID,
                            RateOn = DateTime.Now,
                            UserID = currentUserID,
                            Value = model.Rating
                        };

                        this.data.FieldRatings.Add(newFieldRating);
                    }
                    else
                    {
                        rateEntity.Value = model.Rating;
                        this.data.FieldRatings.Update(rateEntity);
                    }

                    this.data.SaveChanges();

                    var updatedData = this.data.FieldRatings.All()
                        .Where(rate => rate.FieldID == model.ID)
                        .ToArray();

                    var votesCount = updatedData.Count();
                    var averageRate = updatedData.Average(rate => rate.Value);

                    return Json(new { success = true, averageRate = averageRate, votesCount = votesCount });
                }

                return Json(new { success = false });
            }

            return Json(new { success = false });
        }
    }
}