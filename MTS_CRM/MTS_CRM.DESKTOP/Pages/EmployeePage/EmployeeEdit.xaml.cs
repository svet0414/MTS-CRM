//using MTS_CRM.CONTROLLERS;
using MTS_CRM.CONTROLLERS;
using MTS_CRM.DB;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MTS_CRM.DESKTOP.Pages.EmployeePage
{
    /// <summary>
    /// Interaction logic for EmployeeEdit.xaml
    /// </summary>
    public partial class EmployeeEdit : Page
    {
        //new version with service
        static EmployeeService.IEmployeeService employeeService = new EmployeeService.EmployeeServiceClient();
        MODEL.EmailCheck check = new MODEL.EmailCheck();

        Employee employeeIn;


        public EmployeeEdit(Employee employee)
        {

            this.employeeIn = employee;
            InitializeComponent();

            emailTXT.Text = employee.EmployeeEmail;
            lNameTXT.Text = employee.EmployeeLName;
            fNameTXT.Text = employee.EmployeeFName;
            positionTXT.Text = employee.Position;
            usernameTXT.Text = employee.Username;
            passwordTXT.Password = employee.Password;
            phoneNoTXT.Text = employee.EmployeePhoneNumber;

            DateTime? dat = employee.DateOfBirth;
            dateBirthTXT.SelectedDate = dat;

            if (employee.IsAdmin == true)
            {
                adminRadio.IsChecked = true;
            }
            else
                adminRadio.IsChecked = false;


        }

        //Update Employee Button
        private void Button_Click(object sender, RoutedEventArgs e)
        {

            //----- Adding the new informations for Employee
            Employee empNew = new Employee
            {
                EmployeeFName = fNameTXT.Text,
                EmployeeLName = lNameTXT.Text,
                EmployeePhoneNumber = phoneNoTXT.Text,
                Position = positionTXT.Text,
                Username = usernameTXT.Text
                
            };
            DateTime? dat = dateBirthTXT.SelectedDate.Value;
            empNew.DateOfBirth = dat;

            if (check.IsValid(emailTXT.Text))
            {
                empNew.EmployeeEmail = emailTXT.Text;
            }
            else
            {
                MessageBox.Show("Input a valid email");
            }

            if (adminRadio.IsChecked == true)
                empNew.IsAdmin = true;
            else
                empNew.IsAdmin = false;

            if (passwordTXT.IsEnabled == true && passwordTXT.Password != employeeIn.Password)
                empNew.Password = passwordTXT.Password;
            //---------

            //Checking for concurency, if someone else have been updated the employee
            var empDB = employeeService.GetEmpById(employeeIn.EmpID);
            if (empDB != null && (empDB.DateOfBirth == employeeIn.DateOfBirth && empDB.EmployeeEmail == employeeIn.EmployeeEmail &&
                empDB.EmployeeFName == employeeIn.EmployeeFName && empDB.EmployeeLName == employeeIn.EmployeeLName &&
                empDB.EmployeePhoneNumber == employeeIn.EmployeePhoneNumber && empDB.IsAdmin == employeeIn.IsAdmin &&
                empDB.Position == employeeIn.Position && empDB.Username == employeeIn.Username))
            {
                employeeService.UpdateEmployee(empNew); 
                //EmployeeController.GetInstance().UpdateEmployee(empNew);
                MessageBox.Show("Employee updated");
            }
            else
            {
                 MessageBox.Show("DB upd: Please refresh the page.", "Alert", MessageBoxButton.OK, MessageBoxImage.Warning);
            }



        }



        private void StackPanel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            emailTXT.IsReadOnly = false;
            if (e.ClickCount >= 2)
            {
                emailTXT.IsEnabled = true; //only hit here on DoubleClick  
            }
        }

        private void StackPanel_MouseDown_1(object sender, MouseButtonEventArgs e)
        {

            if (e.ClickCount >= 2)
            {
                fNameTXT.IsEnabled = true; //only hit here on DoubleClick  
            }
        }

        private void StackPanel_MouseDown_2(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount >= 2)
            {
                lNameTXT.IsEnabled = true; //only hit here on DoubleClick  
            }
        }

        private void StackPanel_MouseDown_3(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount >= 2)
            {
                dateBirthTXT.IsEnabled = true; //only hit here on DoubleClick  
            }
        }

        private void StackPanel_MouseDown_4(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount >= 2)
            {
                phoneNoTXT.IsEnabled = true; //only hit here on DoubleClick  
            }
        }

        private void StackPanel_MouseDown_5(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount >= 2)
            {
                positionTXT.IsEnabled = true; //only hit here on DoubleClick  
            }
        }

        private void StackPanel_MouseDown_6(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount >= 2)
            {
                adminRadio.IsEnabled = true; //only hit here on DoubleClick  
            }
        }

        private void StackPanel_MouseDown_7(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount >= 2)
            {
                usernameTXT.IsEnabled = true; //only hit here on DoubleClick  
            }
        }

        private void StackPanel_MouseDown_8(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount >= 2)
            {
                passwordTXT.IsEnabled = true; //only hit here on DoubleClick  
            }
        }
    }
}
