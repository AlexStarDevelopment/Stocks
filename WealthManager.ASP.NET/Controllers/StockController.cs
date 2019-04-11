using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WealthManager.BL;

namespace WealthManager.ASP.NET.Controllers
{
    public class StockController : Controller
    {
        CStockList stocks;
        // GET: Stock
        public ActionResult Index()
        {
            if(Session["user"] != null)
            {
                stocks = new CStockList();
                stocks.Load();
                return View(stocks);
            }
            return RedirectToAction("Login", "Login");
        }

        // GET: Stock/Create
        public ActionResult Create()
        {
            CStock c = new CStock();
            return View(c);
        }

        // POST: Stock/Create
        [HttpPost]
        public ActionResult Create(CStock collection)
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

        // GET: Stock/Delete/5
        public ActionResult Delete(Guid id)
        {
            CStock stock = new CStock();
            stock.LoadById(id);
            return View(stock);
        }

        // POST: Stock/Delete/5
        [HttpPost]
        public ActionResult Delete(Guid id, CStock collection)
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
