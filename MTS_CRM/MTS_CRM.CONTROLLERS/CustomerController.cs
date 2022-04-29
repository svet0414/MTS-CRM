using MTS_CRM.CONTROLLERS.ErrorCatcher;
using MTS_CRM.CONTROLLERS.Interfaces;
using MTS_CRM.DB;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace MTS_CRM.CONTROLLERS
{
    public class CustomerController : ICustomerController
    {
        List<Customer> customers = new List<Customer>();
        private static CustomerController custCtrlInstance;
        private CustomerController() { }

        public static CustomerController GetInstance()
        {
            if (custCtrlInstance == null)
            {
                custCtrlInstance = new CustomerController();
            }
            return custCtrlInstance;
        }
        public void CreateCustomerCompany(Customer cust, Company comp, Location location)
        {
            CatchError.IfNameIsNull(cust, nameof(cust));
            CatchError.IfNullOrEmpty(cust.email, nameof(cust.email));
            CatchError.IfNullOrEmpty(cust.phoneNo, nameof(cust.phoneNo));
            // CatchError.IfNullOrEmpty(cust.yearOfJoin, nameof(cust.yearOfJoin));
            // CatchError.IfNullOrEmpty(cust.Location, nameof(cust.Location));
            using (var db = new DB.MTSContext())
            {

                var loc = new Location
                {
                    address = location.address,
                    city = location.city,
                    country = location.country,
                    zipCode = location.zipCode

                };
                db.Locations.Add(loc);

                db.SaveChanges();

                var company = new Company
                {
                    email = cust.email,
                    phoneNo = cust.phoneNo,
                    yearOfJoin = cust.yearOfJoin,
                    name = comp.name,
                    CVR = comp.CVR,
                    locationID = loc.ID

                };

                //db.Customers.Add(privCust);
                db.Customers.Add(company);
                db.SaveChanges();
            }
        }
        public void CreateCustomerPrivate(Customer cust, Private custPriv, Location location)
        {

            CatchError.IfNameIsNull(cust, nameof(cust));
            CatchError.IfNullOrEmpty(cust.email, nameof(cust.email));
            CatchError.IfNullOrEmpty(cust.phoneNo, nameof(cust.phoneNo));
            // CatchError.IfNullOrEmpty(cust.yearOfJoin, nameof(cust.yearOfJoin));
            // CatchError.IfNullOrEmpty(cust.Location, nameof(cust.Location));
            using (var db = new DB.MTSContext())
            {
                var loc = new Location
                {
                    address = location.address,
                    city = location.city,
                    country = location.country,
                    zipCode = location.zipCode

                };
                db.Locations.Add(loc);
                db.SaveChanges();

                var privCust = new Private
                {
                    locationID = loc.ID,
                    email = cust.email,
                    phoneNo = cust.phoneNo,
                    yearOfJoin = cust.yearOfJoin,
                    fname = custPriv.fname,
                    lname = custPriv.lname,
                    age = custPriv.age,

                };


                // db.Customers.Add(privCust);
                db.Entry(privCust).State = EntityState.Added;
                db.SaveChanges();
            }

        }



        public List<Customer> GetAllCustomers(String email)
        {
            //CatchError.IfNullOrEmpty(firstName, nameof(firstName));
            List<Customer> customers;
            using (var db = new MTSContext())
            {
                customers = db.Customers.Where(x => x.email.Contains(email)).ToList();

            }
            return customers;
        }
        public List<Private> GetPrivates(String email)
        {
            List<Private> privates;
            using (var db = new MTSContext())
            {
                privates = db.Privates.Where(x => x.fname.Contains(email)).ToList();
            }
            return privates;
        }

        public List<Private> GetPrivatesYear(DateTime startDate, DateTime endDate)
        {
            List<Private> privates1;
            using (var db = new MTSContext())
            {

                privates1 = db.Privates
                    .Where(o => o.yearOfJoin >= startDate && o.yearOfJoin < endDate)
                .ToList();
            }
            return privates1;
        }

        public List<Company> GetCompanys(String email)
        {
            List<Company> companies;
            using (var db = new MTSContext())
            {
                companies = db.Companies.Where(x => x.name.Contains(email)).ToList();
            }
            return companies;
        }

        public Location GetLocationById(int id)
        {

            CatchError.IfNullOrEmpty(id.ToString(), nameof(id));
            using (var db = new DB.MTSContext())
            {
                var currentLocation = db.Locations.FirstOrDefault(x => x.ID == id);
                return currentLocation;
            }
        }

        public Customer GetCustomerById(int id)
        {

            CatchError.IfNullOrEmpty(id.ToString(), nameof(id));
            using (var db = new DB.MTSContext())
            {
                var currentCustomer = db.Customers.FirstOrDefault(x => x.ID == id);
                return currentCustomer;
            }
        }

        public Private GetPrivateById(int id)
        {

            CatchError.IfNullOrEmpty(id.ToString(), nameof(id));
            using (var db = new DB.MTSContext())
            {
                var currentPrivate = db.Privates.FirstOrDefault(x => x.ID == id);
                return currentPrivate;
            }
        }

        public Customer GetCustomerEmail(string email)
        {
            CatchError.IfNullOrEmpty(email, nameof(email));

            using (var db = new DB.MTSContext())
            {
                var currentCustomer = db.Customers.FirstOrDefault(x => x.email == email);
                return currentCustomer;
            }
        }
        public Private GetPrivateEmail(string email)
        {
            CatchError.IfNullOrEmpty(email, nameof(email));

            using (var db = new DB.MTSContext())
            {
                var currentPriv = db.Privates.FirstOrDefault(x => x.email == email);
                return currentPriv;
            }
        }

        public Company GetCompanyEmail(string email)
        {
            CatchError.IfNullOrEmpty(email, nameof(email));

            using (var db = new DB.MTSContext())
            {
                var currentComp = db.Companies.FirstOrDefault(x => x.email == email);
                return currentComp;
            }
        }

        public void RemoveCustomer(Customer cust)
        {
            using (var db = new DB.MTSContext())
            {
                var custToRemove = GetCustomerById(cust.ID);

                db.Customers.Attach(custToRemove);
                db.Entry(custToRemove).State = EntityState.Deleted;
                db.SaveChanges();
            }
        }

        public void RemovePrivate(Private priv)
        {
            using (var db = new DB.MTSContext())
            {
                var privateToRemove = GetPrivateEmail(priv.email);

                db.Privates.Attach(privateToRemove);
                db.Entry(privateToRemove).State = EntityState.Deleted;
                db.SaveChanges();
            }
        }

        public void RemoveCompany(Company comp)
        {
            using (var db = new DB.MTSContext())
            {
                var companyToRemove = GetCompanyEmail(comp.email);

                db.Companies.Attach(companyToRemove);
                db.Entry(companyToRemove).State = EntityState.Deleted;
                db.SaveChanges();
            }
        }

        public void UpdateCustomer(Customer cust)
        {
            using (var db = new DB.MTSContext())
            {
                var updCustToDB = GetCustomerById(cust.ID);


                db.Customers.Attach(updCustToDB);

                updCustToDB.email = cust.email;
                updCustToDB.phoneNo = cust.phoneNo;
                //updCustToDB.yearOfJoin = cust.yearOfJoin;

                db.Entry(updCustToDB).State = EntityState.Modified;
                db.SaveChanges();
            }

        }
        public void UpdatePrivate(string oldEmail, Private priv)
        {
            using (var db = new DB.MTSContext())
            {
                var updPrivToDB = GetPrivateEmail(oldEmail);


                db.Privates.Attach(updPrivToDB);

                updPrivToDB.email = priv.email;
                updPrivToDB.phoneNo = priv.phoneNo;
                updPrivToDB.lname = priv.lname;
                updPrivToDB.fname = priv.fname;
                updPrivToDB.age = priv.age;

                //updCustToDB.yearOfJoin = cust.yearOfJoin;

                db.Entry(updPrivToDB).State = EntityState.Modified;
                db.SaveChanges();
            }

        }

        public void UpdateCompany(string oldEmail, Company comp)
        {
            using (var db = new DB.MTSContext())
            {
                var updCompToDB = GetCompanyEmail(oldEmail);


                db.Companies.Attach(updCompToDB);

                updCompToDB.email = comp.email;
                updCompToDB.phoneNo = comp.phoneNo;
                updCompToDB.name = comp.name;
                updCompToDB.CVR = comp.CVR;


                //updCustToDB.yearOfJoin = cust.yearOfJoin;

                db.Entry(updCompToDB).State = EntityState.Modified;
                db.SaveChanges();
            }

        }

        public bool DoesExistEmail(string email)
        {
            CatchError.IfNullOrEmpty(email, nameof(email));
            using (var db = new DB.MTSContext())
            {
                return db.Customers.Any(x => x.email == email);
            }
        }

    }
}
