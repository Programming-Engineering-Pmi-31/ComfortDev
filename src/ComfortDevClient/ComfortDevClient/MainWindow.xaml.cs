using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using ComfortDevClient.Pages;
using GalaSoft.MvvmLight.Command;

namespace ComfortDevClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) { this.DragMove(); }
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new ViewModel.MainViewModel();
        }
        private void Button_Hide(object sender, RoutedEventArgs e) { Hide(); }
        private void Button_Close(object sender, RoutedEventArgs e) { Close(); }
        private void Btn1_Click(object sender, RoutedEventArgs e)
        {
            if (Btn1.Content.ToString() == "Sign Up")
                Btn1.Content = "Log In";
            else
                Btn1.Content = "Sign Up";
        }
    }
}
