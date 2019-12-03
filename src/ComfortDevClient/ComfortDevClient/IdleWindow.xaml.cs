using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ComfortDevClient.ViewModel;
using ComfortDevClient.Pages;
using Logic;

namespace ComfortDevClient
{
    /// <summary>
    /// Interaction logic for IdleWindow.xaml
    /// </summary>
    public partial class IdleWindow : Window
    {
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) { this.DragMove(); }

        public IdlePage idlePage;
        public TaskPage taskPage;
        public TestsPage testsPage;
        public IdleWindow()
        {
            InitializeComponent();
            testsPage = new TestsPage();
            idlePage = new IdlePage();
            taskPage = new TaskPage();

            var testInfo = Actions.GetTestQuestions();


            /// UNTIL NOT RELIZE        testInfo.IsComplated
            if (!testInfo.IsCompleted)   /// HERE
                MyFrame.Navigate(testsPage);
            else
                MyFrame.Navigate(idlePage);

            testsPage.SkipButton.Click += Button_Skip;
            idlePage.task1.Click += Button_To_Task;
            taskPage.backToIdlePageButton.Click += Button_Skip;
            idlePage.toTestAgain.Click += Button_To_Test_Page;

        }

        public void Button_To_Task(object sender, RoutedEventArgs e) { MyFrame.Navigate(taskPage); }
        public void Button_Skip(object sender, RoutedEventArgs e) { MyFrame.Navigate(idlePage); }
        public void Button_To_Test_Page(object sender, RoutedEventArgs e) { MyFrame.Navigate(testsPage); }
        public void Button_Hide(object sender, RoutedEventArgs e) { Hide(); }
        public void Button_Close(object sender, RoutedEventArgs e) { Close(); }
    }
}
