using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using WealthManager.BL;

namespace WealthManager.SL
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Tran" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Tran.svc or Tran.svc.cs at the Solution Explorer and start debugging.
    public class Tran : ITran
    {
        public void DeleteTransaction(Guid id)
        {
            CTransaction transaction = new CTransaction();
            transaction.Id = id;
            transaction.Delete();
        }

        public void DeleteTransactionObj(CTransaction transaction)
        {
            transaction.Delete();
        }

        public CTransactionList GetTransactions()
        {
            CTransactionList transactionList = new CTransactionList();
            transactionList.Load();
            return transactionList;
        }

        public void InsertTransaction(Guid Id, Guid UserId, DateTime transactionDate, bool isBuy, int quantity, decimal pricePerShare)
        {
            //new CTransaction(UserId, transactionDate, isBuy, quantity, pricePerShare).Insert();
        }

        public void InsertTransaction(string description)
        {
            throw new NotImplementedException();
        }

        public void InsertTransactionObj(CTransaction transaction)
        {
            //transaction.Insert();
        }

        public void UpdateTransaction(Guid UserId, DateTime transactionDate, bool isBuy, int quantity, decimal pricePerShare)
        {
            new CTransaction(UserId, transactionDate, isBuy, quantity, pricePerShare).Update();
        }

        public void UpdateTransaction(Guid id, string description)
        {
            throw new NotImplementedException();
        }

        public void UpdateTransactionObj(CTransaction transaction)
        {
            transaction.Update();
        }

        public void Commit(int fRetaining, int grfTC, int grfRM)
        {
            throw new NotImplementedException();
        }

        public void Abort(ref BOID pboidReason, int fRetaining, int fAsync)
        {
            throw new NotImplementedException();
        }

        public void GetTransactionInfo(out XACTTRANSINFO pinfo)
        {
            throw new NotImplementedException();
        }

        public void DoWork()
        {
            throw new NotImplementedException();
        }
    }
}
