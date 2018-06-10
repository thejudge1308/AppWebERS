using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AppWebERS.Startup))]
namespace AppWebERS
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
