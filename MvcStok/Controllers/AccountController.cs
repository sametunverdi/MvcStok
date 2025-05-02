using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MvcStok.Controllers
{
    public class AccountController : Controller
    {
        // GET: /Account/Login
        public ActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Kategori");

            return View();
        }

        // POST: /Account/Login
        [HttpPost]
        public ActionResult Login(string kullaniciAdi, string sifre)
        {
            if (kullaniciAdi == "admin" && sifre == "1234")
            {
                FormsAuthentication.SetAuthCookie(kullaniciAdi, false);
                return RedirectToAction("Index", "Kategori");
            }

            ViewBag.Error = "Geçersiz kullanıcı adı veya şifre!";
            return View();
        }

        // GET: /Account/Logout
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }

        // GET: /Account/ChangeLanguage
        public ActionResult ChangeLanguage(string language)
        {
            // 1) 'language' adında bir çerez oluştur, 1 yıl geçerli olsun
            var cookie = new HttpCookie("language", language)
            {
                Expires = DateTime.Now.AddYears(1)
            };
            Response.Cookies.Add(cookie);

            // 2) Kullanıcıyı geldiği sayfaya (veya Login sayfasına) geri yönlendir
            string returnUrl = Request.UrlReferrer != null
                ? Request.UrlReferrer.ToString()
                : Url.Action("Login", "Account");
            return Redirect(returnUrl);
        }
    }
}
