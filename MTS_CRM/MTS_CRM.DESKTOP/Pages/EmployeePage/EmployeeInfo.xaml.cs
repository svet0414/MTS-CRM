using MTS_CRM.DB;
using System.Threading;
using System.Windows;
using System.Windows.Controls;

namespace MTS_CRM.DESKTOP.Pages.EmployeePage
{
    /// <summary>
    /// Interaction logic for EmployeeInfo.xaml
    /// </summary>
    public partial class EmployeeInfo : Page
    {
        static EmployeeService.IEmployeeService employeeService = new EmployeeService.EmployeeServiceClient();
        Employee employee;
        public EmployeeInfo(Employee employee)
        {
            this.employee = employee;
            InitializeComponent();
            empLNameLBL.Content = employee.EmployeeLName;
            empFNameLBL.Content = employee.EmployeeFName;
            empEmailLBL.Content = employee.EmployeeEmail;
            empPhoneNumberLBL.Content = employee.EmployeePhoneNumber;
            empPositionLBL.Content = employee.Position;
            empUsernameLBL.Content = employee.Username;
            empAdminLBL.Content = employee.IsAdmin;
        }

        //Remove Employee Button
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            loadingLBL.Visibility = Visibility.Visible;
            Thread th = new Thread(t =>
            {
               
                employeeService.RemoveEmployee(employee);
                MessageBox.Show("Employee removed");
                
                this.Dispatcher.Invoke(() =>
                {
                    loadingLBL.Visibility = Visibility.Hidden;
                    //returning back to the CustomersList page
                    this.NavigationService.Navigate(new EmployeePage.EmployeeList());
                });

            })
            { IsBackground = true };
            th.Start();
            
        }

        //Navigate to Update/Edit Employee page
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new EmployeePage.EmployeeEdit(employee));
        }
    }
}
