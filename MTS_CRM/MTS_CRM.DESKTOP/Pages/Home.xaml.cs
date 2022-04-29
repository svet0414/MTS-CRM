using LiveCharts;
using LiveCharts.Wpf;
using MTS_CRM.CONTROLLERS;
using MTS_CRM.DB;
using System;
using System.Windows.Controls;

namespace MTS_CRM.DESKTOP.Pages
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : Page
    {
        CustomerController custCtrl = CustomerController.GetInstance();
        public Home()
        {
            var customers = custCtrl.GetAllCustomers("");
            InitializeComponent();

            int year2016 = 0,
                year2017 = 0,
                year2018 = 0,
                year2019 = 0,
                year2020 = 0;
            foreach (Customer customer in customers)
            {
                if (customer.yearOfJoin > new DateTime(2016, 01, 01) && customer.yearOfJoin < new DateTime(2017, 01, 01))
                    year2016++;
                if (customer.yearOfJoin > new DateTime(2017, 01, 01) && customer.yearOfJoin < new DateTime(2018, 01, 01))
                    year2017++;
                if (customer.yearOfJoin > new DateTime(2018, 01, 01) && customer.yearOfJoin < new DateTime(2019, 01, 01))
                    year2018++;
                if (customer.yearOfJoin > new DateTime(2019, 01, 01) && customer.yearOfJoin < new DateTime(2020, 01, 01))
                    year2019++;
                if (customer.yearOfJoin > new DateTime(2020, 01, 01) && customer.yearOfJoin < new DateTime(2021, 01, 01))
                    year2020++;
            }
            
            SeriesCollection = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Customers",
                    Values = new ChartValues<int> { year2016, year2017, year2018, year2019, year2020} // changing the int in something else? default double
                },

            };

            Labels = new[] { "2016", "2017", "2018", "2019" , "2020"};
            YFormatter = value => value + ""; // how to show only integer number on Y axis????


            DataContext = this;
        }
        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> YFormatter { get; set; }
    }
}
