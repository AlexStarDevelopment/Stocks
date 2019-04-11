using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WealthManager.BL;

namespace WealthManager.ASP.NET.Models
{
    public class TransactionStock
    {
        public CTransaction NewTransaction { get; set; }
        public CStockList NewStock { get; set; }
    }
}