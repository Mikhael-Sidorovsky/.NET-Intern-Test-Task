using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ShopCart
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute("Login", "Account/{action}/{id}", new { controller = "Account", action = "Login", id = UrlParameter.Optional },
                             new[] { "ShopCart.Controllers" });
            //*****************************************

            routes.MapRoute("Register", "Account/{action}/{id}", new { controller = "Account", action = "Register", id = UrlParameter.Optional },
                             new[] { "ShopCart.Controllers" });
            //*****************************************

            routes.MapRoute("Cart", "Cart/{action}/{id}", new { controller = "Cart", action = "Index", id = UrlParameter.Optional }, 
                             new[] { "ShopCart.Controllers" });
            //*****************************************

            routes.MapRoute("Shop", "Shop/{action}/{id}", new { controller = "Shop", action = "Index", id = UrlParameter.Optional },
                             new[] { "ShopCart.Controllers" });
            //*****************************************

            routes.MapRoute("About", "Home/{action}/{id}", new { controller = "Home", action = "About", id = UrlParameter.Optional },
                             new[] { "ShopCart.Controllers" });
            //*****************************************

            routes.MapRoute("Home", "{home}", new { controller = "Home", action = "Index" },
                             new[] { "ShopCart.Controllers" });
            //*****************************************

            routes.MapRoute("Default", "", new { controller = "Home", action = "Index"},
                             new[] { "ShopCart.Controllers" });
            //*****************************************

        }
    }
}
