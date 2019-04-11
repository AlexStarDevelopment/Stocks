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
    /// Interaction logic for EditTransactions.xaml
    /// </summary>
    public partial class EditTransactions : Window
    {
        CStockList stocks;
        CTransactionList trans;

        CTransaction tran;
        CStock stock;

        public EditTransactions()
        {
            InitializeComponent();
            Refresh();
        }

        private void cboTicker_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cboTicker.SelectedIndex > -1)
            {
                stock = stocks[cboTicker.SelectedIndex];
            }
        }
        public void Refresh()
        {
                cboTicker.ItemsSource = null;

                stocks = new CStockList();
                stocks.Load();
                cboTicker.ItemsSource = stocks;

                cboTicker.DisplayMemberPath = "Ticker";
                cboTicker.SelectedValuePath = "Ticker";
            
        }

        public void add()
        {
            try
            {
                //veriables
                int quan;
                decimal pps;

                //parse
                Int32.TryParse(txtQuantity.Text, out quan);
                decimal.TryParse(txtPPS.Text, out pps);

                //new transaction
                tran = new CTransaction();

                //contents
                tran.StockId = tran.GetGuid(cboTicker.SelectedValue.ToString());
                tran.TransDate = DTP.SelectedDate.Value;
                tran.Quantity = quan;
                tran.PricePerSharePaid = pps;

                //if
                if ((bool)rdoBuy.IsChecked)
                {
                    tran.Buy = true;
                }
                if ((bool)rdoSell.IsChecked)
                {
                    tran.Buy = false;
                }

                //insert
                tran.Insert();

                MessageBox.Show("Transaction successfuly added", "Yay!");

                //email 
                CUser u = new CUser();
                //u.SendEmail();

                //close form
                this.Close();
                // I have decided to not close the form after adding a transaction
                // for usability reasons. It is annoying to enter multiple transactions 
                // and clicking to open the dialog box again and again 

                txtPPS.Text = string.Empty;
                txtQuantity.Text = string.Empty;
                DTP = null;
                rdoBuy.IsChecked = false;
                rdoSell.IsChecked = false;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            add();
        }
    }
}
