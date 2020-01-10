using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ShopCart.Startup))]
namespace ShopCart
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
