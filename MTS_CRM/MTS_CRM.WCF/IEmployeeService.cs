using MTS_CRM.DB;
using MTS_CRM.DTO;
using System.Collections.Generic;
//using MTS_CRM.MODEL;
using System.ServiceModel;

namespace MTS_CRM.WCF
{
    [ServiceContract]
    interface IEmployeeService
    {

        [OperationContract]
        void CreateEmployee(Employee emp);

        [OperationContract]
        void UpdateEmployee(DTO.EmployeeDTO emp);
        [OperationContract]
        void RemoveEmployee(Employee emp);
        [OperationContract]
        EmployeeDTO GetEmpByUsername(string username);
        [OperationContract]
        EmployeeDTO GetEmpById(int id);
        [OperationContract]
        EmployeeDTO GetEmpByFirstName(string firstName);
        [OperationContract]
        List<Employee> GetAllEmployeesByFirstName(string firstName);


    }
}
