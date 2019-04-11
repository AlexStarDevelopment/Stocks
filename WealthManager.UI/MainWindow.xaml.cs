using System;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using WealthManager.BL;
using WealthManager.UI.Risk;

namespace WealthManager.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Refresh();
            Timer();
        }

        public void Refresh()
        {
            decimal runningTotal = 0;

            try
            {
                CStockList stocks = new CStockList();

                stocks.Load();
                dgStocks.ItemsSource = stocks;

                //get total
                foreach (var s in stocks)
                {
                    CTransaction t = new CTransaction();
                    runningTotal = t.GetTotal(s.Ticker) + runningTotal;
                }

                lblTotal.Content = runningTotal.ToString("c2");

                dgStocks.Columns[4].Header = "Quantity";
                dgStocks.Columns[5].Header = "Position Value";
                dgStocks.Columns[0].Visibility = Visibility.Hidden;
                dgStocks.Columns[1].Visibility = Visibility.Hidden;
            }
            catch (Exception ex)
            {
            }


            try
            {
                CTransactionList trans = new CTransactionList();
                trans.Load();

                dgTransactions_Copy.ItemsSource = trans;

                dgTransactions_Copy.Columns[0].Header = "Stock";
                dgTransactions_Copy.Columns[3].Header = "Date of Transaction";
                dgTransactions_Copy.Columns[4].Header = "Buy";
                dgTransactions_Copy.Columns[5].Header = "Quantity";
                dgTransactions_Copy.Columns[6].Header = "Price Per Share";

                dgTransactions_Copy.Columns[1].Visibility = Visibility.Hidden;
                dgTransactions_Copy.Columns[2].Visibility = Visibility.Hidden;

            }
            catch (Exception ex)
            {
            }

            try
            {
                CCryptoList stocks = new CCryptoList();

                stocks.Load();
                dgCrypto1.ItemsSource = stocks;

                //get total
                foreach (var s in stocks)
                {
                    CCryptoTran t = new CCryptoTran();
                    decimal.TryParse(t.GetTotal(s.Ticker).ToString(), out decimal g);
                    runningTotal = g + runningTotal;
                }

                lblTotal.Content = runningTotal.ToString("c2");

                dgCrypto1.Columns[4].Header = "Quantity";
                dgCrypto1.Columns[5].Header = "Position Value";
                dgCrypto1.Columns[0].Visibility = Visibility.Hidden;
                dgCrypto1.Columns[1].Visibility = Visibility.Hidden;
            }
            catch (Exception ex)
            {
            }

            try
            {
                CCryptoTranList crypto = new CCryptoTranList();
                crypto.Load();

                dgCrypto_Copy.ItemsSource = crypto;

                dgCrypto_Copy.Columns[0].Header = "Currency";
                dgCrypto_Copy.Columns[3].Header = "Transaction Date";
                dgCrypto_Copy.Columns[4].Header = "Buy";
                dgCrypto_Copy.Columns[5].Header = "Quantity";
                dgCrypto_Copy.Columns[6].Header = "Price Per Share";

                dgCrypto_Copy.Columns[1].Visibility = Visibility.Hidden;
                dgCrypto_Copy.Columns[2].Visibility = Visibility.Hidden;
            }
            catch (Exception)
            {
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddStock add = new AddStock();
            add.ShowDialog();
        }

        private void btnAddTrans_Click(object sender, RoutedEventArgs e)
        {
            EditTransactions add = new EditTransactions();
            add.ShowDialog();
        }
        private void btnRiskCalc_Click(object sender, RoutedEventArgs e)
        {
            Form1 form = new Form1();
            form.Show();
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            Login login = new Login();
            Close();
            login.ShowDialog();
            
        }

        private void btnExpClick(object sender, RoutedEventArgs e)
        {
            try
            {
                CTransactionList c = new CTransactionList();
                c.Export();

                MessageBox.Show("Export Successful", "Yay!");
            }
            catch (Exception ex)
            {

            }
        }

        private void btnLaunchChart(object sender, RoutedEventArgs e)
        {
            //this will be the link to website once deployed 
            System.Diagnostics.Process.Start("http://wealthmanagerchartingpro.azurewebsites.net/Home/Index");
        }

        public void Timer()
        {
            System.Timers.Timer aTimer = new System.Timers.Timer();
            aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            aTimer.Interval = 60000;
            aTimer.Enabled = true;
        }

        private void OnTimedEvent(object sender, ElapsedEventArgs e)
        {
            Refresh();
        }

        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            Refresh();
        }

        private void btnAddCryptoTrans_Click(object sender, RoutedEventArgs e)
        {
            AddCrypto add = new AddCrypto();
            add.Show();
        }

        private void btnAddCryptoClick(object sender, RoutedEventArgs e)
        {
            AddCryptoCurr add = new AddCryptoCurr();
            add.Show();
        }

        private void btnExpClick2(object sender, RoutedEventArgs e)
        {
            try
            {
                CCryptoTranList c = new CCryptoTranList();
                c.Export();

                MessageBox.Show("Export Successful", "Yay!");
            }
            catch (Exception ex)
            {

            }
        }
    }
}
