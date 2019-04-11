using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using WealthManager.BL;

namespace WealthManager.SL
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IStock" in both code and config file together.
    [ServiceContract]
    public interface IStock
    {
            [OperationContract]
            CStockList GetStocks();

            [OperationContract]
            void InsertStock(string description);

            [OperationContract]
            void InsertStockObj(CStock Stock);

            [OperationContract]
            void DeleteStock(Guid id);

            [OperationContract]
            void DeleteStockObj(CStock Stock);
        }
}
