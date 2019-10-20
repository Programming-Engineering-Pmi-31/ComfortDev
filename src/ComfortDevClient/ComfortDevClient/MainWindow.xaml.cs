using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.util;
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

namespace ComfortDevClient {
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();        
        }

        private void button_login_Click(object sender, RoutedEventArgs e)
        {
            if (loginBox.Text.Length < 4 || passwordBox.Password.Length <= 8)
            {
                MessageBox.Show("Your login or password is wrong!");
            }
            else
            {
                var response = Actions.AuthenticateUser(loginBox.Text, passwordBox.Password);
                if ((int)response.StatusCode == 200)
                {
                    MessageBox.Show("Welcome");
                }
                else
                {
                    MessageBox.Show("Connection filed!");
                }
            }
        }

        private void button_regist_Click(object sender, RoutedEventArgs e)
        {

            if (loginBox.Text.Length < 4 || passwordBox.Password.Length <= 8)
            {
                MessageBox.Show("Your login or password is short, please try again!");
            }
            else
            {
                var response = Actions.RegisterUser(loginBox.Text, passwordBox.Password);
                if ((int)response.StatusCode == 200)
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
