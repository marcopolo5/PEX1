using ElearningDatabase.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPF_Client.Custom
{
    /// <summary>
    /// Interaction logic for QuizCard.xaml
    /// </summary>
    public partial class QuizCard : UserControl
    {
        public Question Question { set; get; }

        public QuizCard()
        {
            InitializeComponent();
            DataContext = this;
        }
    }
}
