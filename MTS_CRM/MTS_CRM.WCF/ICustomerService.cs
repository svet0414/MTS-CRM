using MTS_CRM.DB;
using MTS_CRM.DTO;
using System.Collections.Generic;
using System.ServiceModel;

namespace MTS_CRM.WCF
{
    [ServiceContract]
    interface ICustomerService
    {
        [OperationContract]
        void CreateCustomerCompany(Customer cust, Company comp, Location location);
        [OperationContract]
        void CreateCustomerPrivate(Customer cust, Private custPriv, Location location);
        [OperationContract]
        void CreateCustomer(Customer cust, Private custPriv, Company comp);
        /*[OperationContract]
        void UpdateCustomer(CustomerDTO cust);*/
        [OperationContract]
        void RemoveCustomer(Customer cust);
        [OperationContract]
        CustomerDTO GetCustomerEmail(string email);
        [OperationContract]
        CustomerDTO GetCustomerById(int id);
        [OperationContract]
        public void UpdatePrivateCustomer(string oldEmail, Private privateDB);
        [OperationContract]
        IEnumerable<Customer> GetAllCustomers();
    }
}
