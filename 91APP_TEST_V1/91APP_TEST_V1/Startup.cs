using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(_91APP_TEST_V1.Startup))]
namespace _91APP_TEST_V1
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
