using Elearning.Business;
using Elearning.Database.Models;
using ElearningDatabase;
using ElearningDatabase.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WPF_Client
{
    /// <summary>
    /// Interaction logic for CourseViewWindow.xaml
    /// </summary>
    public partial class CourseViewWindow : Window
    {
        private ElearningContext db;

        public List<Lesson> Lessons { get; set; }
        public List<Review> Reviews { get; set; }

        public LessonService LessonService;
        public Lesson lesson;
        public User user;
        public Course course;
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
            GetConnection();
            ShowReviews();
            GetConnection();
            LessonService = new LessonService();
            lesson = new Lesson();
           
        }

        public void GetCourses()
        {
            CourseService singleCourse = new CourseService();
            Lessons = singleCourse.GetLessons(this.course.Id);
            DataContext = this; //data binding
        }


        private void ListViewItem_MouseButtonDown(object sender, MouseButtonEventArgs e)
        {
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

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void AddReviewButton_Click(object sender, RoutedEventArgs e)
        {
            CourseService singleCourse = new CourseService();
            var review = ReviewTxtBox.Text;
            singleCourse.InsertReview(user, course.Id, review);
            

        }

        private void LessonsListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void downloadButton_Click(object sender, RoutedEventArgs e)
        {
            lesson = (Lesson)LessonsListView.SelectedItem;
            Uri uri = new Uri(lesson.Content);
            var processStartInfo = new ProcessStartInfo
            {
                FileName = uri.ToString(),
                UseShellExecute = true
            };
            Process.Start(processStartInfo);
        }
    }
}