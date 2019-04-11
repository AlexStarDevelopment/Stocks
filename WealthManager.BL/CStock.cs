using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WealthManager.PL;
using System.ComponentModel;
using System.Net;
using Newtonsoft.Json.Linq;

namespace WealthManager.BL
{
    public class CStock
    {
        #region Props
        public Guid Id { get; set; }
        public Guid UserId { get; }
        public string Ticker { get; set; }
        [DisplayName("Price")]
        public decimal Price { get; set; }
        public int TotalShares { get; set; }
        public decimal TotalPerTick { get; set; }
        #endregion

        public decimal GetTotalPerTick(string ticker)
        {
            decimal total = 0;
            try
            {
                CTransaction t = new CTransaction();
                total = t.GetTotal(ticker);
            }
            catch (Exception ex)
            {
                CErrorLog err = new CErrorLog();
                err.LogError(ex.Message);
                throw ex;
            }
            return total;
        }
        public decimal GetTotalShares(string ticker)
        {
            int total = 0;
            try
            {
                CTransaction t = new CTransaction();
                total = t.GetTotalShares(ticker);
            }
            catch (Exception ex)
            {
                CErrorLog err = new CErrorLog();
                err.LogError(ex.Message);
                throw ex;
            }
            return total;
        }

        #region API Call
        public decimal GetPrice(string tickers)
        {
            decimal price = 0;
            string json;

            try
            {
                //remove white space for api
                tickers = tickers.Replace(" ", "");

                using (var web = new WebClient())
                {
                    var url = $"https://api.iextrading.com/1.0/stock/{tickers}/quote";
                    json = web.DownloadString(url);
                }

                //json = json.Replace("\\", "");

                JObject v = JObject.Parse(json); 

                var ticker = v.SelectToken("symbol");
                price = (decimal)v.SelectToken("delayedPrice");

            }
            catch (Exception ex)
            {
                CErrorLog err = new CErrorLog();
                err.LogError(ex.Message);
                throw ex;
            }
            return price;
        } 
        #endregion

        #region Constructors
        // Default Constructor
        public CStock()
        {

        }

        // Custom Constructor
        public CStock(Guid id, string ticker)
        {
            Id = id;
            Price = GetPrice(ticker);
            Ticker = ticker;
        }

        // Custom Constructor
        public CStock(Guid id, Guid user, string ticker, decimal currentPricePerShare)
        {
            Id = id;
            UserId = user;
            Ticker = ticker;
            Price = GetPrice(ticker); //GetCurrentStockPrice(ticker);

        }

        public CStock(string ticker)
        {
            Ticker = ticker;
            Price = GetPrice(ticker);
        }
        #endregion

        #region CRUD Methods
        public void LoadById(Guid id)
        {
            try
            {
                WealthDataContext oDc = new WealthDataContext();
                tblStock stock = oDc.tblStocks.FirstOrDefault(p => p.Id == id);

                if (stock != null)
                {
                    this.Id = id;
                    this.Ticker = stock.Ticker;
                    this.Price = GetPrice(this.Ticker);
                    //this.UserId = stock.UserId;

                }
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

            try
            {
                var results = (from s in oDc.tblStocks
                               join u in oDc.tblUsers on s.UserId equals u.Id
                               where u.Id == CLogin.UserLoggedIn & s.Id == this.Id
                               select new
                               {
                                   s.Id,
                                   custId = s.UserId,
                                   s.Ticker,
                                   s.CurrentPricePerShare,
                               }).FirstOrDefault();



                if (results == null)
                {
                    tblStock stock = new tblStock();

                    stock.Id = Guid.NewGuid();
                    stock.UserId = CLogin.UserLoggedIn;
                    stock.Ticker = this.Ticker.ToUpper();
                    stock.CurrentPricePerShare = GetPrice(this.Ticker);
                    oDc.tblStocks.InsertOnSubmit(stock);
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

        public void Delete()
        {
            try
            {
                WealthDataContext oDc = new WealthDataContext();
                tblStock stock = oDc.tblStocks.FirstOrDefault(p => p.Id == this.Id);

                if (stock != null)
                {
                    oDc.tblStocks.DeleteOnSubmit(stock);
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

    public class CStockList : List<CStock>
    {
        #region API Call
        public decimal GetPrice(string tickers)
        {
            decimal price = 0;
            string json;

            //remove white space for api
            tickers = tickers.Replace(" ", "");

            try
            {


                using (var web = new WebClient())
                {
                    var url = $"https://api.iextrading.com/1.0/stock/{tickers}/quote";
                    json = web.DownloadString(url);
                }

                //json = json.Replace("\\", "");

                JObject v = JObject.Parse(json);

                var ticker = v.SelectToken("symbol");
                price = (decimal)v.SelectToken("delayedPrice");

            }
            catch (Exception ex)
            {
                CErrorLog err = new CErrorLog();
                err.LogError(ex.Message);
                throw ex;
            }
            return price;
        }
        #endregion

        public void Load()
        {
            try
            {
                //we will need to pass in the userID here to be able to limit the user to only see their list of stocks. see below *

                WealthDataContext oDc = new WealthDataContext();

                var results = (from s in oDc.tblStocks
                               join u in oDc.tblUsers on s.UserId equals u.Id
                               where u.Id == CLogin.UserLoggedIn
                               orderby s.Ticker
                               // * where s.userid == u.userId
                               select new
                               {
                                   s.Id,
                                   custId = s.UserId,
                                   s.Ticker,
                                   s.CurrentPricePerShare,
                               }).ToList();

                foreach (var stock in results)
                {
                    
                    CStock oStock = new CStock();
                    CTransaction t = new CTransaction();
                    oStock.TotalPerTick = t.GetTotal(stock.Ticker);
                    oStock.TotalShares = t.GetTotalShares(stock.Ticker);
                    oStock.Id = stock.Id;
                    oStock.Ticker = stock.Ticker;
                    oStock.Price = GetPrice(stock.Ticker);
                    Add(oStock);
                }

            }
            catch (Exception ex)
            {
                CErrorLog err = new CErrorLog();
                err.LogError(ex.Message);
                throw ex;
            } 
            
        }
        #endregion
        }
    }
