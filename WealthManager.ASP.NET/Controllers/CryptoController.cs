using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WealthManager.BL;

namespace WealthManager.ASP.NET.Controllers
{
    public class CryptoController : Controller
    {
        CCryptoList cryptos;
        // GET: Crypto
        public ActionResult Index()
        {
            if (Session["user"] != null)
            {
                cryptos = new CCryptoList();
                cryptos.Load();
                return View(cryptos); 
            }
            return RedirectToAction("Login", "Login");
        }

        // GET: Crypto/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Crypto/Create
        public ActionResult Create()
        {
            if (Session["user"] != null)
            {
            CCrypto crypto = new CCrypto();
            return View(crypto);
            }
            return RedirectToAction("Login", "Login");

        }

        // POST: Crypto/Create
        [HttpPost]
        public ActionResult Create(CCrypto collection)
        {
            try
            {
                // TODO: Add insert logic here
                collection.Insert();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Crypto/Edit/5
        public ActionResult Edit(Guid id)
        {
            if (Session["user"] != null)
            {
            CCrypto c = new CCrypto();
            c.LoadById(id);
            return View();
            }
            return RedirectToAction("Login", "Login");

        }

        // POST: Crypto/Edit/5
        [HttpPost]
        public ActionResult Edit(Guid id, CCrypto collection)
        {
            try
            {
                // TODO: Add update logic here
                collection.Delete();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Crypto/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Crypto/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
