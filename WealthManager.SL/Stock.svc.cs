using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using WealthManager.BL;

namespace WealthManager.SL
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Stock" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Stock.svc or Stock.svc.cs at the Solution Explorer and start debugging.
    public class Stock : IStock
    {
        public void DeleteStock(Guid id)
        {
            CStock stock = new CStock {Id = id};
            stock.Delete();
        }

        public void DeleteStockObj(CStock stock)
        {
            stock.Delete();
        }

        public CStockList GetStocks()
        {
            CStockList stockList = new CStockList();
            stockList.Load();
            return stockList;
        }

        public void InsertStock(string description)
        {
            new CStock(description).Insert();
        }

        public void InsertStockObj(CStock stock)
        {
            stock.Insert();
        }
    }
}
