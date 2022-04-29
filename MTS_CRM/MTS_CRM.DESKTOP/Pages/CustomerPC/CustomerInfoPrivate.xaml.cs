using System;
using System.Windows.Controls;

namespace MTS_CRM.DESKTOP.Pages.CustomerPC
{
    /// <summary>
    /// Interaction logic for CustomerInfoPrivate.xaml
    /// this is the Information Page for Private Customer
    /// </summary>
    public partial class CustomerInfoPrivate : Page
    {
        public CustomerInfoPrivate()
        {
            InitializeComponent();
        }

        public void FirstName(String fName)
        {
            firstnameLBL.Content = fName;
        }
        public void LastName(String lName)
        {
            lastnameLBL.Content = lName;


        }
        public void Age(int age)
        {
            ageLBL.Content = age;
        }
    }
}
