﻿using Elearning.Business;
using ElearningDatabase;
using ElearningDatabase.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

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
        }

        public void GetCourses()
        {
            CourseService singleCourse = new CourseService();
            Lessons = singleCourse.GetLessons(1);
            DataContext = this;
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }
    }
}