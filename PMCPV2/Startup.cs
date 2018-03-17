using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PMCPV2.Startup))]
namespace PMCPV2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
