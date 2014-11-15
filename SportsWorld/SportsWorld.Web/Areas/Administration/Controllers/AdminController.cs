namespace SportsWorld.Web.Areas.Administration.Controllers
{
    using SportsWorld.Data;
    using System.Web.Mvc;

    [Authorize(Roles = "admin")]
    public abstract class AdminController : Controller
    {
        protected ISportsWorldData data;

        protected AdminController(ISportsWorldData data)
        {
            this.data = data;
        }
    }
}