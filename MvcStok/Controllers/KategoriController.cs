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

    [AllowAnonymous]
    public ActionResult Index()
    {
        // Etiketler
        ViewBag.CategoryId = Labels.CategoryId;
        ViewBag.CategoryName = Labels.CategoryName;
        ViewBag.Delete = Labels.Delete;
        ViewBag.Update = Labels.Update;
        ViewBag.NewCategory = Labels.NewCategory;

        // Kategorileri veritabanından çek
        var kategoriListesi = db.TBLKATEGORILER.ToList();

        return View(kategoriListesi); // View'a gönder
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
        if (!ModelState.IsValid)
        {
            return View("YeniKategori");
        }

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
    public JsonResult KategoriGetir(int id)
    {
        var kgtr = db.TBLKATEGORILER.Find(id);
        if (kgtr != null)
        {
            return Json(new
            {
                KATEGORIID = kgtr.KATEGORIID,
                KATEGORIAD = kgtr.KATEGORIAD,
                KATEGORIAD_EN = kgtr.KATEGORIAD_EN // <-- Bu eklendi
            }, JsonRequestBehavior.AllowGet);
        }
        return Json(null, JsonRequestBehavior.AllowGet);
    }

    [Authorize]
    [HttpPost]
    public ActionResult KategoriGuncelle(TBLKATEGORILER kategori)
    {
        if (kategori == null || kategori.KATEGORIID == 0)
        {
            return Json(new { success = false, message = "Geçersiz veri." });
        }

        var kategoriDb = db.TBLKATEGORILER.Find(kategori.KATEGORIID);
        if (kategoriDb != null)
        {
            kategoriDb.KATEGORIAD = kategori.KATEGORIAD;
            kategoriDb.KATEGORIAD_EN = kategori.KATEGORIAD_EN;
            db.SaveChanges();
            return Json(new { success = true });
        }

        return Json(new { success = false, message = "Kategori bulunamadı." });
    }

    [Authorize]
    [HttpPost]
    public ActionResult YeniKategoriAjax(TBLKATEGORILER kategori)
    {
        if (string.IsNullOrWhiteSpace(kategori.KATEGORIAD) || string.IsNullOrWhiteSpace(kategori.KATEGORIAD_EN))
        {
            return Json(new { success = false, message = "Kategori adı boş olamaz." });
        }

        db.TBLKATEGORILER.Add(kategori);
        db.SaveChanges();

        return Json(new { success = true });
    }


}
