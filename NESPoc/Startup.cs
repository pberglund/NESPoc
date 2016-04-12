using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(NESPoc.Startup))]
namespace NESPoc
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
