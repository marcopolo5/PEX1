using Elearning.Business;
using ElearningDatabase;
using ElearningDatabase.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
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
    /// Interaction logic for AddLessonUI.xaml
    /// </summary>
    public partial class AddLessonUI : Window
    {
        private ElearningContext db;
        public List<Lesson> Lessons { get; set; }
        public List<Resource> Resourcess { get; set; }

        public Course course;

        public AddLessonUI(Course course)
        {
            InitializeComponent();
            GetConnection();
            Resourcess = new List<Resource>();
            this.course = course;
        }

        public void GetConnection()
        {
            var dbCOntext = new DbContextOptionsBuilder<ElearningContext>();
            dbCOntext.UseSqlServer(Elearning.Database.ResourceFile.connectionString);
            db = new ElearningContext(dbCOntext.Options);
            db.Migrate();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void AddResourceButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = @"PDF Files (*.pdf)|*.pdf";
            if (openFileDialog.ShowDialog() == true)
            {
                AddResourceButton.DataContext = File.ReadAllText(openFileDialog.FileName);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
        }

        private void ValidateLesson()
        {
            if(LessonNameTxtBox.Text.Length <= 0)
            {
                throw new Exception("Lesson name cannot be empty!");
            }
            if (LessonContentTxtBox.Text.Length <= 0)
            {
                throw new Exception("Lesson content cannot be empty!");
            }
            //if (AddResourceButton.DataContext. <= 0)
            //{
            //    throw new Exception("Resource cannot be empty!");
            //}
        }

        private void SaveLessonButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ValidateLesson();
                Lesson lesson = new Lesson();
                CourseService singleCourse = new CourseService();
                lesson.Name = LessonNameTxtBox.Text;
                lesson.Content = LessonContentTxtBox.Text;
                Resource resource = new Resource();
                resource.File = (string)AddResourceButton.DataContext;
                lesson.Resource = resource;

                singleCourse.InsertLesson(lesson, course.Id);
                this.Close();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
            
        }
    }
}