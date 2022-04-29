using MTS_CRM.CONTROLLERS;
using MTS_CRM.DB;
using System.Windows;
using System.Windows.Controls;

namespace MTS_CRM.DESKTOP.Pages.CustomerPage
{
    /// <summary>
    /// Interaction logic for CustomerEdit.xaml
    /// </summary>
    /// 

    public partial class CustomerEdit : Page
    {
        CompanyCustomer companyCustomer = new CompanyCustomer();
        PrivateCustomer privateCustomer = new PrivateCustomer();
        Private privIn;

        Company compIn;
        Location loc;
        CustomerController custCtrl = CustomerController.GetInstance();
        static CustomerService.ICustomerService customerService = new CustomerService.CustomerServiceClient();
        public CustomerEdit(Private priv)
        {
            
            InitializeComponent();
            privIn = priv;
            customerTypeFrame.NavigationService.Navigate(privateCustomer);
            cEmailTXT.Text = privIn.email;
            cPhoneNoTXT.Text = privIn.phoneNo;
            loc = custCtrl.GetLocationById(privIn.locationID);
            cAddressTXT.Text = loc.address;
            cCityTXT.Text = loc.city;
            cCountryTXT.Text = loc.country;
            cZipCodeTXT.Text = loc.zipCode.ToString();
            privateCustomer.cAgeTXT.Text = privIn.age.ToString();
            privateCustomer.cFirstNameTXT.Text = privIn.fname;
            privateCustomer.cLastNameTXT.Text = privIn.lname;
        }

        public CustomerEdit(Company comp)
        {

            InitializeComponent();
            compIn = comp;
            customerTypeFrame.NavigationService.Navigate(companyCustomer);
            cEmailTXT.Text = comp.email;
            cPhoneNoTXT.Text = comp.phoneNo;
            loc = custCtrl.GetLocationById(comp.locationID);
            cAddressTXT.Text = loc.address;
            cCityTXT.Text = loc.city;
            cCountryTXT.Text = loc.country;
            cZipCodeTXT.Text = loc.zipCode.ToString();
            companyCustomer.cNameTXT.Text = comp.name;
            companyCustomer.cCVRTXT.Text = comp.CVR;

        }

        //Update Customer button
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (privIn != null)
            {
                //creating the private
                Private privNew = new Private
                {
                    ID = privIn.ID,
                    email = cEmailTXT.Text,
                    age = privateCustomer.Age(),
                    fname = privateCustomer.FirstName(),
                    lname = privateCustomer.LastName(),
                    phoneNo = cPhoneNoTXT.Text

                };
                Private privDB = custCtrl.GetPrivateEmail(privIn.email);

                //Checking for concurency, if someone else have been updated the private customer
                if (privDB != null && (privDB.ID == privIn.ID && privDB.lname == privIn.lname && privDB.phoneNo == privIn.phoneNo && privDB.fname == privIn.fname && privDB.age == privIn.age
                    && privDB.email == privIn.email))
                {
                   //customerService.UpdatePrivateCustomer(privIn.email, privNew);
                    custCtrl.UpdatePrivate(privIn.email, privNew);
                    MessageBox.Show("Customer Updated");
                }
                else
                {
                     MessageBox.Show("W1 - DB upd: Please refresh the page.", "Alert", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                if (compIn != null)
                {
                    Company compNew = new Company
                    {
                        ID = compIn.ID,
                        email = cEmailTXT.Text,
                        CVR = companyCustomer.CVR(),
                        name = companyCustomer.Name(),
                        phoneNo = cPhoneNoTXT.Text,

                    };
                    Company compDB = custCtrl.GetCompanyEmail(compIn.email);

                    //Checking for concurency, if someone else have been updated the private customer
                    if (compDB == null || !(compDB.ID == compIn.ID && compDB.name == compIn.name && compDB.phoneNo == compIn.phoneNo &&
                        compDB.CVR == compIn.CVR && compDB.email == compIn.email))
                    {
                        MessageBox.Show("W1 - DB upd: Please refresh the page.", "Alert", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    else
                    {
                        custCtrl.UpdateCompany(compIn.email, compNew);
                        MessageBox.Show("Customer Updated");
                    }
                }
            }
        }
    }
}
