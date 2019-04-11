using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WealthManager.BL;

namespace WealthManager.ASP.NET.Controllers
{
    public class LoginController : Controller
    {

        public ActionResult Seed()
        {
            //new CUser.Seed(); //probably missing a using statement
            return View();
        }
        public ActionResult Logout()
        {
            Session["user"] = null;
            return View();
        }
        public ActionResult Login(string returnurl)
        {
            returnurl = ViewBag.ReturnURL;
            return View();
        }
        [HttpPost]
        public ActionResult Login(CUser user, string returnurl)
        {
            try
            {
                returnurl = ViewBag.ReturnURL;
                if (user.Login())
                {
                    Session["user"] = user;
                    return RedirectToAction("index", "stock");
                }
                else
                {
                    ViewBag.Message = "Sorry, wrong credentials.";
                    return RedirectToAction("Login", "Login");
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View();
            }
        }
    }
}
