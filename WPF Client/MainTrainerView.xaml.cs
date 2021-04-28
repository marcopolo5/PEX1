using System;
using System.Collections.Generic;
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
        public MainTrainerView()
        {
            InitializeComponent();
            ExploreBtn.Style = this.Resources["ClickNavigationButtons"] as Style;
            ExploreCourses.Visibility = Visibility.Visible;
            MyCourses.Visibility = Visibility.Hidden;
            CardsTrainer mycard = new CardsTrainer();
            ExploreCoursesGrid.Children.Add(mycard);
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
    }
}
