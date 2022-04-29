using AutoMapper;
using MTS_CRM.CONTROLLERS;
using MTS_CRM.DB;
using MTS_CRM.DTO;
using System;
using System.Collections.Generic;

namespace MTS_CRM.WCF
{
    class CustomerService : ICustomerService
    {
        
        public void CreateCustomerCompany(Customer cust, Company comp, Location location)
        {

            CustomerController.GetInstance().CreateCustomerCompany(cust, comp, location);
        }
        public void CreateCustomerPrivate(Customer cust, Private custPriv, Location location)
        {

            CustomerController.GetInstance().CreateCustomerPrivate(cust, custPriv, location);
        }

        public CustomerDTO GetCustomerById(int id)
        {
            var dbCust = CustomerController.GetInstance().GetCustomerById(id);

            return MapDbCustomerToCustomerDto(dbCust);
        }
        /*public CustomerDTO getPrivateCustomersbyEmail(String email)
        {
            var CUSTformDB = CustomerController.GetInstance().GetPrivates(email);

            return MapDbCustomerToCustomerDto(CUSTformDB);
        }*/

        public CustomerDTO GetCustomerEmail(string email)
        {
            var CUSTformDB = CustomerController.GetInstance().GetCustomerEmail(email);

            return MapDbCustomerToCustomerDto(CUSTformDB);
        }

        public void RemoveCustomer(Customer cust)
        {
            CustomerController.GetInstance().RemoveCustomer(cust);
        }

        public void UpdatePrivateCustomer(string oldEmail,Private privateDB)
        {
            try
            {

                var privCustUpd = MaPrivateDtoToDbPrivate(privateDB);
                CustomerController.GetInstance().UpdatePrivate(oldEmail, privCustUpd);
            }
            catch (Exception ex)
            {
                throw new ArgumentException();
            }
        }


        public virtual PrivateDTO MapDbPrivateToPrivateDto(Private privDB)
        {


            return Mapper.Map<PrivateDTO>(privDB);
        }
        public virtual Private MaPrivateDtoToDbPrivate(Private privDTO)
        {

            return Mapper.Map<Private>(privDTO);


        }

        public virtual CustomerDTO MapDbCustomerToCustomerDto(Customer custDB)
        {


            return Mapper.Map<CustomerDTO>(custDB);
        }

        public virtual Customer MaCustomerDtoToDbCustomer(Customer custDTO)
        {

            return Mapper.Map<Customer>(custDTO);


        }

        public void CreateCustomer(Customer cust, Private custPriv, Company comp)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Customer> GetAllCustomers()
        {
            throw new NotImplementedException();
        }
    }
}
