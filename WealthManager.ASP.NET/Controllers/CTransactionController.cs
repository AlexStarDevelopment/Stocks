using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WealthManager.BL;
using WealthManager.ASP.NET.Models;

namespace WealthManager.ASP.NET.Controllers
{
    public class CTransactionController : Controller
    {
        CTransactionList trans;
        // GET: CTransaction
        public ActionResult Index()
        {
            if (Session["user"] != null)
            {
                trans = new CTransactionList();
                trans.Load();
                return View(trans);
            }
            return RedirectToAction("Login", "Login");

        }

        // GET: CTransaction/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CTransaction/Create
        public ActionResult Create()
        {
            if (Session["user"] != null)
            {
            CStockList sl = new CStockList();
            sl.Load();

            TransactionStock ts = new TransactionStock();
            ts.NewStock = sl;
            ts.NewTransaction = new CTransaction();
            
            return View(ts);
            }
            return RedirectToAction("Login", "Login");

        }

        // POST: CTransaction/Create
        [HttpPost]
        public ActionResult Create(TransactionStock collection)
        {
            try
            {
                // TODO: Add insert logic here

                collection.NewTransaction.Insert();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: CTransaction/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CTransaction/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: CTransaction/Delete/5
        public ActionResult Delete(Guid id)
        {
            if (Session["user"] != null)
            {
            CTransaction t = new CTransaction();
            t.LoadById(id);
            return View(t);
            }
            return RedirectToAction("Login", "Login");

        }

        // POST: CTransaction/Delete/5
        [HttpPost]
        public ActionResult Delete(Guid id, CTransaction collection)
        {
            try
            {
                // TODO: Add delete logic here
                collection.Delete();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
