using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using TulipShopping.Models;

namespace TulipShopping.Controllers
{
    public class AdminController : Controller
    {


        private TulipShoppingModel db = new TulipShoppingModel();



        public ActionResult Index()
        {
            try
            {
                return View(db.AdminTable.OrderByDescending(K => K.Id).ToList());
            }
            catch
            {
                ViewBag.Basarisiz = "Anasayfa yüklenirken bir hata oluştu !!!";
                return View();
            }
        }


        public ActionResult Details(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                AdminTable adminTable = db.AdminTable.Find(id);
                if (adminTable == null)
                {
                    return HttpNotFound();
                }
                return View(adminTable);
            }
            catch
            {
                ViewBag.Basarisiz = "Detaylar sayfası yüklenmedi !!!";
                return View();
            }

        }


        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AdminTable adminTable, HttpPostedFileBase KategoriFoto)
        {
            try
            {
                if (KategoriFoto != null)
                {
                    WebImage image = new WebImage(KategoriFoto.InputStream);
                    FileInfo imageInfo = new FileInfo(KategoriFoto.FileName);

                    string newfoto = Guid.NewGuid().ToString() + imageInfo.Extension;
                    image.Resize(800, 350);
                    image.Save("~/Upload/KategoriPhoto/" + newfoto);
                    adminTable.KategoriFoto = "/Upload/KategoriPhoto/" + newfoto;
                }
                db.AdminTable.Add(adminTable);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                ViewBag.Id = new SelectList(db.AdminTable, "Id", "KategoriAdi");
                ViewBag.Basarisiz = "Kategori Eklenemedi !!!";
                return View();
            }
        }


        public ActionResult Edit(int? id)
        {
            try
            {
                var Kategori = db.AdminTable.Where(m => m.Id == id).SingleOrDefault();

                if (Kategori == null)
                {
                    return HttpNotFound();
                }
                ViewBag.Id = new SelectList(db.AdminTable, "Id", "KategoriAdi", Kategori.Id);
                return View(Kategori);
            }
            catch
            {
                ViewBag.Basarisiz = "Güncelleme işlemi başarısız oldu !!";
                return View();
            }

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, HttpPostedFileBase kategoriFoto, AdminTable adminTable, AdminTable oldTable)
        {
            try
            {
                var EditKategori = db.AdminTable.Where(m => m.Id == id).SingleOrDefault();

                if (kategoriFoto != null)
                {
                    if (System.IO.File.Exists(Server.MapPath(EditKategori.KategoriFoto)))
                    {
                        System.IO.File.Delete(Server.MapPath(EditKategori.KategoriFoto));
                    }

                    WebImage image = new WebImage(kategoriFoto.InputStream);
                    FileInfo imageInfo = new FileInfo(kategoriFoto.FileName);

                    string newfoto = Guid.NewGuid().ToString() + imageInfo.Extension;
                    image.Resize(800, 350);
                    image.Save("~/Upload/KategoriFoto/" + newfoto);
                    EditKategori.KategoriFoto = "/Upload/KategoriFoto/" + newfoto;
                }
                EditKategori.KategoriAdi = oldTable.KategoriAdi;
                EditKategori.Dolar = oldTable.Dolar;
                EditKategori.Dinar = oldTable.Dinar;
                EditKategori.TransferMiktari = oldTable.TransferMiktari;
                EditKategori.Kargo = oldTable.Kargo;
                EditKategori.Nakliye = oldTable.Nakliye;
                EditKategori.Katki = oldTable.Katki;
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                ViewBag.CategoryId = new SelectList(db.AdminTable, "Id", "kategoriAdi", oldTable.Id);
                ViewBag.Basarisiz = "Güncelleme işlemi başarısız oldu !!";
                return View(oldTable);
            }
        }


        public ActionResult Delete(int id)
        {
            try
            {
                AdminTable adminTable = db.AdminTable.Find(id);
                db.AdminTable.Remove(adminTable);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                ViewBag.Basarisiz = "Silme işlemi başarısız oldu !!";
                return View();
            }
        }
    }
}
