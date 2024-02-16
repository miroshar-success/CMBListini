using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CMBListini.Startup))]
namespace CMBListini
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
