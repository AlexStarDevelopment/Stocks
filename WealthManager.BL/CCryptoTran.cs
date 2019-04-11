using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WealthManager.Exporter;
using WealthManager.PL;

namespace WealthManager.BL
{
    public class CCryptoTran
    {

        //TODO: implement email notifications for an inserted transaction

        public String Ticker { get; set; }
        public Guid Id { get; set; }
        public Guid StockId { get; set; }
        [DisplayName("Transaction Date")]
        [DataType(DataType.Date)]
        public DateTime TransDate { get; set; }
        public bool Buy { get; set; }
        public decimal Quantity { get; set; }
        [DisplayName("Price Per Share")]
        public double PricePerSharePaid { get; set; }


        public double GetTotal(string ticker)
        {
            try
            {
                CCrypto stock = new CCrypto();
                decimal totalQuan = 0;
                double tickerprice = 0;

                WealthDataContext oDc = new WealthDataContext();

                var results = (from u in oDc.tblUsers
                               join s in oDc.tblCryptos on u.Id equals s.UserId
                               join t in oDc.tblCryptoTrans on s.Id equals t.CryptoId
                               where s.Symbol == ticker && s.UserId == CLogin.UserLoggedIn
                               select new
                               {
                                   quan = t.Quantity,
                                   buy = t.Buy
                               }).ToList();
                foreach (var quan in results)
                {

                    if (quan.buy == true)
                    {
                        this.Quantity = (decimal)quan.quan;
                        totalQuan = Quantity += totalQuan;
                    }
                    else
                    {
                        this.Quantity = (decimal)quan.quan;
                        totalQuan = totalQuan -= Quantity;
                    }
                }

                tickerprice = stock.GetPrice(ticker);
                double.TryParse(totalQuan.ToString(), out double g);

                double temp;
                temp = g * tickerprice;
                temp = Math.Round(temp, 2);
                return temp;
            }
            catch (Exception ex)
            {
                CErrorLog err = new CErrorLog();
                err.LogError(ex.Message);
                throw ex;
            }

        }

        public decimal GetTotalShares(string ticker)
        {
            try
            {
                CCrypto stock = new CCrypto();
                decimal totalQuan = 0;

                WealthDataContext oDc = new WealthDataContext();

                var results = (from u in oDc.tblUsers
                               join s in oDc.tblCryptos on u.Id equals s.UserId
                               join t in oDc.tblCryptoTrans on s.Id equals t.CryptoId
                               where s.Symbol == ticker && s.UserId == CLogin.UserLoggedIn
                               select new
                               {
                                   quan = t.Quantity,
                                   buy = t.Buy
                               }).ToList();
                foreach (var quan in results)
                {
                    if (quan.buy == true)
                    {
                        this.Quantity = (decimal)quan.quan;
                        totalQuan = Quantity += totalQuan;
                    }
                    else
                    {
                        this.Quantity = (decimal)quan.quan;
                        totalQuan = totalQuan -= Quantity;
                    }
                }

                return totalQuan;
            }
            catch (Exception ex)
            {
                CErrorLog err = new CErrorLog();
                err.LogError(ex.Message);
                throw ex;
            }

        }

        public Guid GetGuid(string stocktick)
        {
            Guid newguid;

            WealthDataContext oDc = new WealthDataContext();


            var result = (from s in oDc.tblCryptos
                          where s.Symbol == stocktick
                          select new
                          {
                              sid = s.Id
                          }).FirstOrDefault();

            newguid = result.sid;


            return newguid;
        }

        // Default Constructor
        public CCryptoTran()
        {

        }

        public CCryptoTran(Guid id)
        {
            Id = id;
        }

        // Custom Constructor
        public CCryptoTran(Guid sid, DateTime transactionDate, bool buy, int quantity, double pricePerShare)
        {
            StockId = sid;
            TransDate = transactionDate;
            Buy = buy;
            Quantity = quantity;
            PricePerSharePaid = pricePerShare;
        }

        // Custom Constructor
        public CCryptoTran(Guid id, Guid sid, DateTime transactionDate, bool buy, int quantity, double pricePerShare)
        {
            Id = id;
            StockId = sid;
            TransDate = transactionDate;
            Buy = buy;
            Quantity = quantity;
            PricePerSharePaid = pricePerShare;
        }

        public void LoadById(Guid id)
        {
            try
            {
                WealthDataContext oDc = new WealthDataContext();

                var result = (from t in oDc.tblTransactions
                              join s in oDc.tblStocks on t.StockId equals s.Id
                              where t.Id == id
                              orderby t.TransDate
                              select new
                              {
                                  t.Id,
                                  quan = t.Quantity,
                                  tdate = t.TransDate,
                                  tbuy = t.Buy,
                                  tpps = t.PricePerSharePaid,
                              }).FirstOrDefault();

                this.Id = result.Id;
                this.Quantity = (int)result.quan;
                this.TransDate = (DateTime)result.tdate;
                this.Buy = (bool)result.tbuy;
                this.PricePerSharePaid = (double)result.tpps;
            }
            catch (Exception ex)
            {
                CErrorLog err = new CErrorLog();
                err.LogError(ex.Message);
                throw ex;
            }

        }



        public void Insert()
        {
            WealthDataContext oDc = new WealthDataContext();
            tblCryptoTran transaction = new tblCryptoTran();

            var result = (from s in oDc.tblCryptos
                          join u in oDc.tblUsers on s.UserId equals u.Id
                          where u.Id == CLogin.UserLoggedIn & s.Id == this.StockId
                          select new
                          {
                              oldid = s.Id
                          }).FirstOrDefault();

            transaction.Id = Guid.NewGuid();
            transaction.CryptoId = result.oldid;
            transaction.TransDate = this.TransDate;
            transaction.Buy = this.Buy;
            transaction.Quantity = this.Quantity;

            decimal g;
            decimal.TryParse(this.PricePerSharePaid.ToString(), out g);

            transaction.Price = g;

            oDc.tblCryptoTrans.InsertOnSubmit(transaction);
            oDc.SubmitChanges();
        }

        //public void Update()
        //{
        //    try
        //    {
        //        WealthDataContext oDc = new WealthDataContext();

        //        tblCryptoTran transaction = oDc.tblCryptoTrans.FirstOrDefault(p => p.Id == this.Id);

        //        if (transaction != null)
        //        {
        //            transaction.TransDate = this.TransDate;
        //            transaction.Buy = this.Buy;
        //            transaction.CryptoId = this.StockId;
        //            transaction.Quantity = this.Quantity;
        //            decimal g;
        //            decimal.TryParse(this.PricePerSharePaid.ToString(), out g);

        //            transaction.Price = g;
        //            oDc.SubmitChanges();
        //        }
        //        else
        //        {
        //            throw new Exception("Row not found. (Id : " + this.Id + ")");
        //        }


        //    }
        //    catch (Exception ex)
        //    {
        //        CErrorLog err = new CErrorLog();
        //        err.LogError(ex.Message);
        //        throw ex;
        //    }
        //}

        public void Delete()
        {
            try
            {
                WealthDataContext oDc = new WealthDataContext();
                tblTransaction transaction = oDc.tblTransactions.FirstOrDefault(p => p.Id == this.Id);

                if (transaction != null)
                {
                    oDc.tblTransactions.DeleteOnSubmit(transaction);
                    oDc.SubmitChanges();
                }
            }
            catch (Exception ex)
            {
                CErrorLog err = new CErrorLog();
                err.LogError(ex.Message);
                throw ex;
            }
        }
    }

    public class CCryptoTranList : List<CCryptoTran>
    {
        public void Export()
        {
            try
            {
                int iCnt = 0;

                Load();
                //TODO: figure out why this.count is = to 0. it should be = to 2

                string[,] transactions = new string[this.Count + 1, 6];

                transactions[0, 0] = "Ticker";
                transactions[0, 1] = "Transaction Date";
                transactions[0, 2] = "Quantity";
                transactions[0, 3] = "Price";
                transactions[0, 4] = "Buy";

                while (iCnt < this.Count)
                {
                    iCnt++;
                    CCryptoTran v = this[iCnt - 1];
                    transactions[iCnt, 0] = v.Ticker.ToString();
                    transactions[iCnt, 1] = v.TransDate.Date.ToString();
                    transactions[iCnt, 2] = v.Quantity.ToString();
                    transactions[iCnt, 3] = v.PricePerSharePaid.ToString();
                    transactions[iCnt, 4] = v.Buy.ToString();

                }

                CExcel.Export("Transaction.xlsx", transactions);
            }
            catch (Exception ex)
            {
                CErrorLog err = new CErrorLog();
                err.LogError(ex.Message);
                throw ex;
            }
        }

        public void Load()
        {
            try
            {
                WealthDataContext oDc = new WealthDataContext();


                var result = (from t in oDc.tblCryptoTrans
                              join s in oDc.tblCryptos on t.CryptoId equals s.Id
                              join u in oDc.tblUsers on s.UserId equals u.Id
                              where u.Id == CLogin.UserLoggedIn
                              orderby t.TransDate
                              select new
                              {
                                  t.Id,
                                  t.CryptoId,
                                  quan = t.Quantity,
                                  tdate = t.TransDate,
                                  tbuy = t.Buy,
                                  tpps = t.Price,
                                  tick = s.Symbol
                              }).ToList();

                foreach (var tran in result)
                {
                    CCryptoTran oTran = new CCryptoTran();

                    oTran.Ticker = tran.tick;
                    oTran.Id = tran.Id;
                    oTran.StockId = (Guid)tran.CryptoId;
                    oTran.Quantity = (int)tran.quan;
                    oTran.TransDate = (DateTime)tran.tdate;
                    oTran.Buy = (bool)tran.tbuy;
                    oTran.PricePerSharePaid = (double)tran.tpps;
                    Add(oTran);
                }



            }
            catch (Exception ex)
            {
                CErrorLog err = new CErrorLog();
                err.LogError(ex.Message);
                throw ex;
            }
        }
    }

}
