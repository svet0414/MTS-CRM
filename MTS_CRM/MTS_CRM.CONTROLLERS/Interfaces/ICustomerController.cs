using MTS_CRM.DB;
using System;
using System.Collections.Generic;

namespace MTS_CRM.CONTROLLERS.Interfaces
{
    public interface ICustomerController
    {

        public void CreateCustomerPrivate(Customer cust, Private custPriv, Location location);
        public void CreateCustomerCompany(Customer cust, Company comp, Location location);
        public void UpdateCustomer(Customer cust);
        public void RemoveCustomer(Customer cust);
        public Customer GetCustomerEmail(string email);
        public Customer GetCustomerById(int id);
        public List<Customer> GetAllCustomers(String email);
        public List<Private> GetPrivates(String email);
        public List<Company> GetCompanys(String email);
        public void RemovePrivate(Private priv);
        public Private GetPrivateById(int id);
        public Private GetPrivateEmail(String email);
        public Company GetCompanyEmail(String email);
        public void RemoveCompany(Company company);
        public bool DoesExistEmail(string email);
        public void UpdatePrivate(string oldEmail, Private priv);
        public void UpdateCompany(string oldEmail, Company comp);
        public List<Private> GetPrivatesYear(DateTime startDate, DateTime endDate);




    }
}
