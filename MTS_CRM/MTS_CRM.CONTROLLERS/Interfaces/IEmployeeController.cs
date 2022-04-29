//using MTS_CRM.MODEL;
using MTS_CRM.DB;
using System.Collections.Generic;

namespace MTS_CRM.CONTROLLERS.Interfaces
{
    public interface IEmployeeController
    {
        public void CreateEmployee(Employee emp);
        public void UpdateEmployee(Employee emp);
        public void RemoveEmployee(Employee emp);
        public Employee GetEmpByUsername(string username);
        public Employee GetEmpById(int id);
        public Employee GetEmpByFirstName(string firstName);
        public List<Employee> GetAllEmployeesByFirstName(string firstName);
        public bool DoesExist(string username);
        //public void ChangePasswordEmp(string password); TODO
    }
}
