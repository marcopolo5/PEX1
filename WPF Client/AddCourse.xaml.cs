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
    /// Interaction logic for AddCourseName.xaml
    /// </summary>
    public partial class AddCourse : Window
    {
        User user;
        public Course InsertedCourse { get; set; }
        public AddCourse(User user)
        {
            InitializeComponent();
            dificultyComboBox.ItemsSource = Enum.GetValues(typeof(DifficultyEnum)).Cast<DifficultyEnum>();
            categoryComboBox.ItemsSource = Enum.GetValues(typeof(CategoryEnum)).Cast<CategoryEnum>();
            this.user = user;
           
        }

        private Course InitializeCourse()
        {
            Course course = new Course();

            ValidateAddedCourse();
            course.Name = CourseNameTxtBox.Text;
            course.Description = CourseDescriptionTxtBox.Text;
            course.Category = (CategoryEnum)categoryComboBox.SelectedItem;
            course.Difficulty = (DifficultyEnum)dificultyComboBox.SelectedItem;
            return course;
            
        }

        public void ValidateAddedCourse()
        {
            if (CourseNameTxtBox.Text.Length <= 0)
            {
                throw new Exception("The name of the course cannot be empty!");
            }
            if (CourseDescriptionTxtBox.Text.Length <= 0)
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
        
        private void SaveCourse_click(object sender, RoutedEventArgs e)
        {
            CourseService courseService = new CourseService();
            try
            {
                var course = InitializeCourse();
                InsertedCourse = courseService.InsertCourse(course, user);
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
