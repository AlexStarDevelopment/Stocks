using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using WealthManager.BL;

namespace WealthManager.SL
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ITran" in both code and config file together.
    [ServiceContract]
    public interface ITran
    {
        [OperationContract]
        CTransactionList GetTransactions();

        [OperationContract]
        void InsertTransaction(Guid Id, Guid UserId, DateTime transactionDate, bool isBuy, int quantity, decimal pricePerShare);

        [OperationContract]
        void InsertTransactionObj(CTransaction Transaction);

        [OperationContract]
        void UpdateTransaction(Guid UserId, DateTime transactionDate, bool isBuy, int quantity, decimal pricePerShare);

        [OperationContract]
        void UpdateTransactionObj(CTransaction Transaction);

        [OperationContract]
        void DeleteTransaction(Guid id);

        [OperationContract]
        void DeleteTransactionObj(CTransaction Transaction);
    }
}
