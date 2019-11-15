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

namespace ComfortDevClient
{
    /// <summary>
    /// Interaction logic for IdleWindow.xaml
    /// </summary>
    public partial class IdleWindow : Window
    {
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) { this.DragMove(); }

        public bool testIsDone;

        public IdlePage idlePage;
        public IdleWindow()
        {
            InitializeComponent();
            TestsPage testsPage = new TestsPage();
            idlePage = new IdlePage();
            testIsDone = false;

            if (!testIsDone)
                MyFrame.Navigate(testsPage);
            else
                MyFrame.Navigate(idlePage);

            testsPage.SkipButton.Click += Button_Skip;
        }

        public void Button_Skip(object sender, RoutedEventArgs e) { MyFrame.Navigate(idlePage); }
        public void Button_Hide(object sender, RoutedEventArgs e) { Hide(); }
        public void Button_Close(object sender, RoutedEventArgs e) { Close(); }
    }
}
