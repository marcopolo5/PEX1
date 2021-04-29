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
        CourseService courseService;
        public EditCourseUI(Course course)
        {
            InitializeComponent();
            courseService = new CourseService();
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


        private void AddLesson_Click(object sender, RoutedEventArgs e)
        {
            AddLessonUI addLesson = new AddLessonUI(course);
            addLesson.ShowDialog();
            ShowLessons();
            lessonListView.ItemsSource = Lessons;
            ICollectionView view = CollectionViewSource.GetDefaultView(lessonListView.ItemsSource);
            view.Refresh();

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
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void GetUpdatedCourse()
        {
            course.Name = courseNameBox.Text;
            course.Description = courseDescriptionBox.Text;
            course.Category = (CategoryEnum)categoryComboBox.SelectedItem;
            course.Difficulty = (DifficultyEnum)dificultyComboBox.SelectedItem;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            GetUpdatedCourse();
            courseService.UpdateCourse(course);
            this.Close();
        }

        private void RemoveLessonButton_Click(object sender, RoutedEventArgs e)
        {
            courseService.RemoveLesson((Lesson)lessonListView.SelectedItem);
            ShowLessons();
            lessonListView.ItemsSource = Lessons;
            ICollectionView view = CollectionViewSource.GetDefaultView(lessonListView.ItemsSource);
            view.Refresh();
        }
    }
}
