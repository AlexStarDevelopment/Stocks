using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WealthManager.BL;

namespace WealthManager.BL.Test
{
    [TestClass]
    public class utStock
    {
        [TestMethod]
        public void LoadStocksTest()
        {
            CStockList stocks = new CStockList();
            stocks.Load();
            Assert.AreEqual(1, stocks.Count);
        }

        [TestMethod]
        public void InsertStocksTest()
        {
            CStock stock = new CStock("GOOG");
            stock.Insert();

            CStock newStock = new CStock();
            newStock.LoadById(stock.Id);

            Assert.AreEqual(stock.Ticker, newStock.Ticker);
        }
    }
}
