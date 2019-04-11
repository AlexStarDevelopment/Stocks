using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WealthManager.BL.Test
{
    [TestClass]
    public class utTransaction
    {
        [TestMethod]
        public void LoadTransactionTest()
        {
            CTransactionList transactions = new CTransactionList();
            transactions.Load();
            Assert.AreEqual(1, transactions.Count);
        }

        [TestMethod]
        public void InsertTransactionTest()
        {
            CTransaction transaction = new CTransaction(Guid.NewGuid(), DateTime.Parse("2014-06-10"), true, 100, 175);
            transaction.Insert();

            CTransaction newTransaction = new CTransaction();
            newTransaction.LoadById(transaction.Id);

            Assert.AreEqual(transaction.TransDate, newTransaction.TransDate);

        }
    }
}
