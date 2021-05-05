using Elearning.Business;
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

namespace WPF_Client
{
    /// <summary>
    /// Interaction logic for AddQuiz.xaml
    /// </summary>
    public partial class AddQuiz : Window
    {
        public List<Question> Questions { set; get; }
        Course course;
        Quiz Quiz;
        public AddQuiz()
        {
            InitializeComponent();
            course = new Course();
            Quiz = new Quiz();
            Quiz.CourseId = 4;
            ShowQuestions();
            AddQuestionButton.IsEnabled = false;
            RemoveQuestionButton.IsEnabled = false;

        }

        private void AddQuestionButton_Click(object sender, RoutedEventArgs e)
        {
            AddQuestion addQuestion = new AddQuestion(Quiz);
            addQuestion.ShowDialog();

            ShowQuestions();
        }

        private void CancelQuizButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void SaveQuizButton_Click(object sender, RoutedEventArgs e)
        {
            
            CourseService singleCourse = new CourseService();
            Quiz quiz = new Quiz();
            quiz = Quiz;
            quiz.Name = QuizNameTextBox.Text;
            Quiz = singleCourse.InsertQuiz(quiz);

            AddQuestionButton.IsEnabled = true;
            RemoveQuestionButton.IsEnabled = true;
            SaveQuizButton.IsEnabled = false;
            CancelQuizButton.IsEnabled = false;

        }

        private void ShowQuestions()
        {
            CourseService singleCourse = new CourseService();
            Questions = singleCourse.GetQuestions(this.Quiz.Id);
            DataContext = this; //data binding

            questionsListView.ItemsSource = Questions;
            ICollectionView view = CollectionViewSource.GetDefaultView(questionsListView.ItemsSource);
            view.Refresh();
        }

        private void RemoveQuestionButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CourseService courseService = new CourseService();
                courseService.RemoveQuestion((Question)questionsListView.SelectedItem);
                ShowQuestions();
            }
            catch
            {
                MessageBox.Show("Plese select a quiz");
            }
        }
    }
}
