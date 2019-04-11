using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WealthManager.BL;

namespace WealthManager.ASP.NET.Models
{
    public class TransactionCrypto
    {
        public CCryptoTran NewTransaction { get; set; }
        public CCryptoList NewCrypto { get; set; }
    }
}