using MTS_CRM.CONTROLLERS;
using MTS_CRM.DB;
using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MTS_CRM.DESKTOP.Pages.EmployeePage
{
    /// <summary>
    /// Interaction logic for EmployeeList.xaml
    /// </summary>
    public partial class EmployeeList : Page
    {
        DataTable dt;
        //EmployeeController empCtrl = EmployeeController.GetInstance();
        static EmployeeService.IEmployeeService employeeService = new EmployeeService.EmployeeServiceClient();
        public EmployeeList()
        {
            dt = new DataTable();
            InitializeComponent();
            dt.Clear();
        }

        // this method is used to fill the "DataGrid" table with data from database
        private void fillingTable()
        {

            //var employees = empCtrl.GetAllEmployeesByFirstName(getEmployeeByNameTXT.Text);
            var employees = employeeService.GetAllEmployeesByFirstName(getEmployeeByNameTXT.Text);
            //Creating each row
            foreach (Employee employee in employees)
            {

                String firstname1 = employee.EmployeeFName, lastname1 = employee.EmployeeLName, phonenumber1 = employee.EmployeePhoneNumber;
                int? id1 = employee.EmpID;

                DataRow firstRow = dt.NewRow();
                firstRow[0] = id1;
                firstRow[1] = firstname1;
                firstRow[2] = lastname1;
                firstRow[3] = phonenumber1;
                dt.Rows.Add(firstRow);
            }


            employeeListData.ItemsSource = dt.DefaultView;
        }

        //this is used to create the columns for DataGrid
        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {

            DataColumn id = new DataColumn("ID", typeof(int));
            DataColumn firstname = new DataColumn("First Name", typeof(string));
            DataColumn lastname = new DataColumn("Last Name", typeof(string));
            DataColumn phone = new DataColumn("Phone Number", typeof(string));

            dt.Columns.Add("ID");
            dt.Columns.Add("First Name");
            dt.Columns.Add("Last Name");
            dt.Columns.Add("Phone Number");
            employeeListData.ItemsSource = dt.DefaultView;
        }

        //Button used to search for employees
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            dt.Clear();
            this.fillingTable();
        }

        //method used when is double clicked a row from DataGrid
        private void employeeListData_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // var employees = empCtrl.GetAllEmployeesByFirstName(getEmployeeByNameTXT.Text);
            var employees = employeeService.GetAllEmployeesByFirstName(getEmployeeByNameTXT.Text);
            var currentIndex = employeeListData.Items.IndexOf(employeeListData.CurrentItem);
            //MessageBox.Show(employees[currentIndex].EmployeeFName);
            this.NavigationService.Navigate(new EmployeePage.EmployeeInfo(employees[currentIndex]));
        }


    }
}
