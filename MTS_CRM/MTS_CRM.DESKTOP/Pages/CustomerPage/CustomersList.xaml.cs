using MTS_CRM.CONTROLLERS;
using MTS_CRM.DB;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MTS_CRM.DESKTOP.Pages
{
    /// <summary>
    /// Interaction logic for CustomersList.xaml
    /// </summary>
    public partial class CustomersList : Page
    {
        DataTable dt;
        CustomerController custCtrl = CustomerController.GetInstance();
        public CustomersList()
        {

            dt = new DataTable();
            InitializeComponent();
            dt.Clear();
        }
        
        //this button is searching and filling the DataGrid with data
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            dt.Clear();
            this.fillingTable();
        }

        //this method is filling the DataGrid with data
        private void fillingTable()
        {

            List<Private> privates = custCtrl.GetPrivates(getCustomerEmailTXT.Text);
            List<Company> companys = custCtrl.GetCompanys(getCustomerEmailTXT.Text);

            //Adding private customers to DataGrid
            foreach (Private private1 in privates)
            {
                String name1 = private1.fname + " " + private1.lname; //Combining the first name and last name so in the DataGrid table it will be only one column for it
                String email1 = private1.email, phoneNo1 = private1.phoneNo;
                int? id1 = private1.ID;

                //creating the row
                DataRow firstRow = dt.NewRow();
                firstRow[0] = name1;
                firstRow[1] = email1;
                firstRow[2] = phoneNo1;
                dt.Rows.Add(firstRow);
            }
            
            //Adding company customers to DataGrid after finishing with the private customers
            foreach (Company company in companys)
            {
                String name1 = company.name;
                String email1 = company.email, phoneNo1 = company.phoneNo;
                int? id1 = company.ID;

                //creating the row
                DataRow firstRow = dt.NewRow();
                firstRow[0] = name1;
                firstRow[1] = email1;
                firstRow[2] = phoneNo1;
                dt.Rows.Add(firstRow);
            }


            customerListData.ItemsSource = dt.DefaultView;
        }

        //this is used to load the DataGrid
        private void customerListData_Loaded(object sender, RoutedEventArgs e)
        {
            DataColumn name = new DataColumn("Name", typeof(int));
            DataColumn email = new DataColumn("Email", typeof(string));
            DataColumn phone = new DataColumn("Phone Number", typeof(string));

            dt.Columns.Add("Name");
            dt.Columns.Add("Email");
            dt.Columns.Add("Phone Number");
            customerListData.ItemsSource = dt.DefaultView;
        }

        //method used when is double clicked a row from DataGrid
        private void customerListData_MouseDoubleClick_1(object sender, MouseButtonEventArgs e)
        {
            List<Private> privates = custCtrl.GetPrivates(getCustomerEmailTXT.Text);
            List<Company> companys = custCtrl.GetCompanys(getCustomerEmailTXT.Text);


            var currentIndex = customerListData.Items.IndexOf(customerListData.CurrentItem); //taking the number of the clicked row

            int privNo = privates.Count(); //no of private Customer objects in the list
            int compNo = companys.Count(); //no of company Customers objects in the list

            //because the table index is starting with 0, we are adding 1  to compare it with the number of existing private customers
            //if the number of the clicked row is smaller then the number private customers we will open a new page with 
            //the private customer informations 
            if (currentIndex + 1 <= privNo)
            {
                this.NavigationService.Navigate(new CustomerInfo(privates[currentIndex]));
            }
            else
            {
                this.NavigationService.Navigate(new CustomerInfo(companys[currentIndex - privNo]));
            }

        }
    }
}
