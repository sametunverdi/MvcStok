﻿using System.Web;
using System.Web.Mvc;

namespace MvcStok
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            // Bu satırın olması gerekmiyor
            // filters.Add(new AuthorizeAttribute()); 
        }
    }
}
