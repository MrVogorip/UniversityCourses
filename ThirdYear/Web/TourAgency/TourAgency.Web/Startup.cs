using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TourAgency.Web.Startup))]
namespace TourAgency.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
