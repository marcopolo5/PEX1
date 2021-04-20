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
    /// Interaction logic for Cards.xaml
    /// </summary>
    public partial class Cards : UserControl
    {
        public Cards()
        {
            InitializeComponent();
            this.DataContext = this;
            CourseDescription.Visibility = Visibility.Hidden;
        }

        public string Author { get; set; }
        public string Course { get; set; }
        public string Description { get; set; }

        public string Category { get; set; }

        public string Difficulty { get; set; }

        public int CourseId { get; set; }

        public int UserId { get; set; }

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
            CourseName.Style = this.Resources["ClickCourseTextTemplate"] as Style;
            AuthorName.Style = this.Resources["ClickAuthorTextTemplate"] as Style;
            CourseDescription.Visibility = Visibility.Visible;
            CourseName.Visibility = Visibility.Hidden;
            AuthorName.Visibility = Visibility.Hidden;
            CourseImage.Visibility = Visibility.Hidden;
        }

        private void Button_MouseLeave(object sender, MouseEventArgs e)
        {
            CourseName.Style = this.Resources["CourseTextTemplate"] as Style;
            AuthorName.Style = this.Resources["AuthorTextTemplate"] as Style;
            CourseDescription.Visibility = Visibility.Hidden;
            CourseName.Visibility = Visibility.Visible;
            AuthorName.Visibility = Visibility.Visible;
            CourseImage.Visibility = Visibility.Visible;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            CourseViewWindow window = new CourseViewWindow();
            
            window.Show();
        }
    }
}
