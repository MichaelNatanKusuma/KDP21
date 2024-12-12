using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RegisterLogin.Startup))]
namespace RegisterLogin
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
