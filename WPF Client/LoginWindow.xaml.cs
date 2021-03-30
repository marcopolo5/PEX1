using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Elearning.Business;
using System.Data.SqlClient;

namespace ElearningClient
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void LoginBtn_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conString"].ConnectionString);
            try
            {
                if (conn.State == ConnectionState.Closed)
                { 
                    conn.Open();
                    Login login = new Login();
                    String username = UsernameTxt.Text;
                    String password = PasswordTxt.Text;
                    login.ValidateLogin(conn, username, password);
                    this.ShowMainWindow();
                    
                }
            }
            catch(Exception ex)
            {
                UsernameTxt.Clear();
                PasswordTxt.Clear();
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        public void ShowMainWindow()
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void RegisterBtn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
