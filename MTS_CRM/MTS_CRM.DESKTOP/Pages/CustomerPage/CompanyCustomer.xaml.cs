using System;
using System.Windows.Controls;

namespace MTS_CRM.DESKTOP.Pages
{
    /// <summary>
    /// Interaction logic for Company.xaml
    /// /// This is the edit/update and create page for private customer
    /// </summary>
    public partial class CompanyCustomer : Page
    {
        public CompanyCustomer()
        {
            InitializeComponent();
        }

        public String Name()
        {
            return cNameTXT.Text;
        }
        public String CVR()
        {
            return cCVRTXT.Text;
        }

    }
}
