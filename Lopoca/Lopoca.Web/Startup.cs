using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Lopoca.Web.Startup))]
namespace Lopoca.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
