﻿using Elearning.Business;
using ElearningDatabase;
using ElearningDatabase.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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
        public List<Resource> Resources { get; set; }

        public LessonService LessonService;
        public Lesson lesson;

        public string content = " ";

        public void GetConnection()
        {
            var dbCOntext = new DbContextOptionsBuilder<ElearningContext>();
            dbCOntext.UseSqlServer(Elearning.Database.ResourceFile.connectionString);
            db = new ElearningContext(dbCOntext.Options);
            db.Migrate();
            

        }

        public CourseViewWindow()
        {
            InitializeComponent();
            GetCourses();
            LessonService = new LessonService();
            lesson = new Lesson();
        }

        public void GetCourses()
        {
            CourseService singleCourse = new CourseService();
            Lessons = singleCourse.GetLessons(1);
            DataContext = this; //data binding
        }


        private void ListViewItem_MouseButtonDown(object sender, MouseButtonEventArgs e)
        {
            lesson = (Lesson)LessonsListView.SelectedItem;
            Uri uri = new Uri(lesson.Content);
            WebBrowser.Source = uri;

        }
    }
}