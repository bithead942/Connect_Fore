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

namespace ConnectFore
{
    /// <summary>
    /// Interaction logic for Admin.xaml
    /// </summary>
    public partial class AdminPassword : Window
    {
        String strPassword = "";

        public AdminPassword()
        {
            InitializeComponent();
        }

        void checkPassword()
        {
            if (txtPassword.Password == "314159")
            {
                lblResult.Text = "";
                txtPassword.Password = "";
                strPassword = "";

                AdminFunctions frmAdminFunction = new AdminFunctions();
                frmAdminFunction.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                frmAdminFunction.ShowDialog();  //Waits for a response

                this.Close();
            }
            else
            {
                lblResult.Text = "Invalid Password.";
                txtPassword.Password = "";
                strPassword = "";
            }
        }

        #region BUTTONS
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnPassword_Click(object sender, RoutedEventArgs e)
        {
            checkPassword();
        }


        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                checkPassword();
            }
        }

        private void btn0_Click(object sender, RoutedEventArgs e)
        {
            strPassword = strPassword + "0";
            txtPassword.Password = strPassword;
        }

        private void btn1_Click(object sender, RoutedEventArgs e)
        {
            strPassword = strPassword + "1";
            txtPassword.Password = strPassword;

        }

        private void btn2_Click(object sender, RoutedEventArgs e)
        {
            strPassword = strPassword + "2";
            txtPassword.Password = strPassword;

        }

        private void btn3_Click(object sender, RoutedEventArgs e)
        {
            strPassword = strPassword + "3";
            txtPassword.Password = strPassword;

        }

        private void btn4_Click(object sender, RoutedEventArgs e)
        {
            strPassword = strPassword + "4";
            txtPassword.Password = strPassword;

        }

        private void btn5_Click(object sender, RoutedEventArgs e)
        {
            strPassword = strPassword + "5";
            txtPassword.Password = strPassword;

        }

        private void btn6_Click(object sender, RoutedEventArgs e)
        {
            strPassword = strPassword + "6";
            txtPassword.Password = strPassword;

        }

        private void btn7_Click(object sender, RoutedEventArgs e)
        {
            strPassword = strPassword + "7";
            txtPassword.Password = strPassword;

        }

        private void btn8_Click(object sender, RoutedEventArgs e)
        {
            strPassword = strPassword + "8";
            txtPassword.Password = strPassword;

        }

        private void btn9_Click(object sender, RoutedEventArgs e)
        {
            strPassword = strPassword + "9";
            txtPassword.Password = strPassword;

        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            strPassword = "";
            txtPassword.Password = "";
            lblResult.Text = "";

        }

        #endregion
    }
}
