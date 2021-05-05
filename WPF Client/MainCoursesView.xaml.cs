using Elearning.Business;
using ElearningDatabase;
using ElearningDatabase.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WPF_Client.Custom;
using Elearning.Business.Services;
using ElearningClient;

namespace WPF_Client
{
    /// <summary>
    /// Interaction logic for MainCoursesView.xaml
    /// </summary>
    public partial class MainCoursesView : Window
    {
        private bool exploreBtnClicked = true;
        private bool mycoursesBtnClicked = false;
        private bool achievementsBtnClicked = false;
        private bool gamesBtnClicked = false;
        private int myCoursesCount = 0;
        private int exploreCoursesCount = 0;

        private User user;
        private ElearningContext db;
        
        public MainCoursesView(User user)
        {
            InitializeComponent();
            GetConnection();
            ExploreBtn.Style = this.Resources["ClickNavigationButtons"] as Style;
            ExploreCourses.Visibility = Visibility.Visible;
            MyCourses.Visibility = Visibility.Hidden;
            this.user = user;

            this.UserNameLabel.Content = this.user.Username + "!";
            InitializeExploreCoursesGrid();
            InitializeMyCoursesGrid();

            ChangeImages();

            
            
        }

        public void InitializeExploreCoursesGrid()
        {
            AllCoursesBusiness courses = new AllCoursesBusiness();
            var suggestedCourses = courses.GetSuggestedCoursesForAnUser(this.user);
            foreach (var course in suggestedCourses)
            {
                Cards card = new Cards(course);
                exploreCoursesCount++;
                if (exploreCoursesCount % 3 == 0)
                {
                    ExploreCoursesGrid.Height = 300 * (exploreCoursesCount / 3) + 100;
                }
                else
                {
                    ExploreCoursesGrid.Height = 300 * (exploreCoursesCount / 3 + 1) + 100;
                }
                //card.CourseName = course.Name;
                //card.Description = course.Description;
                //card.Category = course.Category;
                //card.Course = course;
                ExploreCoursesGrid.Children.Add(card);
                card.MouseDoubleClick += new MouseButtonEventHandler(DoubleClickExploreCourseHandler);
            }
        }

        public void GetConnection()
        {
            var dbCOntext = new DbContextOptionsBuilder<ElearningContext>();
            dbCOntext.UseSqlServer(Elearning.Database.ResourceFile.connectionString);
            db = new ElearningContext(dbCOntext.Options);
            db.Migrate();
        }

        public void DoubleClickExploreCourseHandler(object sender, MouseButtonEventArgs e)
        {
            try
            {
                EnrollmentService service = new EnrollmentService();
                service.AddEnrollment(this.user.Id, ((Cards)sender).Course.Id);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
            CourseViewWindow window = new CourseViewWindow(this.user, ((Cards)sender).Course);
            window.Show();
            this.Close();
        }

        public void DoubleClickMyCourseHandler(object sender, MouseButtonEventArgs e)
        {
            CourseViewWindow window = new CourseViewWindow(this.user, ((Cards)sender).Course);
            window.Show();
            this.Close();
        }

        public void InitializeMyCoursesGrid()
        {
            AllCoursesBusiness courses = new AllCoursesBusiness();
            var myCourses = courses.GetAllCoursesOfAnUser(this.user);
            foreach (var course in myCourses)
            {
                Cards card = new Cards(course);
                myCoursesCount++;
                if (myCoursesCount % 3 == 0)
                {
                    MyCoursesGrid.Height = 300 * (myCoursesCount / 3) + 100;
                }
                else
                {
                    MyCoursesGrid.Height = 300 * (myCoursesCount / 3 + 1) + 100;
                }
                //card.CourseName = course.Name;
                //card.Description = course.Description;
                //card.Category = course.Category;
                //card.Course = course;
                MyCoursesGrid.Children.Add(card);
                card.MouseDoubleClick += new MouseButtonEventHandler(DoubleClickMyCourseHandler);
            }
        }

        private void ChangeImages()
        {
            if (exploreBtnClicked)
            {
                ExploreImage.Source = new BitmapImage(new Uri("/Resources/explore.png", UriKind.Relative));
                MycoursesImage.Source = new BitmapImage(new Uri("/Resources/mycoursesGrey.png", UriKind.Relative));
                AchivementsImage.Source = new BitmapImage(new Uri("/Resources/achivementsGrey.png", UriKind.Relative));
                GamesImage.Source = new BitmapImage(new Uri("/Resources/gamesGrey.png", UriKind.Relative));
            }
            if (mycoursesBtnClicked)
            {
                ExploreImage.Source = new BitmapImage(new Uri("/Resources/exploreGrey.png", UriKind.Relative));
                MycoursesImage.Source = new BitmapImage(new Uri("/Resources/mycourses.png", UriKind.Relative));
                AchivementsImage.Source = new BitmapImage(new Uri("/Resources/achivementsGrey.png", UriKind.Relative));
                GamesImage.Source = new BitmapImage(new Uri("/Resources/gamesGrey.png", UriKind.Relative));
            }
            if (achievementsBtnClicked)
            {
                ExploreImage.Source = new BitmapImage(new Uri("/Resources/exploreGrey.png", UriKind.Relative));
                MycoursesImage.Source = new BitmapImage(new Uri("/Resources/mycoursesGrey.png", UriKind.Relative));
                AchivementsImage.Source = new BitmapImage(new Uri("/Resources/achivements.png", UriKind.Relative));
                GamesImage.Source = new BitmapImage(new Uri("/Resources/gamesGrey.png", UriKind.Relative));
            }
            if (gamesBtnClicked)
            {
                ExploreImage.Source = new BitmapImage(new Uri("/Resources/exploreGrey.png", UriKind.Relative));
                MycoursesImage.Source = new BitmapImage(new Uri("/Resources/mycoursesGrey.png", UriKind.Relative));
                AchivementsImage.Source = new BitmapImage(new Uri("/Resources/achivementsGrey.png", UriKind.Relative));
                GamesImage.Source = new BitmapImage(new Uri("/Resources/games.png", UriKind.Relative));
            }
        }

        private void ExploreBtn_Click(object sender, RoutedEventArgs e)
        {
            exploreBtnClicked = true;
            mycoursesBtnClicked = false;
            achievementsBtnClicked = false;
            gamesBtnClicked = false;
            ExploreBtn.Style = this.Resources["ClickNavigationButtons"] as Style;
            AchievementsBtn.Style = this.Resources["NavigationButtons"] as Style;
            GamificationBtn.Style = this.Resources["NavigationButtons"] as Style;
            MyCoursesBtn.Style = this.Resources["NavigationButtons"] as Style;
            ExploreCourses.Visibility = Visibility.Visible;
            MyCourses.Visibility = Visibility.Hidden;
            ChangeImages();
        }
        private void ExploreBtn_MouseEnter(object sender, MouseEventArgs e)
        {
            if (!exploreBtnClicked)
                ExploreImage.Source = new BitmapImage(new Uri("/Resources/explore.png", UriKind.Relative));
        }

        private void ExploreBtn_MouseLeave(object sender, MouseEventArgs e)
        {
            if (!exploreBtnClicked)
                ExploreImage.Source = new BitmapImage(new Uri("/Resources/exploreGrey.png", UriKind.Relative));
        }
        private void MyCoursesBtn_Click(object sender, RoutedEventArgs e)
        {
            mycoursesBtnClicked = true;
            exploreBtnClicked = false;
            achievementsBtnClicked = false;
            gamesBtnClicked = false;
            MyCoursesBtn.Style = this.Resources["ClickNavigationButtons"] as Style;
            ExploreBtn.Style = this.Resources["NavigationButtons"] as Style;
            AchievementsBtn.Style = this.Resources["NavigationButtons"] as Style;
            GamificationBtn.Style = this.Resources["NavigationButtons"] as Style;
            ExploreCourses.Visibility = Visibility.Hidden;
            MyCourses.Visibility = Visibility.Visible;
            ChangeImages();
        }
        private void MyCoursesBtn_MouseEnter(object sender, MouseEventArgs e)
        {
            if (!mycoursesBtnClicked)
                MycoursesImage.Source = new BitmapImage(new Uri("/Resources/mycourses.png", UriKind.Relative));
        }

        private void MyCoursesBtn_MouseLeave(object sender, MouseEventArgs e)
        {
            if (!mycoursesBtnClicked)
                MycoursesImage.Source = new BitmapImage(new Uri("/Resources/mycoursesGrey.png", UriKind.Relative));
        }

        private void AchievementsBtn_Click(object sender, RoutedEventArgs e)
        {
            achievementsBtnClicked = true;
            exploreBtnClicked = false;
            mycoursesBtnClicked = false;
            gamesBtnClicked = false;
            AchievementsBtn.Style = this.Resources["ClickNavigationButtons"] as Style;
            ExploreBtn.Style = this.Resources["NavigationButtons"] as Style;
            GamificationBtn.Style = this.Resources["NavigationButtons"] as Style;
            MyCoursesBtn.Style = this.Resources["NavigationButtons"] as Style;
            ChangeImages();
        }
        private void AchievementsBtn_MouseEnter(object sender, MouseEventArgs e)
        {
            if (!achievementsBtnClicked)
                AchivementsImage.Source = new BitmapImage(new Uri("/Resources/achivements.png", UriKind.Relative));
        }

        private void AchievementsBtn_MouseLeave(object sender, MouseEventArgs e)
        {
            if (!achievementsBtnClicked)
                AchivementsImage.Source = new BitmapImage(new Uri("/Resources/achivementsGrey.png", UriKind.Relative));
        }

        private void GamificationButton_Click(object sender, RoutedEventArgs e)
        {
            gamesBtnClicked = true;
            exploreBtnClicked = false;
            mycoursesBtnClicked = false;
            achievementsBtnClicked = false;
            GamificationBtn.Style = this.Resources["ClickNavigationButtons"] as Style;
            AchievementsBtn.Style = this.Resources["NavigationButtons"] as Style;
            ExploreBtn.Style = this.Resources["NavigationButtons"] as Style;
            MyCoursesBtn.Style = this.Resources["NavigationButtons"] as Style;
            GamesImage.Source = new BitmapImage(new Uri("/Resources/games.png", UriKind.Relative));
            ChangeImages();
        }

        private void GamificationBtn_MouseEnter(object sender, MouseEventArgs e)
        {
            if (!gamesBtnClicked)
                GamesImage.Source = new BitmapImage(new Uri("/Resources/games.png", UriKind.Relative));
        }

        private void GamificationBtn_MouseLeave(object sender, MouseEventArgs e)
        {
            if (!gamesBtnClicked)
                GamesImage.Source = new BitmapImage(new Uri("/Resources/gamesGrey.png", UriKind.Relative));
        }

        //for using left click to show contextmenu
        private void Profile_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                Image image = sender as Image;
                ContextMenu contextMenu = image.ContextMenu;
                contextMenu.PlacementTarget = image;
                contextMenu.IsOpen = true;
                e.Handled = true;
            }
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow login = new LoginWindow();
            login.Show();
            this.Close();
        }

    }
}
