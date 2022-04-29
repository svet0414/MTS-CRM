//using MTS_CRM.CONTROLLERS.PasswordEncryption;
using MTS_CRM.DB;
using System;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace MTS_CRM.DESKTOP.Pages.EmployeePage
{
    /// <summary>
    /// Interaction logic for EmployeeCreate.xaml
    /// </summary>
    public partial class EmployeeCreate : Page
    {
        static EmployeeService.IEmployeeService employeeService = new EmployeeService.EmployeeServiceClient();
        bool isValidated = false;
        public EmployeeCreate()
        {
            InitializeComponent();
        }
        //Register Employee button
        private void registerEmployeeBTN_Click(object sender, RoutedEventArgs e)
        {
            String email = emailTXT.Text, firstname = firstNameTXT.Text, lastname = lastNameTXT.Text, phonenumber = phoneNumberTXT.Text;
            String position = positionTXT.Text;
            String username = usernameTXT.Text, password = passwordTXT.Password;

            Employee employee = new Employee();
            employee.EmployeeEmail = email;
            employee.EmployeeFName = firstname;
            employee.EmployeeLName = lastname;
            employee.EmployeePhoneNumber = phonenumber;
            if (adminRadio.IsChecked == true)
                employee.IsAdmin = true;
            else
                employee.IsAdmin = false;
            employee.Password = password;
            employee.Position = position;
            employee.Username = username;
            DateTime dat;
            //checking if all textboxes are not empty. And the datePickerTXT must be checked if is null before getting the date 
            //so we won't receive an error
            if (email != null && firstname != null && lastname != null && phonenumber != null && password != null &&
                username != null && datePickerTXT.SelectedDate != null)
            {
                isValidated = true;
                dat = datePickerTXT.SelectedDate.Value;
                employee.DateOfBirth = dat;
            }
            else
            {
                isValidated = false;
            }

            //if the fields are not empty, the employee will be created
            if (isValidated == true)
            {
                loadingLBL.Visibility = Visibility.Visible;
                Thread th = new Thread(t =>
                {
                    employeeService.CreateEmployee(employee);
                    MessageBox.Show("Employee created");
                    this.Dispatcher.Invoke(() =>
                    {
                        loadingLBL.Visibility = Visibility.Hidden;
                    });
                    
                })
                { IsBackground = true };
                th.Start();
                

            }
            else
            {
                MessageBox.Show("Employee could not be created");
            }
        }

        //Checking the password
        private void passwordTXT_KeyUp(object sender, KeyEventArgs e)
        {
            String plainText = passwordTXT.Password;

            var hasNumber = new Regex(@"[0-9]+");
            var hasUpperChar = new Regex(@"[A-Z]+");
            var hasMinimum5Chars = new Regex(@".{5,}");


            if (hasNumber.IsMatch(plainText) && hasUpperChar.IsMatch(plainText) && hasMinimum5Chars.IsMatch(plainText))
            {
                isValidated = true;
                passwordNeedsLBL.Visibility = Visibility.Hidden;
                oneNoLBL.Visibility = Visibility.Hidden;
                oneUpperCaseLBL.Visibility = Visibility.Hidden;
                fiveCharactersLBL.Visibility = Visibility.Hidden;
            }
            else
            {
                passwordNeedsLBL.Visibility = Visibility.Visible;
                oneNoLBL.Visibility = Visibility.Visible;
                oneUpperCaseLBL.Visibility = Visibility.Visible;
                fiveCharactersLBL.Visibility = Visibility.Visible;

                if (hasNumber.IsMatch(plainText))
                    oneNoLBL.Foreground = Brushes.Green;
                else
                    oneNoLBL.Foreground = Brushes.Red;

                if (hasUpperChar.IsMatch(plainText))
                    oneUpperCaseLBL.Foreground = Brushes.Green;
                else
                    oneUpperCaseLBL.Foreground = Brushes.Red;

                if (hasMinimum5Chars.IsMatch(plainText))
                    fiveCharactersLBL.Foreground = Brushes.Green;
                else
                    fiveCharactersLBL.Foreground = Brushes.Red;
            }

        }
    }
}
