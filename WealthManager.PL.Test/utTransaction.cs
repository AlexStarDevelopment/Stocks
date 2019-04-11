using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WealthManager.PL;
using System.Collections.Generic;
using System.Linq;

namespace WealthManager.PL.Test
{
    [TestClass]
    public class utTransaction
    {
        [TestMethod]
        public void LoadTest()
        {
            WealthDataContext wdc = new WealthDataContext();

            List<tblTransaction> trans = (from u in wdc.tblTransactions select u).ToList();

            Assert.AreNotEqual(0, trans.Count);
        }
        [TestMethod]
        public void InsertTest()
        {
            WealthDataContext wdc = new WealthDataContext();

            tblTransaction otblTrans = new tblTransaction();

            otblTrans.Id = Guid.NewGuid();
            otblTrans.TransDate = DateTime.Now;
            otblTrans.Buy = true;
            otblTrans.Quantity = 100;
            otblTrans.PricePerSharePaid = 336;
            otblTrans.StockId = Guid.NewGuid();

            wdc.tblTransactions.InsertOnSubmit(otblTrans);
            wdc.SubmitChanges();

            tblTransaction trans = (from u in wdc.tblTransactions where u.PricePerSharePaid == 336 select u).FirstOrDefault();

            Assert.IsNotNull(trans);
        }
        [TestMethod]
        public void UpdateTest()
        {
            WealthDataContext wdc = new WealthDataContext();

            tblTransaction trans = (from u in wdc.tblTransactions where u.PricePerSharePaid == 336 select u).FirstOrDefault();

            trans.Quantity = 50;

            wdc.SubmitChanges();

            tblTransaction transUpdate = (from u in wdc.tblTransactions where u.Quantity == 50 select u).FirstOrDefault();

            Assert.IsNotNull(transUpdate);
        }
        [TestMethod]
        public void DeleteTest()
        {
            WealthDataContext wdc = new WealthDataContext();

            tblTransaction trans = (from u in wdc.tblTransactions where u.Quantity == 50 select u).FirstOrDefault();

            wdc.tblTransactions.DeleteOnSubmit(trans);
            wdc.SubmitChanges();

            tblTransaction transDelete = (from u in wdc.tblTransactions where u.Quantity == 50 select u).FirstOrDefault();

            Assert.IsNull(transDelete);
        }
    }
}
