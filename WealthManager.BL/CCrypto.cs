using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WealthManager.PL;

namespace WealthManager.BL
{
    public class CCrypto
    {
        #region Props
        public Guid Id { get; set; }
        public Guid UserId { get; }
        public string Ticker { get; set; }
        [DisplayName("Price")]
        public double Price { get; set; }
        public decimal TotalShares { get; set; }
        [DisplayName("Total")]
        public double TotalPerTick { get; set; }
        #endregion

        public double GetTotalPerTick(string symbol)
        {
            double total = 0;
            try
            {
                CCryptoTran t = new CCryptoTran();
                total = t.GetTotal(symbol);
            }
            catch (Exception ex)
            {
                CErrorLog err = new CErrorLog();
                err.LogError(ex.Message);
                throw ex;
            }
            return total;
        }
        public decimal GetTotalShares(string symbol)
        {
            decimal total = 0;
            try
            {
                CCryptoTran t = new CCryptoTran();
                total = t.GetTotalShares(symbol);
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
        public double GetPrice(string symbols)
        {
            double price = 0;
            string json;

            //remove white space for api
            symbols = symbols.Replace(" ", "");

            try
            {
                using (var web = new WebClient())
                {
                    var url = $"https://api.coinmarketcap.com/v1/ticker/{symbols}/";
                    json = web.DownloadString(url);
                }

                //json = json.Replace("\\", "");

                JArray v = JArray.Parse(json);

                foreach (var a in v)
                {
                    var symbol = a.SelectToken("name");
                    price = (double)a.SelectToken("price_usd");
                    //price = Math.Round(price, 2);
                }
            }
            catch (Exception ex)
            {
                CErrorLog err = new CErrorLog();
                err.LogError(ex.Message);
                throw ex;
            }
            price = Math.Round(price, 2);
            return price;
        }
        #endregion

        #region Constructors
        // Default Constructor
        public CCrypto()
        {

        }

        // Custom Constructor
        public CCrypto(Guid id, string symbol)
        {
            Id = id;
            Price = GetPrice(symbol);
            Ticker = symbol;
        }

        // Custom Constructor
        public CCrypto(Guid id, Guid user, string symbol, double currentPricePerShare)
        {
            Id = id;
            UserId = user;
            Ticker = symbol;
            Price = GetPrice(symbol); //GetCurrentStockPrice(symbol);

        }

        public CCrypto(string symbol)
        {
            Ticker = symbol;
            Price = GetPrice(symbol);
        }
        #endregion

        #region CRUD Methods
        public void LoadById(Guid id)
        {
            WealthDataContext oDc = new WealthDataContext();
            tblCrypto stock = oDc.tblCryptos.FirstOrDefault(p => p.Id == Id);

            if (stock != null) this.Id = stock.Id;
            this.Price = GetPrice(this.Ticker);
            if (stock != null) this.Ticker = stock.Symbol;
        }

        public void Insert()
        {
            try
            {
                WealthDataContext oDc = new WealthDataContext();

                var results = (from s in oDc.tblCryptos
                               join u in oDc.tblUsers on s.UserId equals u.Id
                               where u.Id == CLogin.UserLoggedIn & s.Id == this.Id

                               select new
                               {
                                   s.Id,
                                   custId = s.UserId,
                                   s.Symbol,
                                   s.CurrentPrice,
                               }).ToList();

                if(results != null)
                {
                    //WealthDataContext oDc = new WealthDataContext();

                    tblCrypto stock = new tblCrypto();

                    stock.Id = Guid.NewGuid();
                    stock.UserId = CLogin.UserLoggedIn;
                    stock.Symbol = this.Ticker;

                    decimal.TryParse(GetPrice(this.Ticker).ToString(), out decimal g);
                    stock.CurrentPrice = g;


                    oDc.tblCryptos.InsertOnSubmit(stock);
                    oDc.SubmitChanges();
                }
                else
                {
                }
            }
            catch (Exception ex)
            {
                CErrorLog err = new CErrorLog();
                err.LogError(ex.Message);
                throw ex;
            }
        }

        public void Update()
        {
            try
            {
                WealthDataContext oDc = new WealthDataContext();

                tblCrypto stock = oDc.tblCryptos.FirstOrDefault(p => p.Id == this.Id);

                if (stock != null)
                {
                    stock.Symbol = this.Ticker;
                    decimal.TryParse(GetPrice(this.Ticker).ToString(), out decimal g);
                    stock.CurrentPrice = g;

                    stock.UserId = this.UserId;
                    oDc.SubmitChanges();
                }
                else
                {
                    throw new Exception("Row not found. (Id : " + this.Id + ")");
                }

            }
            catch (Exception ex)
            {
                CErrorLog err = new CErrorLog();
                err.LogError(ex.Message);
                throw ex;
            }
        }

        public void LoadById()
        {
            try
            {
                WealthDataContext oDc = new WealthDataContext();
                tblCrypto stock = oDc.tblCryptos.FirstOrDefault(p => p.Id == this.Id);

                if (stock != null)
                {
                    this.Id = stock.Id;
                    this.Ticker = stock.Symbol;
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

        public void Delete()
        {
            try
            {
                WealthDataContext oDc = new WealthDataContext();
                tblCrypto stock = oDc.tblCryptos.FirstOrDefault(p => p.Id == this.Id);

                if (stock != null)
                {
                    oDc.tblCryptos.DeleteOnSubmit(stock);
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

    public class CCryptoList : List<CCrypto>
    {
        #region API Call
        public double GetPrice(string symbols)
        {
            double price = 0;
            string json;

            //remove white space for api
            symbols = symbols.Replace(" ", "");

            try
            {


                using (var web = new WebClient())
                {
                    var url = $"https://api.coinmarketcap.com/v1/ticker/{symbols}/";
                    json = web.DownloadString(url);
                }

                //json = json.Replace("\\", "");

                JArray v = JArray.Parse(json);

                foreach (var a in v)
                {
                    var symbol = a.SelectToken("id");
                    price = (double)a.SelectToken("price_usd");
                }

            }
            catch (Exception ex)
            {
                CErrorLog err = new CErrorLog();
                err.LogError(ex.Message);
                throw ex;
            }
            price = Math.Round(price, 2);
            return price;
        }
        #endregion

        public void Load()
        {
            try
            {
                //we will need to pass in the userID here to be able to limit the user to only see their list of stocks. see below *

                WealthDataContext oDc = new WealthDataContext();

                var results = (from s in oDc.tblCryptos
                               join u in oDc.tblUsers on s.UserId equals u.Id
                               where u.Id == CLogin.UserLoggedIn
                               orderby s.Symbol
                               // * where s.userid == u.userId
                               select new
                               {
                                   s.Id,
                                   custId = s.UserId,
                                   s.Symbol,
                                   s.CurrentPrice,
                               }).ToList();

                foreach (var stock in results)
                {

                    CCrypto oStock = new CCrypto();
                    CCryptoTran t = new CCryptoTran();
                    oStock.TotalPerTick = t.GetTotal(stock.Symbol);
                    oStock.TotalShares = t.GetTotalShares(stock.Symbol);
                    oStock.Id = stock.Id;
                    oStock.Ticker = stock.Symbol;
                    oStock.Price = GetPrice(stock.Symbol);
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