using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Globalization;
using System.Threading;

namespace MvcStok
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_BeginRequest()
        {
            // 1. Tarayıcıdan 'language' adlı çerezi oku
            HttpCookie langCookie = Request.Cookies["language"];

            // 2. Eğer çerez varsa değeri kullan, yoksa "tr" (Türkçe) atılsın
            string lang = (langCookie != null && !string.IsNullOrEmpty(langCookie.Value))
                ? langCookie.Value
                : "tr";

            // 3. Kültür ve UI kültürü ayarlarını yap
            var culture = new CultureInfo(lang);
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;
        }
    }
}
