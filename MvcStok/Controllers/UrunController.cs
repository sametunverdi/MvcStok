using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcStok.Models.Entity;
using Resources; // <-- Resource dosyasını ekliyoruz

namespace MvcStok.Controllers
{
    [Authorize]
    public class UrunController : Controller
    {
        MvcDbStokEntities db = new MvcDbStokEntities();

        public ActionResult Index()
        {
            // Çok dilli metinleri ViewBag'e atıyoruz
            ViewBag.ProductId = Labels.ProductId;
            ViewBag.ProductName = Labels.ProductName;
            ViewBag.Brand = Labels.Brand;
            ViewBag.Price = Labels.Price;
            ViewBag.Stock = Labels.Stock;
            ViewBag.Delete = Labels.Delete;
            ViewBag.Update = Labels.Update;
            ViewBag.NewProduct = Labels.NewProduct;

            var degerler = db.TBLURUNLER.ToList();
            return View(degerler);
        }

        [HttpGet]
        public ActionResult UrunEkle()
        {
            List<SelectListItem> degerler = (from i in db.TBLKATEGORILER.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.KATEGORIAD,
                                                 Value = i.KATEGORIID.ToString()
                                             }).ToList();
            ViewBag.dgr = degerler;
            return View();
        }

        [HttpPost]
        public ActionResult UrunEkle(TBLURUNLER p1)
        {
            var ktg = db.TBLKATEGORILER.Where(m => m.KATEGORIID == p1.TBLKATEGORILER.KATEGORIID).FirstOrDefault();
            p1.TBLKATEGORILER = ktg;
            db.TBLURUNLER.Add(p1);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult SIL(int id)
        {
            var urun = db.TBLURUNLER.Find(id);
            db.TBLURUNLER.Remove(urun);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
