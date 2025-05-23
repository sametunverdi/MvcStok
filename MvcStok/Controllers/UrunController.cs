﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcStok.Models.Entity;
using Resources;


namespace MvcStok.Controllers
{
    [Authorize]
    public class UrunController : Controller
    {
        MvcDbStokEntities db = new MvcDbStokEntities();

        // Ürünler listeleniyor
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

            // Ürünleri çekiyoruz
            var degerler = db.TBLURUNLER.ToList();
            return View(degerler);
        }

        // Ürün ekleme sayfası
        [HttpGet]

        public ActionResult UrunEkle()
        {
            // Kategorileri çekiyoruz ve dropdown list olarak View'a gönderiyoruz
            List<SelectListItem> degerler = (from i in db.TBLKATEGORILER.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.KATEGORIAD,
                                                 Value = i.KATEGORIID.ToString()
                                             }).ToList();
            ViewBag.Kategoriler = degerler;
            return View();
        }

        // Ürün ekleme işlemi
        [HttpPost]
        public ActionResult UrunEkle(TBLURUNLER p1)
        {
            try
            {
                var kategori = db.TBLKATEGORILER.Find(p1.URUNKATEGORI);
                if (kategori != null)
                {
                    p1.TBLKATEGORILER = kategori;
                    db.TBLURUNLER.Add(p1);
                    db.SaveChanges();
                    return Json(new { success = true });
                }
                return Json(new { success = false });
            }
            catch (Exception)
            {
                return Json(new { success = false });
            }
        }


        // Ürün silme işlemi
        public ActionResult SIL(int id)
        {
            var urun = db.TBLURUNLER.Find(id);
            if (urun != null)
            {
                db.TBLURUNLER.Remove(urun);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public JsonResult UrunGetir(int id)
        {
            var urun = db.TBLURUNLER.Find(id);
            if (urun != null)
            {
                return Json(new
                {
                    URUNID = urun.URUNID,
                    URUNAD = urun.URUNAD,
                    MARKA = urun.MARKA,
                    FIYAT = urun.FIYAT,
                    STOK = urun.STOK
                }, JsonRequestBehavior.AllowGet);
            }
            return Json(null, JsonRequestBehavior.AllowGet);
        }

        // Ürün güncelleme
        [HttpPost]
        public JsonResult UrunGuncelle(TBLURUNLER urun)
        {
            var urunDb = db.TBLURUNLER.Find(urun.URUNID);
            if (urunDb != null)
            {
                urunDb.URUNAD = urun.URUNAD;
                urunDb.MARKA = urun.MARKA;
                urunDb.FIYAT = urun.FIYAT;
                urunDb.STOK = urun.STOK;
                db.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }


        [HttpGet]
        public JsonResult KategoriListesi()
        {
            try
            {
                var kategoriler = db.TBLKATEGORILER.Select(k => new
                {
                    KATEGORIID = k.KATEGORIID,
                    KATEGORIAD = k.KATEGORIAD
                }).ToList();

                return Json(kategoriler, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }

    }
}
