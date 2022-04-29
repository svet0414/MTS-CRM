using System;
using System.Windows.Controls;

namespace MTS_CRM.DESKTOP.Pages.CustomerPC
{
    /// <summary>
    /// Interaction logic for CustomerInfoCompany.xaml
    /// this is the information page for Company Customer
    /// </summary>
    public partial class CustomerInfoCompany : Page
    {
        public CustomerInfoCompany()
        {
            InitializeComponent();
        }
        public void Name(String name)
        {
            nameLBL.Content = name;
        }
        public void CVR(String cvr)
        {
            cvrLBL.Content = cvr;
        }
    }
}
