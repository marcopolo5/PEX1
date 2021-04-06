﻿using Elearning.Business;
using ElearningDatabase;
using ElearningDatabase.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;

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
            signupGrid.Visibility = Visibility.Hidden;
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
                // Debug.WriteLine("on clik: logged user = " + loggedUser + " id = " + loggedUser.Id);
                MainWindow mainWindow = new MainWindow();
                mainWindow.SetUser(loggedUser);
                mainWindow.Show();
                this.Close();
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

            try
            {
                if (register.ValidateRegister(username, password, firstname, lastname, confirmPassword, checkInstructor, email))
                {
                    //this.ShowMainWindow();
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
        }

        private void signupBtn_Click(object sender, RoutedEventArgs e)
        {
            signinGrid.Visibility = Visibility.Hidden;
            signupGrid.Visibility = Visibility.Visible;
        }
    }
}