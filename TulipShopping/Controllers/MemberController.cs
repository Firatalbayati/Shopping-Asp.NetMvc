using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TulipShopping.Models;

namespace TulipShopping.Controllers
{
    public class MemberController : Controller
    {
        private TulipShoppingModel db = new TulipShoppingModel();



        //GET: Member
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Member member)
        {
            try
            {
                var check = db.Member.FirstOrDefault(x => x.UserName == member.UserName);

                if (check.UserName == member.UserName && check.Password == member.Password)
                {
                    Session["memberid"] = check.Id;
                    Session["username"] = check.UserName;
                    Session["authorityid"] = check.AuthorityId;
                    return RedirectToAction("Index", "Admin");
                }
                else
                {
                    ViewBag.HataErorr = "Kullanıcı adı veya şifre yanlış";
                    return View();
                }
            }
            catch (Exception ex)
            {
                ViewBag.ReError = ex.Message;
                return View();
            }

        }

        public ActionResult Logout()
        {
            try
            {
                Session["memberid"] = null;
                Session.Abandon();
                return RedirectToAction("Index", "Home");
            }
            catch
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }
    }
}