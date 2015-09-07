using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Catalog
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //    routes.MapRoute(
            //    name: "Admin",
            //    url: "Admin/{category}/{id}",
            //    defaults: new
            //    {
            //        controller = "Admin",
            //        action = "Index",
            //        category = UrlParameter.Optional,
            //        id = UrlParameter.Optional
            //);
            routes.MapRoute(
         name: "ShopBuy",
         url: "Shop/Buy/{productId}",
         defaults: new
         {
             controller = "Shop",
             action = "Buy",

             productId = UrlParameter.Optional
         }/*,constraints: new {id=@"\d+"}*/
     );
            routes.MapRoute(
           name: "CatalogAddToCArt",
           url: "catalog/Buy/{productId}",
           defaults: new
           {
               controller = "catalog",
               action = "Buy",

               productId = UrlParameter.Optional
           }/*,constraints: new {id=@"\d+"}*/
       );
            routes.MapRoute(
         name: "Summary",
         url: "catalog/OrderSummary/{id}",
         defaults: new
         {
             controller = "catalog",
             action = "OrderSummary",

             productId = UrlParameter.Optional
         }/*,constraints: new {id=@"\d+"}*/
     );

            routes.MapRoute(
             name: "Catalog",
             url: "catalog/{category}/{id}",
             defaults: new
             {
                 controller = "catalog",
                 action = "Index",
                 category = UrlParameter.Optional,
                 id = UrlParameter.Optional
             }/*,constraints: new {id=@"\d+"}*/
         );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

        }
    }
}
