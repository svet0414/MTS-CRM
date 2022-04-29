using System.Windows;
using System.Windows.Controls;

namespace MTS_CRM.DESKTOP.Pages.CustomerPC
{
    /// <summary>
    /// Interaction logic for EmployeeMain.xaml
    /// </summary>
    public partial class EmployeeMain : Page
    {
        public EmployeeMain()
        {
            InitializeComponent();
            Loaded += MyWindow_Loaded;
        }

        //Is loading the default Page
        private void MyWindow_Loaded(object sender, RoutedEventArgs e)
        {
            employeeMainFrame.NavigationService.Navigate(new EmployeePage.EmployeeList());
        }

        //List of Employees Page . this is the code used to navigate between Pages in a Frame. Frame is an object from the toolbox of WPF
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            employeeMainFrame.NavigationService.Navigate(new EmployeePage.EmployeeList());
        }

        //Create Employee Page
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            employeeMainFrame.NavigationService.Navigate(new EmployeePage.EmployeeCreate());
        }
    }
}
