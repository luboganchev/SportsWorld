namespace SportsWorld.Web.Areas.User.Controllers
{
    using SportsWorld.Data;
    using System.Web.Mvc;

    [Authorize(Roles = "admin, user")]
    public abstract class BaseUserController : Controller
    {
        protected ISportsWorldData data;

        protected BaseUserController(ISportsWorldData data)
        {
            this.data = data;
        }
    }
}