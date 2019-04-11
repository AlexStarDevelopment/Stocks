using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WealthManager.PL;
using System.Collections.Generic;
using System.Linq;

namespace WealthManager.PL.Test
{
    [TestClass]
    public class utStocks
    {
        [TestMethod]
        public void LoadTest()
        {
            WealthDataContext wdc = new WealthDataContext();

            List<tblStock> stocks = (from u in wdc.tblStocks select u).ToList();

            Assert.AreNotEqual(0, stocks.Count);
        }
        [TestMethod]
        public void InsertTest()
        {
            WealthDataContext wdc = new WealthDataContext();

            tblStock otblstock = new tblStock();

            otblstock.Id = Guid.NewGuid();
            otblstock.UserId = Guid.NewGuid();
            otblstock.Ticker = "VTI";
            

            wdc.tblStocks.InsertOnSubmit(otblstock);
            wdc.SubmitChanges();

            tblStock stocks = (from u in wdc.tblStocks where u.Ticker == "VTI" select u).FirstOrDefault();

            Assert.IsNotNull(stocks);
        }
        [TestMethod]
        public void UpdateTest()
        {
            WealthDataContext wdc = new WealthDataContext();

            tblStock stocks = (from u in wdc.tblStocks where u.Ticker == "VTI" select u).FirstOrDefault();

            stocks.Ticker = "VEU";

            wdc.SubmitChanges();

            tblStock stockUpdate = (from u in wdc.tblStocks where u.Ticker == "VEU" select u).FirstOrDefault();

            Assert.IsNotNull(stockUpdate);
        }
        [TestMethod]
        public void DeleteTest()
        {
            WealthDataContext wdc = new WealthDataContext();

            tblStock stocks = (from u in wdc.tblStocks where u.Ticker == "VEU" select u).FirstOrDefault();

            wdc.tblStocks.DeleteOnSubmit(stocks);
            wdc.SubmitChanges();

            tblStock stockDelete = (from u in wdc.tblStocks where u.Ticker == "VEU" select u).FirstOrDefault();

            Assert.IsNull(stockDelete);
        }
    }
}
