using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SySSensor.Web.Startup))]
namespace SySSensor.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
