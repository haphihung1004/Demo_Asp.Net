using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SachOnline
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "SachOnline", action = "Index", id = UrlParameter.Optional }
                //thêm hàng sau để tránh xung đột giữa các controllẻr Home
                , namespaces: new[] {"SachOnline.Controllers"}
            );
        }
    }
}
