using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebDisk.Web.Startup))]
namespace WebDisk.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
