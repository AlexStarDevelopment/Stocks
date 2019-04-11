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
    /// Interaction logic for AddCrypto.xaml
    /// </summary>
    public partial class AddCrypto : Window
    {
        CCrypto stock;
        CCryptoList stocks;

        public AddCrypto()
        {
            InitializeComponent();
            Refresh();
        }

        private void cboTicker_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cboName.SelectedIndex > -1)
            {
                stock = stocks[cboName.SelectedIndex];
            }
        }
        public void Refresh()
        {
            cboName.ItemsSource = null;
            stocks = new CCryptoList();
            stocks.Load();
            cboName.ItemsSource = stocks;

            cboName.DisplayMemberPath = "Ticker";
            cboName.SelectedValuePath = "Ticker";

        }
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            Add();
        }

        public void Add()
        {

            try
            {
                //veriables
                decimal quan;
                double pps;

                //parse
                decimal.TryParse(txtQuantity.Text, out quan);
                double.TryParse(txtPPS.Text, out pps);

                //new transaction
                CCryptoTran tran = new CCryptoTran();

                //contents
                tran.StockId = tran.GetGuid(cboName.SelectedValue.ToString());
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
                //tran.Insert(cboName.SelectedValue.ToString());

                MessageBox.Show("Transaction successfuly added", "Yay!");

                //email 
                CUser u = new CUser();
                //u.SendEmail();

                //close form
                //this.Close();
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
    }
}
