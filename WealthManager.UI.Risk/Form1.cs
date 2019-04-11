using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WealthManager.UI.Risk
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            comboBox1.Items.Add("Conservative");
            comboBox1.Items.Add("Moderatly Conservative");
            comboBox1.Items.Add("Moderate");
            comboBox1.Items.Add("Moderatly Aggressive");
            comboBox1.Items.Add("Aggresive");
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(comboBox1.SelectedItem.ToString() == "Conservative")
            {
                lblCashP.Text = "70%";
                lblBondsP.Text = "30%";
                lblStocksP.Text = "0%";
                lblSpecP.Text = "0%";
            }
            if (comboBox1.SelectedItem.ToString() == "Moderatly Conservative")
            {
                lblCashP.Text = "50%";
                lblBondsP.Text = "40%";
                lblStocksP.Text = "10%";
                lblSpecP.Text = "0%";
            }
            if (comboBox1.SelectedItem.ToString() == "Moderate")
            {
                lblCashP.Text = "15%";
                lblBondsP.Text = "30%";
                lblStocksP.Text = "50%";
                lblSpecP.Text = "5%";
            }
            if (comboBox1.SelectedItem.ToString() == "Moderatly Aggressive")
            {
                lblCashP.Text = "10%";
                lblBondsP.Text = "25%";
                lblStocksP.Text = "60%";
                lblSpecP.Text = "5%";
            }
            if (comboBox1.SelectedItem.ToString() == "Aggresive")
            {
                lblCashP.Text = "5%";
                lblBondsP.Text = "20%";
                lblStocksP.Text = "65%";
                lblSpecP.Text = "10%";
            }


        }
    }
}
