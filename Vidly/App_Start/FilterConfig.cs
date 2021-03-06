﻿using System.Web;
using System.Web.Mvc;

namespace Vidly
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new AuthorizeAttribute()); //preventing unauthorized access to all source of website
            filters.Add(new RequireHttpsAttribute()); //allow only https access, but seem to do this automatically in new version of vs
        }
    }
}
