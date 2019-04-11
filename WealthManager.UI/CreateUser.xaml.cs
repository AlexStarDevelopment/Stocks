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
    /// Interaction logic for CreateUser.xaml
    /// </summary>
    public partial class CreateUser : Window
    {
        public CreateUser()
        {
            InitializeComponent();
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtPword1.Text == txtPword2.Text)
                {
                    CUser u = new CUser();
                    u.FirstName = txtFname.Text;
                    u.LastName = txtLname.Text;
                    u.Email = txtEmail.Text;
                    u.Password = txtPword1.Text;
                    u.Insert();

                    MessageBox.Show("User Created Sucessfully", "Yay");
                    Login main = new Login();
                    this.Close();
                    main.Show();
                }
                else
                {
                    lblError.Content = "Passwords do not match";
                }
            }
            catch (Exception ex)
            {
                lblError.Content = ex.Message.ToString();
            }
        }
    }
}
