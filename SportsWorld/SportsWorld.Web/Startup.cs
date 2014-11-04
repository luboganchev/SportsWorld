using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SportsWorld.Web.Startup))]
namespace SportsWorld.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
