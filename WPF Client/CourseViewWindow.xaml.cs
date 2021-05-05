using Elearning.Business;
using Elearning.Database.Models;
using ElearningDatabase;
using ElearningDatabase.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using WPF_Client.Custom;

namespace WPF_Client
{
    /// <summary>
    /// Interaction logic for CourseViewWindow.xaml
    /// </summary>
    public partial class CourseViewWindow : Window
    {
        private ElearningContext db;

        public List<Lesson> Lessons { get; set; }
        public List<Quiz> Quizes { get; set; }
        public List<Review> Reviews { get; set; }

        public LessonService LessonService;
        public Lesson lesson;
        public Quiz quiz;
        public User user;
        public Course course;
        public Resource resource;
        public void GetConnection()
        {
            var dbCOntext = new DbContextOptionsBuilder<ElearningContext>();
            dbCOntext.UseSqlServer(Elearning.Database.ResourceFile.connectionString);
            db = new ElearningContext(dbCOntext.Options);
            db.Migrate();
        }

        public CourseViewWindow(User user, Course course)
        {
            InitializeComponent();
            this.course = course;
            this.user = user;
            GetCourses();
            ShowReviews();
            GetConnection();
            LessonService = new LessonService();
            lesson = new Lesson();
            quiz = new Quiz();
            resource = new Resource();
        }

        public void GetCourses()
        {
            CourseService singleCourse = new CourseService();
            Lessons = singleCourse.GetLessons(this.course.Id);
            Quizes = singleCourse.GetQuizzes(this.course.Id);
            DataContext = this; //data binding
        }

        private void ListViewItem_MouseButtonDown(object sender, MouseButtonEventArgs e)
        {
            WebBrowser.Visibility = Visibility.Visible;
            QuizesGrid.Visibility = Visibility.Hidden;
            lesson = (Lesson)LessonsListView.SelectedItem;
            Uri uri = new Uri(lesson.Content);
            WebBrowser.Source = uri;
        }

        public void ShowReviews()
        {
            CourseService singleCourse = new CourseService();
            Reviews = singleCourse.GetReviews(this.course.Id);
            DataContext = this; //data binding
        }


        private void AddReviewButton_Click(object sender, RoutedEventArgs e)
        {
            CourseService singleCourse = new CourseService();
            var review = ReviewTxtBox.Text;
            Review r = new Review(user.Id, course.Id, review);
            singleCourse.InsertReview(user, course.Id, review);
            Reviews.Add(r);
            ICollectionView view = CollectionViewSource.GetDefaultView(ReviewListView.ItemsSource);
            view.Refresh();
            ReviewTxtBox.Clear();
        }

        private void DownloadButton_Click(object sender, RoutedEventArgs e)
        {
            lesson = (Lesson)LessonsListView.SelectedItem;
            resource.File = LessonService.GetResourceFile(lesson.ResourceId);
            Uri uri = new Uri(resource.File);
            var processStartInfo = new ProcessStartInfo
            {
                FileName = uri.ToString(),
                UseShellExecute = true
            };
            Process.Start(processStartInfo);
        }

        private void HomeButton_MouseEnter(object sender, MouseEventArgs e)
        {
            HomeImage.Source = new BitmapImage(new Uri("/Resources/homeBlue.png", UriKind.Relative));
        }

        private void HomeButton_MouseLeave(object sender, MouseEventArgs e)
        {
            HomeImage.Source = new BitmapImage(new Uri("/Resources/homeGrey.png", UriKind.Relative));
        }

        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.user.Role.ToString().Equals("Student"))
            {
                MainCoursesView dashboard = new MainCoursesView(user);
                dashboard.Show();
            }
            else if (this.user.Role.ToString().Equals("Trainer"))
            {
                MainTrainerView dashboard = new MainTrainerView(user);
                dashboard.Show();
            }
            this.Close();
        }

        private void ShowQuestions(Quiz quiz)
        {
            CourseService courseService = new CourseService();
            var quizes = courseService.GetQuestions(quiz.Id);
            int countQuestions = 0;
            foreach(var question in quizes)
            {
                countQuestions++;
                QuizCard quizCard = new QuizCard();
                quizCard.Question = question;
                quizCard.Question.QuestionText= countQuestions.ToString() + ". " + question.QuestionText;
                //quizCard.Question.Answer1 = question.Answer1;
                //quizCard.Answer2 = question.Answer2;
                //quizCard.Answer3 = question.Answer3;
                //quizCard.Answer4 = question.Answer4;
                QuizesUniformGrid.Children.Add(quizCard);

            }
        }

        private void QuizesListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            QuizesGrid.Visibility = Visibility.Visible;
            quiz = (Quiz)QuizesListView.SelectedItem;
            WebBrowser.Visibility = Visibility.Hidden;

            QuizesUniformGrid.Children.Clear();
            ShowQuestions(quiz);
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            int score = 0;
            foreach(QuizCard item in QuizesUniformGrid.Children)
            {
                int selectedAnswer = 0;

                for (int i = 1; i <= item.radioButtons.Children.Count; i++)
                {
                    var currentButton = (RadioButton)(item.radioButtons.Children[i - 1]);

                    if (currentButton.IsChecked== true)
                    {
                        selectedAnswer = i;
                        currentButton.Foreground = Brushes.Red;
                        ((RadioButton)(item.radioButtons.Children[item.Question.CorrectAnswer - 1])).Foreground=Brushes.Green;
                        if (selectedAnswer == item.Question.CorrectAnswer)
                        {
                            score += 10;
                        }

                    }
                    currentButton.IsEnabled = false;

                }
            }

            MessageBox.Show(score.ToString() + "/" + QuizesUniformGrid.Children.Count * 10);
        }
    }
}
