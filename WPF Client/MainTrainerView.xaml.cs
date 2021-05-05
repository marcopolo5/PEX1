using Elearning.Business.Services;
using ElearningClient;
using ElearningDatabase.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace WPF_Client
{
    /// <summary>
    /// Interaction logic for MainTrainerView.xaml
    /// </summary>
    public partial class MainTrainerView : Window
    {
        private bool exploreBtnClicked = true;
        private bool mycoursesBtnClicked = false;
        private bool achievementsBtnClicked = false;
        private bool gamesBtnClicked = false;
        private int myCoursesCount = 0;
        private int exploreCoursesCount = 0;
        private User trainer;
        public MainTrainerView(User user)
        {
            InitializeComponent();
            ExploreBtn.Style = this.Resources["ClickNavigationButtons"] as Style;
            ExploreCourses.Visibility = Visibility.Visible;
            MyCourses.Visibility = Visibility.Hidden;

            this.DataContext = this;
            
            this.trainer = user;
            this.TrainerNameLabel.Content = this.trainer.Username + "!";

            InitializeMyCourses();
            InitializeExploreCourses();

        }

        private void InitializeMyCourses()
        {
            TrainerCoursesService courses = new TrainerCoursesService();
            var myCourses = courses.GetAllCoursesOfATrainer(this.trainer);
            foreach (var course in myCourses)
            {
                CardsTrainer card = new CardsTrainer(course);
                myCoursesCount++;
                if (myCoursesCount % 3 == 0)
                {
                    MyCoursesGrid.Height = 300 * (myCoursesCount / 3) + 100;
                }
                else
                {
                    MyCoursesGrid.Height = 300 * (myCoursesCount / 3 + 1) + 100;
                }
                MyCoursesGrid.Children.Add(card);
                card.MenuItemEdit.Click += new RoutedEventHandler((sender, e) => EditCourseHandler(sender, e, card));
                card.MenuItemDelete.Click += new RoutedEventHandler((sender, e) => DeleteCourseHandler(sender, e, card));
                
                card.MouseDoubleClick += new MouseButtonEventHandler(DoubleClickMyCourseHandler);
            }
        }

        public void EditCourseHandler(object sender, RoutedEventArgs e, CardsTrainer card)
        {
            EditCourseUI editCourseWindow = new EditCourseUI(card.Course);
            editCourseWindow.ShowDialog();

            card.Course = editCourseWindow.Course;
            UpdateEditedCard(card);
            
        }

        public void UpdateEditedCard(CardsTrainer card)
        {
            card.CourseNameTxtBlock.Text = card.Course.Name;
            card.CourseDescription.Text = card.Course.Description;
            card.CourseCategory.Text = card.Course.Category.ToString();
        }

        public void DeleteCourseHandler(object sender, RoutedEventArgs e, CardsTrainer card)
        {
            try
            {
                TrainerCoursesService service = new TrainerCoursesService();
                service.DeleteCourse(card.Course);
                MyCoursesGrid.Children.Remove(card);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }


        public void InitializeExploreCourses()
        {
            
            TrainerCoursesService courses = new TrainerCoursesService();
            var exploredCourses = courses.GetSuggestedCoursesForATrainer(this.trainer);
            foreach (var course in exploredCourses)
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
                card.Course = course;
                ExploreCoursesGrid.Children.Add(card);

                card.MouseDoubleClick += new MouseButtonEventHandler(DoubleClickExploreCourseHandler);
            }
        }

        public void DoubleClickExploreCourseHandler(object sender, MouseButtonEventArgs e)
        {
           
            CourseViewWindow window = new CourseViewWindow(this.trainer, ((Cards)sender).Course);
            window.Show();
        }

        public void DoubleClickMyCourseHandler(object sender, MouseButtonEventArgs e)
        {

            CourseViewWindow window = new CourseViewWindow(this.trainer, ((CardsTrainer)sender).Course);
            window.Show();
        }

        private void ChangeImages()
        {
            if (exploreBtnClicked)
            {
                ExploreImage.Source = new BitmapImage(new Uri("/Resources/explore.png", UriKind.Relative));
                MycoursesImage.Source = new BitmapImage(new Uri("/Resources/mycoursesGrey.png", UriKind.Relative));
            }
            if (mycoursesBtnClicked)
            {
                ExploreImage.Source = new BitmapImage(new Uri("/Resources/exploreGrey.png", UriKind.Relative));
                MycoursesImage.Source = new BitmapImage(new Uri("/Resources/mycourses.png", UriKind.Relative));
            }
            if (achievementsBtnClicked)
            {
                ExploreImage.Source = new BitmapImage(new Uri("/Resources/exploreGrey.png", UriKind.Relative));
                MycoursesImage.Source = new BitmapImage(new Uri("/Resources/mycoursesGrey.png", UriKind.Relative));
            }
            if (gamesBtnClicked)
            {
                ExploreImage.Source = new BitmapImage(new Uri("/Resources/exploreGrey.png", UriKind.Relative));
                MycoursesImage.Source = new BitmapImage(new Uri("/Resources/mycoursesGrey.png", UriKind.Relative));
            }
        }

        private void ExploreBtn_Click(object sender, RoutedEventArgs e)
        {
            exploreBtnClicked = true;
            mycoursesBtnClicked = false;
            achievementsBtnClicked = false;
            gamesBtnClicked = false;
            ExploreBtn.Style = this.Resources["ClickNavigationButtons"] as Style;
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
            ExploreBtn.Style = this.Resources["NavigationButtons"] as Style;
            MyCoursesBtn.Style = this.Resources["NavigationButtons"] as Style;
            ChangeImages();
        }


        private void GamificationButton_Click(object sender, RoutedEventArgs e)
        {
            gamesBtnClicked = true;
            exploreBtnClicked = false;
            mycoursesBtnClicked = false;
            achievementsBtnClicked = false;

            ExploreBtn.Style = this.Resources["NavigationButtons"] as Style;
            MyCoursesBtn.Style = this.Resources["NavigationButtons"] as Style;

            ChangeImages();
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

        private void AddCourseButton_Click(object sender, RoutedEventArgs e)
        {
            AddCourse addCourse = new AddCourse(this.trainer);
            addCourse.ShowDialog();
            var course = addCourse.InsertedCourse;
            CardsTrainer card = new CardsTrainer(course);
            myCoursesCount++;
            if (myCoursesCount % 3 == 0)
            {
                MyCoursesGrid.Height = 300 * (myCoursesCount / 3) + 100;
            }
            else
            {
                MyCoursesGrid.Height = 300 * (myCoursesCount / 3 + 1) + 100;
            }
            MyCoursesGrid.Children.Add(card);
            card.MenuItemEdit.Click += new RoutedEventHandler((sender, e) => EditCourseHandler(sender, e, card));
            card.MenuItemDelete.Click += new RoutedEventHandler((sender, e) => DeleteCourseHandler(sender, e, card));

            card.MouseDoubleClick += new MouseButtonEventHandler(DoubleClickMyCourseHandler);
        }
    }
}
