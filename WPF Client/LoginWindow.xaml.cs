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
using Microsoft.Data.SqlClient;

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
            var establishConnection= EstablishConnection();
            CheckLogin(establishConnection);
        }

        private void CheckLogin(SqlConnection conn)
        {
            String username = LoginUsernameTxt.Text;
            String password = LoginPasswordTxt.Text;

            String query = "SELECT COUNT(*) FROM Users " +
                "WHERE (Username = @Username AND Password = @Password) OR " +
                "(Email = @Username AND Password = @Password)";

            SqlCommand command = new SqlCommand(query, conn);
            command.CommandType = CommandType.Text;
            command.Parameters.AddWithValue("@Username", username);
            command.Parameters.AddWithValue("@Password", password);
            int count = Convert.ToInt32(command.ExecuteScalar());

            if (count == 1)
            {
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Invalid credentials!");
            }
        }

        private void RegisterBtn_Click(object sender, RoutedEventArgs e)
        {
            EstablishConnection();
        }
    }
}
