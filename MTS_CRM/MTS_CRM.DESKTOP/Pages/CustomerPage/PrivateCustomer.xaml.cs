using System;
using System.Windows.Controls;

namespace MTS_CRM.DESKTOP.Pages
{
    /// <summary>
    /// Interaction logic for Private.xaml
    /// This is the edit/update and create page for private customer
    /// </summary>
    public partial class PrivateCustomer : Page
    {
        public PrivateCustomer()
        {
            InitializeComponent();

        }


        public String FirstName()
        {
            return cFirstNameTXT.Text;
        }
        public String LastName()
        {
            return cLastNameTXT.Text;
        }
        public int Age()
        {
            if (cAgeTXT.Text == "")
                return 0;
            else
                return Convert.ToInt32(cAgeTXT.Text);
        }
    }
}
