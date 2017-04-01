using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebRGRApplication.Startup))]
namespace WebRGRApplication
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
