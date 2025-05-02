using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcStok.Models.Entity;

using Resources;

public class KategoriController : Controller
{
    MvcDbStokEntities db = new MvcDbStokEntities();

    // Herkes görebilir
    [AllowAnonymous]
    public ActionResult Index()
    {
        // ViewBag üzerinden metinleri çoklu dile göre gönderiyoruz
        ViewBag.CategoryId = Labels.CategoryId;
        ViewBag.CategoryName = Labels.CategoryName;
        ViewBag.Delete = Labels.Delete;
        ViewBag.Update = Labels.Update;
        ViewBag.NewCategory = Labels.NewCategory;

        var kategoriler = db.TBLKATEGORILER.ToList();
        return View(kategoriler);
    }

    [Authorize]
    [HttpGet]
    public ActionResult YeniKategori()
    {
        return View();
    }

    [Authorize]
    [HttpPost]
    public ActionResult YeniKategori(TBLKATEGORILER p1)
    {
        db.TBLKATEGORILER.Add(p1);
        db.SaveChanges();
        return RedirectToAction("Index");
    }

    [Authorize]
    public ActionResult SIL(int id)
    {
        var kategori = db.TBLKATEGORILER.Find(id);
        db.TBLKATEGORILER.Remove(kategori);
        db.SaveChanges();
        return RedirectToAction("Index");
    }

    [Authorize]
    public ActionResult KategoriGetir(int id)
    {
        var kgtr = db.TBLKATEGORILER.Find(id);
        return View("KategoriGetir", kgtr);
    }

    [Authorize]
    public ActionResult Guncelle(TBLKATEGORILER p1)
    {
        var ktg = db.TBLKATEGORILER.Find(p1.KATEGORIID);
        ktg.KATEGORIAD = p1.KATEGORIAD;
        db.SaveChanges();
        return RedirectToAction("Index");
    }
}
