﻿using Elearning.Business;
using ElearningDatabase.Models;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Interaction logic for AddCourseUI.xaml
    /// </summary>
    public partial class AddCourseUI : Window
    {
        public AddCourseUI()
        {
            InitializeComponent();
            dificultyComboBox.ItemsSource = Enum.GetValues(typeof(DifficultyEnum)).Cast<DifficultyEnum>();
            categoryComboBox.ItemsSource = Enum.GetValues(typeof(CategoryEnum)).Cast<CategoryEnum>();
        }


        private void AddImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.png;*.jpeg)|*.png;*.jpeg|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                ImageNameTxtBox.Text = File.ReadAllText(openFileDialog.FileName);
            }
                
        }

        private void AddLesson_Click(object sender, RoutedEventArgs e)
        {

        }

        private void dificultyComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
