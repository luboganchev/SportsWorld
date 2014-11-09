namespace SportsWorld.Web.Areas.Company.Controllers
{
    using SportsWorld.Data;
    using System.Web.Mvc;

    //[Authorize(Roles = "admin, companyAgent")]
    public abstract class BaseCompanyController : Controller
    {
        protected ISportsWorldData data;

        protected BaseCompanyController(ISportsWorldData data)
        {
            this.data = data;
        }
    }
}