using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SachOnline.Startup))]
namespace SachOnline
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
