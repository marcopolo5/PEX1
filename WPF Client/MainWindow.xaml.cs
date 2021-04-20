using ElearningDatabase;
using ElearningDatabase.Models;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using System.Windows;
using Elearning.Business;
using System.Diagnostics;
using System.Windows.Controls;
using Elearning.Business.Services;
using System;
using WPF_Client;

namespace ElearningClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ElearningContext db;
        private User user;

        public MainWindow()
        {
            InitializeComponent();
            GetConnection();
            
        }

        public void GetConnection()
        {
            var dbCOntext = new DbContextOptionsBuilder<ElearningContext>();
            dbCOntext.UseSqlServer(Elearning.Database.ResourceFile.connectionString);
            db = new ElearningContext(dbCOntext.Options);
            db.Migrate();
        }

        public void SetUser(User user)
        {
            Debug.WriteLine("set user: user = " + user + " user id = " + user.Id);
            this.user = user;
            InitializeGrids();
        }
        public void InitializeGrids()
        {
            InitializeCoursesDataGrid();
            InitializeSuggestedCoursesDataGrid();
        }

        public void InitializeCoursesDataGrid()
        {
            AllCoursesBusiness courses = new AllCoursesBusiness();
            CoursesDataGrid.ItemsSource = courses.GetAllCoursesOfAnUser(this.user);
        }

        public void InitializeSuggestedCoursesDataGrid()
        {
            AllCoursesBusiness courses = new AllCoursesBusiness();
            SuggestedCoursesDataGrid.ItemsSource = courses.GetSuggestedCoursesForAnUser(this.user);
        }

        private void AccessCourseButton_Click(object sender, RoutedEventArgs e)
        {
            int selectedIndex = CoursesDataGrid.SelectedIndex;
            
            if (selectedIndex < 0)
            {
                MessageBox.Show("Please select a course!");
            }
            else
            {
                CourseDTO selectedCourse = CoursesDataGrid.SelectedItem as CourseDTO;
                string message = string.Format("You selected the following course:\nName = {0} \nDescription = {1} \nCategory = {2}", 
                                                selectedCourse.Name, selectedCourse.Description, selectedCourse.Category);
                MessageBox.Show(message);
                CourseViewWindow window = new CourseViewWindow();
                window.Show();
            }

        }


        //private void CoursesDataGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        //{
        //    int selectedIndex = CoursesDataGrid.SelectedIndex;


        //    CourseDTO selectedCourse = CoursesDataGrid.SelectedItem as CourseDTO;
        //    string message = string.Format("You selected the following course:\nName = {0} \nDescription = {1} \nCategory = {2}",
        //                                    selectedCourse.Name, selectedCourse.Description, selectedCourse.Category);
        //    MessageBox.Show(message);
        //}

        private void TryCourseButton_Click(object sender, RoutedEventArgs e)
        {
            int selectedIndex = SuggestedCoursesDataGrid.SelectedIndex;


            CourseDTO selectedCourse = SuggestedCoursesDataGrid.SelectedItem as CourseDTO;
            try
            {
                EnrollmentService service = new EnrollmentService();
                service.AddEnrollment(this.user.Id, selectedCourse.Id);
                this.InitializeCoursesDataGrid();
                this.InitializeSuggestedCoursesDataGrid();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
            string message = string.Format("You selected the following course:\nName = {0} \nDescription = {1} \nCategory = {2}",
                                            selectedCourse.Name, selectedCourse.Description, selectedCourse.Category);
            
            MessageBox.Show(message);
            CourseViewWindow window = new CourseViewWindow();
            window.Show();

        }
    }
}