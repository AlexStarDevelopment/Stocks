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
    public partial class AddStock : Window
    {
        CStockList stocks;
        CStock stock;

        public AddStock()
        {
            InitializeComponent();
            Refresh();
        }

        private void cboStock_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(cboStock.SelectedIndex > -1)
            {
                stock = stocks[cboStock.SelectedIndex];
                txtStock.Text = stock.Ticker;
            }
        }
        public void Refresh()
        {
            cboStock.ItemsSource = null;

            stocks = new CStockList();
            stocks.Load();
            cboStock.ItemsSource = stocks;

            cboStock.DisplayMemberPath = "Ticker";
            cboStock.SelectedValuePath = "Id";
            txtStock.Text = "";
            txtStock.Focus();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            stock = new CStock();
            stock.Ticker = txtStock.Text;
            stock.Insert();
            Refresh();
            //Close();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            stock.Delete();
            Refresh();

            MainWindow m = new MainWindow();
            m.Refresh();
            //Close();
        }
    }
}
