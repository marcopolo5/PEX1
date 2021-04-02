using Elearning.Business;
using ElearningDatabase;
using Microsoft.EntityFrameworkCore;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace ElearningClient
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private ElearningContext db;

        public void GetConnection()
        {
            var dbCOntext = new DbContextOptionsBuilder<ElearningContext>();
            dbCOntext.UseSqlServer(Elearning.Database.ResourceFile.connectionString);
            db = new ElearningContext(dbCOntext.Options);
            db.Migrate();
        }

        public LoginWindow()
        {
            InitializeComponent();
            GetConnection();
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
            string username = LoginUsernameTxt.Text;
            string password = LoginPasswordTxt.Text;
            try
            {
                login.ValidateLogin(establishConnection, username, password);
                this.ShowMainWindow();
            }
            catch (Exception ex)
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