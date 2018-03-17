using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PMCP2.Startup))]
namespace PMCP2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
