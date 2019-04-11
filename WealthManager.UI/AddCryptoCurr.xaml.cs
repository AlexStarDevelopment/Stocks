using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WealthManager.BL;

namespace WealthManager.UI
{
    /// <summary>
    /// Interaction logic for AddCryptoCurr.xaml
    /// </summary>
    public partial class AddCryptoCurr : Window
    {
        public AddCryptoCurr()
        {
            InitializeComponent();
            Refresh();
        }

        private void cboStock_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cboStock.SelectedIndex > -1)
            {
                CCrypto stock = new CCrypto();
                CCryptoList stocks = new CCryptoList();
                stock = stocks[cboStock.SelectedIndex];
                txtStock.Text = stock.Ticker;
            }
        }
        public void Refresh()
        {
            cboStock.ItemsSource = null;

            CCryptoList stocks = new CCryptoList();
            stocks.Load();
            cboStock.ItemsSource = stocks;

            cboStock.DisplayMemberPath = "Symbol";
            cboStock.SelectedValuePath = "Id";
            txtStock.Text = "";
            txtStock.Focus();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            CCrypto stock = new CCrypto();
            stock.Ticker = txtStock.Text;
            stock.Insert();
            Refresh();
            //Close();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            CCrypto stock = new CCrypto();
            stock.Delete();
            Refresh();

            MainWindow m = new MainWindow();
            m.Refresh();
            //Close();
        }
    }
}
