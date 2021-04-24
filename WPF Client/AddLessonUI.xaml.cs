using Elearning.Business;
using ElearningDatabase;
using ElearningDatabase.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
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
        public List<Resource> Resources { get; set; }

        public Lesson lesson;
        public AddLessonUI()
        {
            InitializeComponent();
            GetConnection();
            lesson = new Lesson();
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
                AddResourceTxtBox.Text = File.ReadAllText(openFileDialog.FileName);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }


        private void SaveLessonButton_Click(object sender, RoutedEventArgs e)
        {
            //CourseService singleCourse = new CourseService();
            //var lessonName = LessonNameTxtBox.Text;
            //var lessonDescription = LessonDescriptionTxtBox.Text;
            //var lessonResource = AddResourceTxtBox.Text;
            //Lesson lesson = new Lesson();
            //lesson.Name = lessonName;
            //lesson.Content = lessonDescription;
            
        }
    }
}
