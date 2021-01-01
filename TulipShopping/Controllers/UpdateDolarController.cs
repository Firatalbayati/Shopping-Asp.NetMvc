using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TulipShopping.Models;

namespace TulipShopping.Controllers
{
    public class UpdateDolarController : Controller
    {

        private TulipShoppingModel db = new TulipShoppingModel();


        // GET: UpdateDolar
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(decimal? Dolar)
        {
            try
            {
                if (Dolar != null)
                {
                    var p1 = new SqlParameter("@Dolar", Dolar);
                    var Dolars = db.Database.ExecuteSqlCommand("UpdateDolar @Dolar", p1);
                    db.SaveChanges();
                    ViewBag.Basarili = "Güncelleme işlemi başarılı";

                }
                return View();
            }
            catch
            {
                ViewBag.Basarisiz = "Hata Dolar güncellenmedi !!!";
                return View();
            }
        }
    }
}