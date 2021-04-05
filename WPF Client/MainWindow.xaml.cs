using ElearningDatabase;
using ElearningDatabase.Models;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using System.Windows;
using Elearning.Business;

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
            InitializeCoursesDataGrid();
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
            this.user = user;
        }

        public void InitializeCoursesDataGrid()
        {
            AllCoursesBusiness courses = new AllCoursesBusiness();
            CoursesDataGrid.ItemsSource = courses.GetAllCoursesOfAnUser(this.user);
        }

    }
}