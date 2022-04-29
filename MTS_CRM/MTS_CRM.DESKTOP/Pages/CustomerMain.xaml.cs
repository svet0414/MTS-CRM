using System.Windows;
using System.Windows.Controls;

namespace MTS_CRM.DESKTOP.Pages
{
    /// <summary>
    /// Interaction logic for Customer.xaml
    /// </summary>
    public partial class CustomerMain : Page
    {
        public CustomerMain()
        {
            InitializeComponent();
            Loaded += MyWindow_Loaded;
        }
        //Is loading the default Page - the customer list
        private void MyWindow_Loaded(object sender, RoutedEventArgs e)
        {
            customerPageFrame.NavigationService.Navigate(new CustomersList());
        }

        //Creating Customer page.  this is the code used to navigate between Pages in a Frame. Frame is an object from the toolbox of WPF
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            customerPageFrame.NavigationService.Navigate(new AddCustomer());
        }

        //List of Customers Page
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            customerPageFrame.NavigationService.Navigate(new CustomersList());
        }
    }
}
