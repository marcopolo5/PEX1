using ElearningDatabase;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using System.Windows;

namespace ElearningClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ElearningContext db;

        public MainWindow()
        {
             InitializeComponent();
            GetConnection();
        }

        public void GetConnection()
        {
            var dbCOntext = new DbContextOptionsBuilder<ElearningContext>();
            dbCOntext.UseSqlServer(ConfigurationManager.ConnectionStrings["conString"].ConnectionString);
            db = new ElearningContext(dbCOntext.Options);
            db.Migrate();
        }
    }
}