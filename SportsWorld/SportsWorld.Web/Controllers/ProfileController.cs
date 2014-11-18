namespace SportsWorld.Web.Controllers
{
    using System.Web.Mvc;
    using Microsoft.AspNet.Identity;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using System.Linq;
    using SportsWorld.Web.Models;
    using SportsWorld.Data;
    using SportsWorld.Web.Areas.User.Models;
    using SportsWorld.Models;

    [Authorize]
    public class ProfileController : BaseController
    {

        public ProfileController(ISportsWorldData data)
            : base(data)
        {

        }

        public ActionResult Index()
        {
            var currentUserId = this.User.Identity.GetUserId();
            var user = this.data.Users.Find(currentUserId);

            var model = Mapper.Map<ProfileViewModel>(user);
            if (this.User.IsInRole("user"))
            {
                var allEvents = this.data.GameEvents.All()
                    .Where(ev => ev.Participants.Any(p => p.UserID == currentUserId));

                var paymentInfo = this.data.CardInfoes.All()
                    .FirstOrDefault(card => card.UserID == currentUserId);

                if (paymentInfo != null)
                {
                    var cardInfo = Mapper.Map<PaymentDataViewModel>(paymentInfo);
                    model.PaymentInfo = cardInfo;
                }

                model.JoinedGames = allEvents;
            }

            return View(model);
        }

        [Authorize(Roles = "user")]
        public ActionResult PaymentUpdate(PaymentDataViewModel model)
        {
            if (ModelState.IsValid)
            {
                var dbModel = Mapper.Map<CardInfo>(model);

                var currentUserId = this.User.Identity.GetUserId();
                dbModel.UserID = currentUserId;

                var paymentInfo = this.data.CardInfoes.All()
                    .FirstOrDefault(card => card.UserID == currentUserId);

                if (paymentInfo != null)
                {
                    paymentInfo.CVV = dbModel.CVV;
                    paymentInfo.DateValid = dbModel.DateValid;
                    paymentInfo.Number = dbModel.Number;
                    paymentInfo.Type = dbModel.Type;

                    this.data.CardInfoes.Update(paymentInfo);
                }
                else
                {
                    this.data.CardInfoes.Add(dbModel);
                }

                this.data.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}