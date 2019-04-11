using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WealthManager.BL;

namespace WealthManager.ASP.Charts.Controllers
{
    public class LoginController : Controller
    {
        public ActionResult Logout()
        {
            Session["user"] = null;
            return View();
        }
        public ActionResult Login(string returnurl)
        {
            ViewBag.ReturnURL = returnurl;
            return View();
        }
        [HttpPost]
        public ActionResult Login(CUser user, string returnurl)
        {
            try
            {
                ViewBag.ReturnUrl = returnurl;
                if (user.Login())
                {
                    Session["user"] = user;
                    return Redirect(returnurl);
                }
                else
                {
                    ViewBag.Message = "Sorry, wrong credentials.";
                    return Redirect(returnurl);
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
