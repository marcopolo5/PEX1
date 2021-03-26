using EFElearning;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using System.Windows;

namespace WPF_Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ElearningContext db;

        public MainWindow()
        {
            GetConnection();
            InitializeComponent();
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