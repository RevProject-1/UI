using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ClientManagement.UI.MVC.Startup))]
namespace ClientManagement.UI.MVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
