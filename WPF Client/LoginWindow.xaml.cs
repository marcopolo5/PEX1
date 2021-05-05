using Elearning.Business;
using ElearningDatabase;
using ElearningDatabase.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using WPF_Client;

namespace ElearningClient
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private ElearningContext db;
        private User user;
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
            signupGrid.Visibility = Visibility.Hidden;
            signinBtn.Style = this.Resources["onClick"] as Style;
            user = new User();
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
            Login login = new Login();
            string username = LoginUsernameTxt.Text;
            string password = LoginPasswordTxt.Password.ToString();
            try
            {
                User loggedUser = login.GetLoggedUser(username, password);
               
                if (loggedUser.Role.ToString().Equals("Student"))
                {
                    MainCoursesView mainWindow = new MainCoursesView(loggedUser);
                    mainWindow.Show();
                    this.Close();
                }
                else if (loggedUser.Role.ToString().Equals("Trainer"))
                {
                    MainTrainerView mainTrainerView = new MainTrainerView(loggedUser);
                    mainTrainerView.Show();
                    this.Close();
                }
                
            }
            catch (Exception ex)
            {
                UsernameTxt.Clear();
                PasswordTxt.Clear();
                MessageBox.Show(ex.Message);
            }
        }
        
        public void ShowMainWindow(User user)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.SetUser(user);
            mainWindow.Show();
            this.Close();
        }
        
        private void RegisterBtn_Click(object sender, RoutedEventArgs e)
        {
            Register register = new Register();
            string username = UsernameTxt.Text;
            string password = PasswordTxt.Password.ToString();
            string firstname = FirstNameTxt.Text;
            string lastname = LastNameTxt.Text;
            string confirmPassword = ConfirmPasswordTxt.Password.ToString();
            bool checkInstructor = InstructorRadioBtn.IsEnabled;
            bool checkStudent = StudentRadioBtn.IsEnabled;
            string email = EmailTxt.Text;
            user.Username = username;
            user.Password = password;
            user.FirstName = firstname;
            user.LastName = lastname;
            user.Email = email;
            user.Role = InstructorRadioBtn.IsEnabled? RoleEnum.Trainer: RoleEnum.Student;

            if(register.CheckIfUserExists(UsernameTxt.Text))
            {
                MessageBox.Show("Username already exists! Please enter a new one.");
            }

            try
            {
                if (register.ValidateRegister(username, password, firstname, lastname, confirmPassword, checkInstructor, email))
                {
                    MessageBox.Show("You're now a part of our e-Learning community. Enjoy!");
                    this.ShowMainWindow(user);                 
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

        //button events
        private void LoginPasswordTxt_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (Keyboard.GetKeyStates(Key.CapsLock) == KeyStates.Toggled)
            {
                capslock1.Content = "CAPS LOCK IS ON";
            }
            else
            {
                capslock1.Content = "";
            }
        }

        private void PasswordTxt_KeyDown(object sender, KeyEventArgs e)
        {
            if (Keyboard.GetKeyStates(Key.CapsLock) == KeyStates.Toggled)
            {
                capslock2.Content = "CAPS LOCK IS ON";
            }
            else
            {
                capslock2.Content = "";
            }
        }

        private void ConfirmPasswordTxt_KeyDown(object sender, KeyEventArgs e)
        {
            if (Keyboard.GetKeyStates(Key.CapsLock) == KeyStates.Toggled)
            {
                capslock3.Content = "CAPS LOCK IS ON";
            }
            else
            {
                capslock3.Content = "";
            }
        }

        private void signinBtn_Click(object sender, RoutedEventArgs e)
        {
            signupGrid.Visibility = Visibility.Hidden;
            signinGrid.Visibility = Visibility.Visible;
            signupBtn.Style = this.Resources["switchGrid"] as Style;
            signinBtn.Style = this.Resources["onClick"] as Style;
        }

        private void signupBtn_Click(object sender, RoutedEventArgs e)
        {
            signinGrid.Visibility = Visibility.Hidden;
            signupGrid.Visibility = Visibility.Visible;
            signupBtn.Style = this.Resources["onClick"] as Style;
            signinBtn.Style = this.Resources["switchGrid"] as Style;
        }

        
    }
}