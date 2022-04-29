using Microsoft.VisualStudio.TestTools.UnitTesting;
using MTS_CRM.CONTROLLERS;
using MTS_CRM.DB;
using System;
using System.Linq;

namespace MTS_CRM.TESTS
{
    [TestClass]
    public class CustomerControllerTest
    {
        private MTSContext db = new MTSContext();
        private CustomerController custCtrl = CustomerController.GetInstance();
        [TestMethod]
        public void GetCustomerByIdTest()
        {
            var expected = GetCustomerById().ID;
            var actual = custCtrl.GetCustomerById(25).ID;
            Assert.AreEqual(expected, actual);
        }
        public Customer GetCustomerById()
        {
            return this.db.Customers.First(u => u.ID == 25);
        }
        [TestMethod]
        public void CreateCustomeTest()
        {
            Private privCust = new Private
            {
                email = "matej@matej.com",
                phoneNo = "+4587878787",
                yearOfJoin = DateTime.Now,

                fname = "Matej",
                lname = "Eres",
                age = 22
            };
        }
    }
}
