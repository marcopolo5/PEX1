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
    /// Interaction logic for EditQuiz.xaml
    /// </summary>
    public partial class EditQuiz : Window
    {
        public List<Question> Questions { set; get; }
        Quiz Quiz;
        public EditQuiz(Quiz quiz_received)
        {
            InitializeComponent();
            Quiz = quiz_received;
            QuizNameTextBox.Text = Quiz.Name;
            ShowQuestions();

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
            
            Quiz.Name = QuizNameTextBox.Text;
            singleCourse.UpdateQuiz(Quiz);


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
                MessageBox.Show("Plese select a question!");
            }
        }
        }
}
