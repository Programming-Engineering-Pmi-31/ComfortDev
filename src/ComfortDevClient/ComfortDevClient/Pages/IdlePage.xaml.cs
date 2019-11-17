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
using ComfortDevClient.ViewModel;

namespace ComfortDevClient.Pages
{
    /// <summary>
    /// Interaction logic for IdlePage.xaml
    /// </summary>
    public partial class IdlePage : Page
    {
        public IdlePage()
        {
            InitializeComponent();
            DataContext = new IdleScript();
        }
    }
}
