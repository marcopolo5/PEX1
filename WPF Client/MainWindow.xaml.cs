using ElearningDatabase;
using ElearningDatabase.Models;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using System.Windows;
using Elearning.Business;
using System.Diagnostics;

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

    }
}