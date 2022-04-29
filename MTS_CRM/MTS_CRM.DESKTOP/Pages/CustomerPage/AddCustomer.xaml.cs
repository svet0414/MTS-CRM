using MTS_CRM.CONTROLLERS;
using MTS_CRM.DB;
using System;
using System.Threading;
using System.Windows;
using System.Windows.Controls;

namespace MTS_CRM.DESKTOP.Pages
{
    /// <summary>
    /// Interaction logic for AddCustomer.xaml
    /// </summary>
    public partial class AddCustomer : Page
    {

        CompanyCustomer companyCustomer = new CompanyCustomer();
        PrivateCustomer privateCustomer = new PrivateCustomer();
        CustomerController custCtrl;
        static CustomerService.ICustomerService customerService = new CustomerService.CustomerServiceClient();
        public AddCustomer()
        {
            custCtrl = CustomerController.GetInstance();
            InitializeComponent();
            Loaded += MyWindow_Loaded;

        }

        // is loading the PrivateCustomer page which is coresponding to the radio buttons
        private void MyWindow_Loaded(object sender, RoutedEventArgs e)
        {
            customerType.NavigationService.Navigate(privateCustomer);

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            String email = cEmailTXT.Text, phoneNumber = cPhoneNoTXT.Text, country = cCountryTXT.Text,
                city = cCityTXT.Text, address = cAddressTXT.Text;
            int zip = Convert.ToInt32(cZipCodeTXT.Text);

            //Private
            String lname = privateCustomer.LastName(), fname = privateCustomer.FirstName();
            int age = privateCustomer.Age();

            //Company
            String name = companyCustomer.Name(), cvr = companyCustomer.CVR();

            //checking if there is already someone in the database with the same email
            if (!custCtrl.DoesExistEmail(email))
            {
                //checking if the fields are empty
                if (email != "" && phoneNumber != "" && country != "" && city != "" && address != "" && cZipCodeTXT.Text != "")
                {
                    //creating the customer
                    Customer customer = new Customer();
                    customer.email = email;
                    customer.phoneNo = phoneNumber;
                    customer.yearOfJoin = DateTime.Now;

                    //creating the location
                    Location location = new Location();
                    location.address = address;
                    location.city = city;
                    location.country = country;
                    location.zipCode = zip;

                    //checking if the field for Private customer are empty and if the radioButton coresponding to private is checked
                    if (cPrivateRadio.IsChecked == true && lname != "" && fname != "" && age != 0)
                    {
                        //creating the private
                        Private privCust = new Private();
                        privCust.lname = lname;
                        privCust.fname = fname;
                        privCust.age = age;

                        //adding the private customer in database
                        loadingLBL.Visibility = Visibility.Visible;
                        Thread th = new Thread(t =>
                        {
                            customerService.CreateCustomerPrivate(customer, privCust, location);
                            //custCtrl.CreateCustomerPrivate(customer, privCust, location);

                            MessageBox.Show("Private Customer Created");
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
                        //checking if the field for Company customer are empty and if the radioButton coresponding to company is checked
                        if (cCompanyRadio.IsChecked == true && name != "" && cvr != "")
                        {
                            //creating the company
                            Company company = new Company();
                            company.CVR = cvr;
                            company.name = name;

                            //adding the company customer in database
                            loadingLBL.Visibility = Visibility.Visible;
                            Thread th = new Thread(t =>
                            {
                                customerService.CreateCustomerCompany(customer, company, location);
                                //custCtrl.CreateCustomerCompany(customer, company, location);
                                MessageBox.Show("Company Customer Created");
                                this.Dispatcher.Invoke(() =>
                                {
                                    loadingLBL.Visibility = Visibility.Hidden;
                                });

                            })
                            { IsBackground = true };
                            th.Start();
                            
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Please check all fields!");
                }
            }
            else
                MessageBox.Show("This email already exists");


        }

        //if private radio button is checking the frame it will change to PrivateCustomer Page
        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            customerType.NavigationService.Navigate(privateCustomer);

        }

        //if private radio button is checking the frame it will change to CompanyCustomer Page
        private void radioCompany_Checked(object sender, RoutedEventArgs e)
        {
            customerType.NavigationService.Navigate(companyCustomer);

        }

        //when is typing in the email textfield, is checking if there is already that email in the database
        private void cEmailTXT_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            String email = cEmailTXT.Text;
            if (email != "" && custCtrl.DoesExistEmail(email))
            {
                emailExistsLBL.Visibility = Visibility.Visible;
            }
            else
                emailExistsLBL.Visibility = Visibility.Hidden;
        }
    }
}
