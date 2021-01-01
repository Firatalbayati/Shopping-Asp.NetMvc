using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TulipShopping.Models;

namespace TulipShopping.Controllers
{
    public class HomeController : Controller
    {


        private TulipShoppingModel db = new TulipShoppingModel();



        public ActionResult Index(int? id)
        {
            try
            {
                return View(db.AdminTable.Where(m => m.Id == id).ToList());
            }
            catch
            {
                ViewBag.Basarisiz = "حدث خطأ أثناء تحميل الصفحة الرئيسية !!!";
                return View();
            }
        }


        [HttpPost]
        public ActionResult Index(int? id, decimal tl)
        {
            try
            {
                if (tl == 0)
                {
                    ViewBag.Message = "يجب أن تكون القيمة المدخلة أكبر من الصفر";
                }
                else
                {
                    if (id != null && tl != null)
                    {
                        var Hesaplama = db.AdminTable.Where(x => x.Id == id)
                                .Select(n => new
                                {
                                    kategoriAdi = n.KategoriAdi,
                                    kategoriFoto = n.KategoriFoto,
                                    dolar = n.Dolar,
                                    dinar = n.Dinar,
                                    transferMiktari = n.TransferMiktari,
                                    kargo = n.Kargo,
                                    nakliye = n.Nakliye,
                                    katki = n.Katki
                                }).ToList();

                        foreach (var h in Hesaplama)
                        {
                            string tegoriFoto = h.kategoriFoto;
                            string kategoriAdi = h.kategoriAdi;
                            decimal dolar = h.dolar;
                            decimal dinar = h.dinar;
                            decimal transferMiktari = h.transferMiktari;
                            decimal kargo = h.kargo;
                            decimal nakliye = h.nakliye;
                            decimal katki = h.katki;

                            decimal TlDolar = tl / dolar;

                            decimal DolarDinar = TlDolar * dinar;

                            decimal Toplam = DolarDinar + kargo + nakliye + katki;

                            Toplam = Math.Round(Toplam, 1);

                            string sonuc = Toplam.ToString();

                            ViewBag.SonucMesage = sonuc + "00" + " دع ";

                        }
                        return View(db.AdminTable.Where(m => m.Id == id).ToList());
                    }
                    else
                    {
                        ViewBag.Message = "الرجاء قم بتحديد الفئة !! ";
                    }
                }
                return View();
            }
            catch
            {
                ViewBag.Message = "حدث خطأ أثناء عملية حسابية !!!";
                return View();
            }
        }

        public ActionResult CategoryPartial()
        {
            try
            {
                return View(db.AdminTable.OrderByDescending(K => K.Id).ToList());
            }
            catch
            {
                ViewBag.Basarisiz = "حدث خطأ أثناء تحميل الفئات !!!";
                return View();
            }

        }


        public ActionResult NoticePartial()
        {
            try
            {
                return View(db.Notice.ToList());
            }
            catch
            {
                ViewBag.Basarisiz = "حدث خطأ أثناء تحميل الملاحظات !!!";
                return View();
            }

        }


        public ActionResult Notice()
        {
            try
            {
                return View(db.Notice.ToList());
            }
            catch
            {
                ViewBag.Basarisiz = "حدث خطأ أثناء تحميل الملاحظات !!!";
                return View();
            }
        }
    }
}