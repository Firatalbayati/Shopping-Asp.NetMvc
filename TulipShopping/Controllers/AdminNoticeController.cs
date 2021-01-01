using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TulipShopping.Models;

namespace TulipShopping.Controllers
{
    public class AdminNoticeController : Controller
    {

        private TulipShoppingModel db = new TulipShoppingModel();



        public ActionResult Index()
        {
            try
            {
                return View(db.Notice.OrderByDescending(K => K.Id).ToList());
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
                Notice notice = db.Notice.Find(id);
                if (notice == null)
                {
                    return HttpNotFound();
                }
                return View(notice);
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
        public ActionResult Create(Notice notice)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Notice.Add(notice);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(notice);
            }
            catch
            {
                ViewBag.Id = new SelectList(db.AdminTable, "Id", "KategoriAdi");
                ViewBag.Basarisiz = "Not Eklenemedi !!!";
                return View();
            }

        }


        public ActionResult Edit(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Notice notice = db.Notice.Find(id);
                if (notice == null)
                {
                    return HttpNotFound();
                }
                return View(notice);
            }
            catch
            {
                ViewBag.Basarisiz = "Not güncelleme işlemi başarısız oldu !!";
                return View();
            }

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id , Notice notice , Notice oldTable)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(notice).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(notice);
            }
            catch
            {
                ViewBag.Id = new SelectList(db.AdminTable, "Id", "Title", oldTable.Id);
                ViewBag.Basarisiz = "Not güncelleme işlemi başarısız oldu !!";
                return View(oldTable);
            }



        }


        public ActionResult Delete(int id)
        {
            try
            {
                Notice notice = db.Notice.Find(id);
                db.Notice.Remove(notice);
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
