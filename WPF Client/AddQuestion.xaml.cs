using Elearning.Business;
using ElearningDatabase.Models;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for AddQuestion.xaml
    /// </summary>
    public partial class AddQuestion : Window
    {
        public Quiz Quiz { set; get; }
        public Question Question { set; get; }

        string[] options = { "1", "2", "3", "4" };

        public CourseService courseService;

        public AddQuestion(Quiz quiz)
        {
            InitializeComponent();
            courseService = new CourseService();
            CorrectAnswerComboBox.ItemsSource = options;
            Question = new Question();
            Quiz = quiz;
        }

        private void ValidateQuestion()
        {
            if (QuestionNameTxtBox.Text.Length <= 0)
            {
                throw new Exception("The question text cannot be empty!");
            }
            if (Answer1TxtBox.Text.Length <= 0 || Answer2TxtBox.Text.Length <= 0 || Answer3TxtBox.Text.Length <= 0 || Answer4TxtBox.Text.Length <= 0)
            {
                throw new Exception("The answers cannot be empty!");
            }
            if (CorrectAnswerComboBox.SelectedItem == null)
            {
                throw new Exception("Please select a correct answer!");
            }
        }

        private void SaveQuestionButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ValidateQuestion();
                Question.QuestionText = QuestionNameTxtBox.Text;
                Question.Answer1 = Answer1TxtBox.Text;
                Question.Answer2 = Answer2TxtBox.Text;
                Question.Answer3 = Answer3TxtBox.Text;
                Question.Answer4 = Answer4TxtBox.Text;
                Question.CorrectAnswer = Convert.ToInt32(CorrectAnswerComboBox.SelectedItem);

                courseService.InsertQuestion(Quiz.Id, Question);
                this.Close();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
            

        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
