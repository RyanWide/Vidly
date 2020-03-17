using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Vidly
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            ////for custom routes, need line: however need to change action manually for change in controller
            //routes.MapRoute(
            //    "MoviesByReleaseDate",//name
            //    "movies/released/{year}/{month}",//url
            //    new { controller = "Movies", action = "ByReleaseDate" },//controller mapping
            //    new { year = @"\d{4}" , month = @"\d{2}"}//restriction inform of regular expression, or do ####|#### to set allowed value
            //);

            //cleaner way to do custom routes using attribute:
            routes.MapMvcAttributeRoutes();

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
