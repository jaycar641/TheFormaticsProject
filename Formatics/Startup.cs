using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Formatics.Startup))]
namespace Formatics
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
