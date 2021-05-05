using ElearningDatabase.Models;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPF_Client.Custom
{
    /// <summary>
    /// Interaction logic for CardsTrainer.xaml
    /// </summary>
    public partial class CardsTrainer : UserControl
    {
        public CardsTrainer(Course course)
        {
            InitializeComponent();
            this.DataContext = this;
            CourseDescription.Visibility = Visibility.Hidden;
            this.Course = course;
        }

        //public string Author { get; set; }
        //public string CourseName { get; set; }
        //public string Description { get; set; }

        //public string Category { get; set; }

        //public string Difficulty { get; set; }

        public Course Course { get; set; }


        public ImageSource Source
        {
            get { return (ImageSource)GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Source.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SourceProperty =
            DependencyProperty.Register("Source", typeof(ImageSource), typeof(Cards));

        private void Button_MouseEnter(object sender, MouseEventArgs e)
        {
            CourseNameTxtBlock.Style = this.Resources["ClickCourseTextTemplate"] as Style;
            AuthorName.Style = this.Resources["ClickAuthorTextTemplate"] as Style;
            CourseDescription.Visibility = Visibility.Visible;
            CourseNameTxtBlock.Visibility = Visibility.Hidden;
            AuthorName.Visibility = Visibility.Hidden;
            CourseImage.Visibility = Visibility.Hidden;
            PointsImg.Source = new BitmapImage(new Uri("/Resources/3PUNCTEGrey.png", UriKind.Relative));
        }

        private void Button_MouseLeave(object sender, MouseEventArgs e)
        {
            CourseNameTxtBlock.Style = this.Resources["CourseTextTemplate"] as Style;
            AuthorName.Style = this.Resources["AuthorTextTemplate"] as Style;
            CourseDescription.Visibility = Visibility.Hidden;
            CourseNameTxtBlock.Visibility = Visibility.Visible;
            AuthorName.Visibility = Visibility.Visible;
            CourseImage.Visibility = Visibility.Visible;
            PointsImg.Source = new BitmapImage(new Uri("/Resources/3PUNCTEBlue.png", UriKind.Relative));
        }

        private void PointsImg_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Cursor = Cursors.Hand;
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
