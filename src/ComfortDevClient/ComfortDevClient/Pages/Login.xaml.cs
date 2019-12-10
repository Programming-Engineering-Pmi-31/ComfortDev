using System;
using System.Collections.Generic;
using System.Net;
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
using ComfortDevClient.ViewModel;
using GalaSoft.MvvmLight.Command;
using Logic;

namespace ComfortDevClient.Pages
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Page
    {
        public Login()
        {
            InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            IdleWindow wind = new IdleWindow();

            if (loginBox.Text.Length < 4 || passwordBox.Password.Length <= 8)
            {
                MessageBox.Show("Your login or password is wrong!");
            }
            else
            {
                var response = await Actions.AuthenticateUser(loginBox.Text, passwordBox.Password);
                if (response.StatusCode == HttpStatusCode.OK)
                {   
                    wind.Show();
                }
                else
                {
                    MessageBox.Show("Connection filed!");
                }
            }
        }
    }
}
