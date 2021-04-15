using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Blazing_Shop.Startup))]
namespace Blazing_Shop
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
