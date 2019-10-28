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
using Logic;

namespace ComfortDevClient.Pages
{
    /// <summary>
    /// Interaction logic for Registration.xaml
    /// </summary>
    public partial class Registration : Page
    {
        public Registration()
        {
            InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            if (loginBox.Text.Length < 4 || passwordBox1.Password.Length <= 8)
            {
                MessageBox.Show("Your login or password is short, please try again!");
            }
            else if (passwordBox1.Password != passwordBox2.Password)
            {
                MessageBox.Show("Passwords are not the same!");
            }
            else
            {
                var response = await Actions.RegisterUser(loginBox.Text, passwordBox1.Password);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    MessageBox.Show("Register successful!");
                }
                else
                {
                    MessageBox.Show("Connection filed!");
                }
            }
        }
    }
}
