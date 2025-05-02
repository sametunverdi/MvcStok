using System.Linq;
using System.Web.Mvc;
using MvcStok.Models.Entity; // Doğru olanı sadece bu
using Resources;
// using Resources; // BU SATIRA GEREK YOK, KALDIRABİLİRSİN

namespace MvcStok.Controllers
{
    [Authorize]
    public class MusteriController : Controller
    {
        MvcDbStokEntities db = new MvcDbStokEntities();

        // GET: Musteri
        [AllowAnonymous]
        public ActionResult Index()
        {
            ViewBag.CustomerId = Labels.CustomerId;
            ViewBag.CustomerName = Labels.CustomerName;
            ViewBag.CustomerSurname = Labels.CustomerSurname;
            ViewBag.Delete = Labels.Delete;
            ViewBag.Update = Labels.Update;
            ViewBag.NewCustomer = Labels.NewCustomer;

            var degerler = db.TBLMUSTERILER.ToList();
            return View(degerler);
        }

        [HttpGet]
        public ActionResult YeniMusteri()
        {
            ViewBag.NewCustomer = Labels.NewCustomer;
            ViewBag.CustomerName = Labels.CustomerName;
            ViewBag.CustomerSurname = Labels.CustomerSurname;
            return View();
        }

        [HttpPost]
        public ActionResult YeniMusteri(TBLMUSTERILER p1)
        {
            db.TBLMUSTERILER.Add(p1);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult SIL(int id)
        {
            var musteri = db.TBLMUSTERILER.Find(id);
            db.TBLMUSTERILER.Remove(musteri);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult MusteriGetir(int id)
        {
            var mus = db.TBLMUSTERILER.Find(id);
            return View("MusteriGetir", mus);
        }

        public ActionResult Guncelle(TBLMUSTERILER p1)
        {
            var musteri = db.TBLMUSTERILER.Find(p1.MUSTERIID);
            musteri.MUSTERIAD = p1.MUSTERIAD;
            musteri.MUSTERISOYAD = p1.MUSTERISOYAD;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
