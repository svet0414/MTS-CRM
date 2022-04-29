//using MTS_CRM.MODEL;
using AutoMapper;
using MTS_CRM.CONTROLLERS;
using MTS_CRM.DB;
using MTS_CRM.DTO;
using System;
using System.Collections.Generic;

namespace MTS_CRM.WCF
{
    class EmployeeService : IEmployeeService
    {
        public void CreateEmployee(Employee emp)
        {
            EmployeeController.GetInstance().CreateEmployee(emp);
        }

        public List<Employee> GetAllEmployeesByFirstName(string firstName)
        {
            return EmployeeController.GetInstance().GetAllEmployeesByFirstName(firstName);
        }
        public EmployeeDTO GetEmpByFirstName(string firstName)
        {
            var dbEmp = EmployeeController.GetInstance().GetEmpByFirstName(firstName);

            return MapDbEmployeeToEmployeeDto(dbEmp);
        }

        public EmployeeDTO GetEmpById(int id)
        {
            var dbEmp = EmployeeController.GetInstance().GetEmpById(id);

            return MapDbEmployeeToEmployeeDto(dbEmp);
        }

        public EmployeeDTO GetEmpByUsername(string username)
        {
            var EMPfromDB = EmployeeController.GetInstance().GetEmpByUsername(username);

            return MapDbEmployeeToEmployeeDto(EMPfromDB);
        }

        public void RemoveEmployee(Employee emp)
        {
           // Mapper.Map<Employee>(emp);
          //  var empToDel= MaEmployeeDtoToDbEmployee(emp);
           EmployeeController.GetInstance().RemoveEmployee(emp);
            //return Mapper.Map<Employee>(emp);
        }

        public void UpdateEmployee(EmployeeDTO emp)
        {
            try
            {

                var empUpd = MaEmployeeDtoToDbEmployee(emp);
                EmployeeController.GetInstance().UpdateEmployee(empUpd);
            }
            catch (Exception ex)
            {
                throw new ArgumentException();
            }

        }

        //mappers


        [Obsolete]
        public static EmployeeDTO MapDbEmployeeToEmployeeDto(Employee emplDB)
        {
            // return Mapper.Map<EmployeeDTO>(emplDB);
            /* var config = new MapperConfiguration(cfg => {
                 cfg.CreateMap<Employee, EmployeeDTO>();
                 cfg.AddProfile<>();
             });*/
            /* var configuration = new MapperConfiguration(cfg => { });
             configuration.CompileMappings();*/

            //Mapper.Map<Employee,EmployeeDTO>();
            Mapper.Initialize(x => x.CreateMap<Employee, EmployeeDTO>());
            //Mapper.Map<Employee,EmployeeDTO>(emplDB);
            return Mapper.Map<EmployeeDTO>(emplDB);
        }

        public static Employee MaEmployeeDtoToDbEmployee(EmployeeDTO empDTO)
        {

            return Mapper.Map<Employee>(empDTO);
            //  return Mapper.Map<Employee, EmployeeDTO>(empDTO);

        }

    }
}