using Glimpse.AspNet.Tab;
using MTS_CRM.DESKTOP.Pages;
using System;
using System.Data.SqlClient;
using System.Windows;

namespace MTS_CRM.DESKTOP
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Loaded += MyWindow_Loaded;
        }
        private void MyWindow_Loaded(object sender, RoutedEventArgs e)
        {
            frame.NavigationService.Navigate(new Home());
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            frame.NavigationService.Navigate(new Pages.CustomerPC.EmployeeMain());
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            frame.NavigationService.Navigate(new Home());
        }

        private void CustomerButton_Click(object sender, RoutedEventArgs e)
        {
            frame.NavigationService.Navigate(new CustomerMain());
        }

        private void ChatButton_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://localhost:53398/");
               
        }
    }
}
