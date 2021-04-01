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

        private SqlConnection EstablishConnection()
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conString"].ConnectionString);
            try
            {
                
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
    
                }
                return conn;

            }
            
            catch (Exception ex)
            {
                
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return conn;
            
        }
        private void LoginBtn_Click(object sender, RoutedEventArgs e)
        {
            var establishConnection = EstablishConnection();
            establishConnection.Open();
            Login login = new Login();
            String username = UsernameTxt.Text;
            String password = PasswordTxt.Text;
            try
            {
                login.ValidateLogin(establishConnection, username, password);
                this.ShowMainWindow();
            }
            catch(Exception ex)
            {
                UsernameTxt.Clear();
                PasswordTxt.Clear();
                MessageBox.Show(ex.Message);
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
            EstablishConnection();
            Register register = new Register();
            string username = UsernameTxt.Text;
            string password = PasswordTxt.Text;
            string firstname = FirstNameTxt.Text;
            string lastname = LastNameTxt.Text;
            string confirmPassword = ConfirmPasswordTxt.Text;
            bool checkInstructor = InstructorCheck.IsEnabled;
            string email = EmailTxt.Text;

            try
            {
                if (register.ValidateRegister(username, password, firstname, lastname, confirmPassword, checkInstructor, email))
                {
                    this.ShowMainWindow();
                }

            }
            catch (Exception ex)
            {
                UsernameTxt.Clear();
                PasswordTxt.Clear();
                FirstNameTxt.Clear();
                LastNameTxt.Clear();
                ConfirmPasswordTxt.Clear();
                EmailTxt.Clear();

                MessageBox.Show(ex.Message);
            }

        }
    }
}
