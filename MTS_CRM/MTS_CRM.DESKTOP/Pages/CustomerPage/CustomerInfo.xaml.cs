using MTS_CRM.CONTROLLERS;
using MTS_CRM.DB;
using MTS_CRM.DESKTOP.Pages.CustomerPC;
using System.Threading;
using System.Windows;
using System.Windows.Controls;

namespace MTS_CRM.DESKTOP.Pages
{
    /// <summary>
    /// Interaction logic for CustomerInfo.xaml
    /// </summary>
    public partial class CustomerInfo : Page
    {
        CustomerInfoPrivate customerInfoPrivate = new CustomerInfoPrivate();
        CustomerInfoCompany customerInfoCompany = new CustomerInfoCompany();
        Private priv;
        Company comp;

        //The program is using the constructor coresponding to the customer needed private/company
        public CustomerInfo(Private priv)
        {
            InitializeComponent();
            this.priv = priv;
            if (priv != null)
            {
                emailLBL.Content = priv.email;
                phonenumberLBL.Content = priv.phoneNo;
                yearofjoinLBL.Content = priv.yearOfJoin;

                PrivInfo(priv);
            }
            customerInfoType.NavigationService.Navigate(customerInfoPrivate);
        }
        public CustomerInfo(Company comp)
        {
            InitializeComponent();
            this.comp = comp;
            if (comp != null)
            {
                emailLBL.Content = comp.email;
                phonenumberLBL.Content = comp.phoneNo;
                yearofjoinLBL.Content = comp.yearOfJoin;
                CompInfo(comp);
            }
            customerInfoType.NavigationService.Navigate(customerInfoCompany);
        }

        //this method is setting the labels of first name, last name and the age in the customerInfoPrivate page
        private void PrivInfo(Private priv)
        {
            customerInfoPrivate.FirstName(priv.fname);
            customerInfoPrivate.LastName(priv.lname);
            customerInfoPrivate.Age(priv.age);
        }
        //this method is setting the labels of name and cvr in the customerInfoCompany page
        private void CompInfo(Company comp)
        {
            customerInfoCompany.Name(comp.name);
            customerInfoCompany.CVR(comp.CVR);
        }

        //Remove Customer  button
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            CustomerController custCtrl = CustomerController.GetInstance();
            if (priv != null)
            {
                loadingLBL.Visibility = Visibility.Visible;
                Thread th = new Thread(t =>
                {
                    custCtrl.RemovePrivate(priv);

                    MessageBox.Show("Customer Removed");
                    this.Dispatcher.Invoke(() =>
                    {
                        loadingLBL.Visibility = Visibility.Hidden;
                        //returning back to the CustomersList page
                        this.NavigationService.Navigate(new CustomersList());
                    });

                })
                { IsBackground = true };
                th.Start();
            }
                
            else
            {
                loadingLBL.Visibility = Visibility.Visible;
                Thread th = new Thread(t =>
                {
                    custCtrl.RemoveCompany(comp);

                    MessageBox.Show("Customer Removed");
                    this.Dispatcher.Invoke(() =>
                    {
                        loadingLBL.Visibility = Visibility.Hidden;
                        //returning back to the CustomersList page
                        this.NavigationService.Navigate(new CustomersList());
                    });

                })
                { IsBackground = true };
                th.Start();
            }
                
           

            
        }

        //Edit Customer button, this button is opening the a new page where the private or company customer can be edited
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (comp != null)
            {
                this.NavigationService.Navigate(new Pages.CustomerPage.CustomerEdit(comp));
            }
            else
            {
                this.NavigationService.Navigate(new Pages.CustomerPage.CustomerEdit(priv));
            }
        }
    }
}
