using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Logic.Classes;
using ComfortDevClient.ViewModel;
using ComfortDevClient.Pages;
using GalaSoft.MvvmLight.Command;
using System.Threading.Tasks;
using System.Threading;
using Logic;

namespace ComfortDevClient.Pages
{
    /// <summary>
    /// Interaction logic for TestsPage.xaml
    /// </summary>
    public partial class TestsPage : Page
    {
        public TestsPage()
        {
            InitializeComponent();            
            Working();
        }

        private async void Working()
        {
            //var questions = await Actions.GetTestQuestions();

            //foreach (var quest in questions.GetEnumerator().Current.QuestionText)
            //    foreach (var i in questions.GetEnumerator().Current.Answers)
            //    {
            //        blockOfAnswers.Children.Add(new RadioButton { Content = i.AnswerText });
            //    }



        }

        private void Next_button_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void Previous_button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
