using Microsoft.VisualStudio.TestTools.UnitTesting;
using MTS_CRM.DB;
//using MTS_CRM.MODEL;
using System;
using System.Linq;

namespace MTS_CRM.CONTROLLERS.Tests
{
    [TestClass()]
    public class EmployeeControllerTests
    {
        private MTSContext db = new MTSContext();
        private EmployeeController empCtrl = EmployeeController.GetInstance();
        // [TestInitialize]
        /* public void Initialize() {
             this.db = new MTSContext();
             this.empCtrl = EmployeeController.GetInstance();

         }*/


        /*public void CreateEmployee(Employee emp)
        {
            
            CreateEmployees(emp);

            var actual = this.db.Employees.First(x => x.EmployeeFName == "Svetlio");

            Assert.AreEqual("Svetlio", actual.EmployeeFName);
        }*/

        [TestMethod()]
        public void GetEmpByUsernameTest()
        {

            var expected = getEmpByUsername().Username;
            var actual = empCtrl.GetEmpByUsername("Tudor").Username;
            Assert.AreEqual(expected, actual);

        }

        public Employee getEmpByUsername()
        {
            return db.Employees.First(u => u.Username == "Tudor");
        }


        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void UsernameInputEmpty()
        {
            var actual = empCtrl.GetEmpByUsername("").Username;
            // Assert.Fail();

        }


        [TestMethod()]
        public void GetEmpByIdTest()
        { 

            var expected = GetEmpById().EmpID;
            var actual = empCtrl.GetEmpById(22).EmpID;
            Assert.AreEqual(expected, actual);

        }
        public Employee GetEmpById()
        {
            return this.db.Employees.First(u => u.EmpID == 22);
        }
        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void IfEmployeeDoesNotExist()
        {
            var actual = empCtrl.GetEmpById(20).EmpID;
            Assert.IsNull(empCtrl);
        }

        [TestMethod()]
        public void CreateEmployeeTest()
        {
            Employee emp = new Employee
            {
                EmployeeFName = "Tudor",
                EmployeeLName = "TudorLname",
                EmployeeEmail = "Tudor@gmail.com",
                Username = "tudor",
                Password = "Password1",
                EmployeePhoneNumber = "12326545",
                Position = "none",
                IsAdmin = true,
                DateOfBirth = DateTime.Now




            };

        }
        private LoginController logCtrl = LoginController.GetInstance();
        [TestMethod()]
        public void LoginCheckTest()
        {
            var test = logCtrl.LoginCheck("tudor", "Password1");
            //Assert.IsTrue(test);
        }
    }
}