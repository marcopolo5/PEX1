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
        public Course Course { get; set; }
        public List<Lesson> Lessons { set; get; }
        Course course = new Course();
        public List<Quiz> Quizes { set; get; }
        CourseService courseService;
        public EditCourseUI(Course course)
        {
            InitializeComponent();
            courseService = new CourseService();
            this.course = course;
            Course = this.course;
            DataContext = this;

            dificultyComboBox.ItemsSource = Enum.GetValues(typeof(DifficultyEnum)).Cast<DifficultyEnum>();
            categoryComboBox.ItemsSource = Enum.GetValues(typeof(CategoryEnum)).Cast<CategoryEnum>();
            
            SetTextBoxesCourse();
            ShowLessons();
            ShowQuizes();
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
           
            lessonListView.ItemsSource = Lessons;
            ICollectionView view = CollectionViewSource.GetDefaultView(lessonListView.ItemsSource);
            view.Refresh();
        }


        private void AddLesson_Click(object sender, RoutedEventArgs e)
        {
            AddLessonUI addLesson = new AddLessonUI(course);
            addLesson.ShowDialog();
            ShowLessons();
            

        }

        private void CancelCourse_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ValidateCourse()
        {
            if (courseNameBox.Text.Length <= 0)
            {
                throw new Exception("The name of the course cannot be empty!");
            }
            if (courseDescriptionBox.Text.Length <= 0)
            {
                throw new Exception("The description of the course cannot be empty!");
            }
            if (categoryComboBox.SelectedItem == null)
            {
                throw new Exception("Please select a category!");
            }
            if (dificultyComboBox.SelectedItem == null)
            {
                throw new Exception("Please select a difficulty!");
            }
        }
        public void GetUpdatedCourse()
        {
            course.Name = courseNameBox.Text;
            course.Description = courseDescriptionBox.Text;
            course.Category = (CategoryEnum)categoryComboBox.SelectedItem;
            course.Difficulty = (DifficultyEnum)dificultyComboBox.SelectedItem;

            Course = course;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ValidateCourse();
                GetUpdatedCourse();
                courseService.UpdateCourse(course);
                this.Close();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);

            }
        }
        private void ShowQuizes()
        {
            CourseService singleCourse = new CourseService();
            Quizes = singleCourse.GetQuizzes(this.course.Id);
            
            quizesListView.ItemsSource = Quizes;
            ICollectionView view = CollectionViewSource.GetDefaultView(quizesListView.ItemsSource);
            view.Refresh();
        }
        private void RemoveLessonButton_Click(object sender, RoutedEventArgs e)
        {
            Lesson selectedLesson = (Lesson)lessonListView.SelectedItem;
            if (selectedLesson != null)
            {
                courseService.RemoveLesson(selectedLesson);
                ShowLessons();
            }
            else
            {
                MessageBox.Show("Please select a lesson!");
            }
            
        }

        private void AddQuizButton_Click(object sender, RoutedEventArgs e)
        {
            AddQuiz addQuizWindow = new AddQuiz(this.Course);
            addQuizWindow.ShowDialog();
            ShowQuizes();
        }

        private void RemoveQuizButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                courseService.RemoveQuiz((Quiz)quizesListView.SelectedItem);
                ShowQuizes();
            }
            catch
            {
                MessageBox.Show("Please select a quiz!");
            }
        }

        private void EditQuizButton_Click(object sender, RoutedEventArgs e)
        {
            Quiz selectedQuiz = (Quiz)quizesListView.SelectedItem;
            if (selectedQuiz != null)
            {
                EditQuiz editQuizWindow = new EditQuiz(selectedQuiz);
                editQuizWindow.ShowDialog();
                ShowQuizes();
            }
            else
            {
                MessageBox.Show("Please select a quiz!");

            }
        }
    }
}
