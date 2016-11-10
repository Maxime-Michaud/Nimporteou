using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Nimporteou.Startup))]
namespace Nimporteou
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
