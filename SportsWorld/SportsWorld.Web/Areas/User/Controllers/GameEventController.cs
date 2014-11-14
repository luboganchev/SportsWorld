namespace SportsWorld.Web.Areas.User.Controllers
{
    using SportsWorld.Data;
    using System.Web.Mvc;

    public class GameEventController : BaseUserController
    {
        public GameEventController(ISportsWorldData data)
            :base(data)
        {

        }

        public ActionResult Reserve()
        {
            return View();
        }
    }
}