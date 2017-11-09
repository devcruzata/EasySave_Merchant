using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Project.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");


            routes.MapRoute(
            name: "MerchantDashboard",
            url: "merchant/dashboard",
            defaults: new { controller = "Home", action = "MerchantDashboard" }
            );

            routes.MapRoute(
            name: "CustomerHome",
            url: "customer/customerHome",
            defaults: new { controller = "Customer", action = "CustomerHome" }
            );

            routes.MapRoute(
            name: "TransactionHome",
            url: "customer/transaction",
            defaults: new { controller = "Transactions", action = "TransactionHome" }
            );

            routes.MapRoute(
            name: "RedemHome",
            url: "customer/redemtransaction",
            defaults: new { controller = "Transactions", action = "RedemTransHome" }
            );

            routes.MapRoute(
            name: "RefundHome",
            url: "customer/refundtransaction",
            defaults: new { controller = "Transactions", action = "RefundTransHome" }
            );

            routes.MapRoute(
            name: "Setings",
            url: "merchant/setings",
            defaults: new { controller = "Setings", action = "ProgramSetings" }
            );

            routes.MapRoute(
            name: "Fliers",
            url: "merchant/fliers",
            defaults: new { controller = "Home", action = "AddFliers" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Authentication", action = "MerchantLogin", id = UrlParameter.Optional }
            );
        }
    }
}