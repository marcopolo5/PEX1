using Elearning.Business;
using ElearningDatabase.Models;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WPF_Client
{
    /// <summary>
    /// Interaction logic for AddCourseUI.xaml
    /// </summary>
    public partial class EditCourseUI : Window
    {
        private List<Lesson> Lessons { set; get; }
        Course course = new Course();
        public EditCourseUI(Course course)
        {
            InitializeComponent();
            ShowLessons();
            dificultyComboBox.ItemsSource = Enum.GetValues(typeof(DifficultyEnum)).Cast<DifficultyEnum>();
            categoryComboBox.ItemsSource = Enum.GetValues(typeof(CategoryEnum)).Cast<CategoryEnum>();
            this.course = course;
            SetTextBoxesCourse();
        }

        private void SetTextBoxesCourse()
        {
            courseNameBox.Text = course.Name;
            courseDescriptionBox.Text = course.Description;
            dificultyComboBox.SelectedItem = course.Difficulty;
            categoryComboBox.SelectedItem = course.Category;
        }

        private void ShowLessons()
        {
            CourseService singleCourse = new CourseService();
            Lessons = singleCourse.GetLessons(this.course.Id);
            DataContext = this; //data binding
        }

        private void AddImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.png;*.jpeg)|*.png;*.jpeg|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                ImageNameTxtBox.Text = File.ReadAllText(openFileDialog.FileName);
            }
                
        }

        private void AddLesson_Click(object sender, RoutedEventArgs e)
        {
            AddLessonUI addLesson = new AddLessonUI(course);
            addLesson.Show();
        }

        private void dificultyComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void CancelCourse_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


        private void lessonListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ICollectionView view = CollectionViewSource.GetDefaultView(lessonListView.ItemsSource);
            view.Refresh();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
