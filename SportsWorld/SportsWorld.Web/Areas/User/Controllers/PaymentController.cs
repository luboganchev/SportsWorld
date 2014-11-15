namespace SportsWorld.Web.Areas.User.Controllers
{
    using SportsWorld.Data;
    using SportsWorld.Web.Areas.User.Models;
    using System.Web.Mvc;

    public class PaymentController : BaseUserController
    {
        public PaymentController(ISportsWorldData data)
            : base(data)
        {
        }

    }
}