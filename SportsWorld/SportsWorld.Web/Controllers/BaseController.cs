namespace SportsWorld.Web.Controllers
{
    using SportsWorld.Data;
    using System.Web.Mvc;

    public abstract class BaseController : Controller
    {
        protected ISportsWorldData data;

        protected BaseController(ISportsWorldData data)
        {
            this.data = data;
        }
    }
}