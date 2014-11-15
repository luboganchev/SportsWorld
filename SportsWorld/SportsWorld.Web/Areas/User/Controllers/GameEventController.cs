namespace SportsWorld.Web.Areas.User.Controllers
{
    using SportsWorld.Data;
    using SportsWorld.Web.Areas.User.Models;
    using System.Web.Mvc;
    using System.Linq;
    using System;
    using Microsoft.AspNet.Identity;
    using AutoMapper;
    using SportsWorld.Models;

    public class GameEventController : BaseUserController
    {
        public GameEventController(ISportsWorldData data)
            : base(data)
        {
        }

        public ActionResult Reserve(GameEventDataModel model)
        {
            if (ModelState.IsValid)
            {
                var viewModel = GetReserveViewModel(model);
                if (viewModel == null)
                {
                    return RedirectToAction("GetAll", "Field");
                }

                return View(viewModel);
            }

            return RedirectToAction("GetAll", "Field");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Reserve(ReserveGameViewModel model)
        {
            if (ModelState.IsValid)
            {
                var eventDataModel = new GameEventDataModel
                {
                    DayOfYear = model.StartTime.DayOfYear,
                    StartHour = model.StartTime.Hour,
                    FieldID = model.FieldID
                };

                var field = this.CheckAvailability(eventDataModel);
                if (field == null)
                {
                    ModelState.AddModelError("", "Not available for reservations");
                    return View(model);
                }

                var currentUserID = this.User.Identity.GetUserId();

                var newGameEvent = new GameEvent
                {
                    StartTime = model.StartTime,
                    EndTime = model.StartTime.AddHours(1),
                    FieldID = model.FieldID,
                    CreatorID = currentUserID
                };

                this.data.GameEvents.Add(newGameEvent);
                var participant = new Participant
                {
                    UserID = currentUserID,
                    GameEventID = newGameEvent.ID
                };

                newGameEvent.Participants.Add(participant);

                //Dummy simulation of payment
                var transaction = new Transaction
                {
                    ApprovalStatus = EnumApprovalStatus.Approved,
                    GameEventID = newGameEvent.ID,
                    UserID = currentUserID
                };

                var company = this.data.Companies.Find(field.CompanyID);
                company.TotalRevenue += field.PricePerHour;
                this.data.Companies.Update(company);

                this.data.Transactions.Add(transaction);
                this.data.SaveChanges();

                return RedirectToAction("Details", "Field", new { id = model.FieldID });
            }

            return View(model);
        }

        private Field CheckAvailability(GameEventDataModel model)
        {
            var field = this.data.Fields.Find(model.FieldID);

            if (field == null)
            {
                return null;
            }

            var isAlreadyReserved = field.GameEvents
                .Any(ev => ev.StartTime.Hour == model.StartHour && ev.StartTime.DayOfYear == model.DayOfYear);
            if (isAlreadyReserved)
            {
                return null;
            }

            return field;
        }

        private ReserveGameViewModel GetReserveViewModel(GameEventDataModel model)
        {
            var field = CheckAvailability(model);
            if (field == null)
            {
                return null;
            }

            DateTime currentDate = new DateTime(DateTime.Now.Year, 1, 1).AddDays(model.DayOfYear - 1);
            var currentUserID = this.User.Identity.GetUserId();

            var viewModel = new ReserveGameViewModel
            {
                FieldID = field.ID,
                Field = field,
                StartTime = currentDate.AddHours(model.StartHour),
                EndTime = currentDate.AddHours(model.StartHour + 1)
            };

            var paymentDetails = this.data.CardInfoes.All()
                .FirstOrDefault(card => card.UserID == currentUserID);
            if (paymentDetails != null)
            {
                viewModel.CVV = paymentDetails.CVV;
                viewModel.DateValid = paymentDetails.DateValid;
                viewModel.Number = paymentDetails.Number;
                viewModel.Type = paymentDetails.Type;
            }

            return viewModel;
        }
    }
}