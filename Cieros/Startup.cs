using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Cieros.Startup))]
namespace Cieros
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
