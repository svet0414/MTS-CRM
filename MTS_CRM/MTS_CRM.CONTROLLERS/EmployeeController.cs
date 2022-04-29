using MTS_CRM.CONTROLLERS.ErrorCatcher;
using MTS_CRM.CONTROLLERS.Interfaces;
using MTS_CRM.CONTROLLERS.PasswordEncryption;
using MTS_CRM.DB;
using System;
using System.Collections.Generic;
//using MTS_CRM.MODEL;
using System.Data.Entity;
using MTS_CRM.DB;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace MTS_CRM.CONTROLLERS
{
    public class EmployeeController : IEmployeeController
    {
        private static EmployeeController empCtrlInstance;
        private MODEL.EmailCheck check = new MODEL.EmailCheck();
        private EmployeeController() { }

        public static EmployeeController GetInstance()
        {
            if (empCtrlInstance == null)
            {
                empCtrlInstance = new EmployeeController();
            }
            return empCtrlInstance;

        }

        public void CreateEmployee(Employee emp)
        {
            //try

            //The nameof operator obtains the name of a variable, type, or member as the string constant:
            //You can use the nameof operator to make the argument-checking code more maintainable:
            CatchError.IfNameIsNull(emp, nameof(emp));
            CatchError.IfNullOrEmpty(emp.Username, nameof(emp.Username));
            CatchError.IfNullOrEmpty(emp.Password, nameof(emp.Password));
            CatchError.IfNullOrEmpty(emp.EmployeeEmail, nameof(emp.EmployeeEmail));
            CatchError.IfNullOrEmpty(emp.EmployeePhoneNumber, nameof(emp.EmployeePhoneNumber));
            CatchError.IfNullOrEmpty(emp.EmployeeFName, nameof(emp.EmployeeFName));
            CatchError.IfNullOrEmpty(emp.EmployeeLName, nameof(emp.EmployeeLName));
            CatchError.IsDate(emp.DateOfBirth);
            SecurePassword.CheckPassword(emp.Password);
            using (var db = new MTSContext())
            {
                var employee = new Employee
                {
                    Username = emp.Username,
                    Password = SecurePassword.Hash(emp.Password),
                    EmployeeFName = emp.EmployeeFName,
                    EmployeeLName = emp.EmployeeLName,
                    EmployeeEmail = emp.EmployeeEmail,
                    EmployeePhoneNumber = emp.EmployeePhoneNumber,
                    DateOfBirth = emp.DateOfBirth,
                    IsAdmin = emp.IsAdmin,
                    Position = emp.Position

                };

                   /* db.Employees.Add(employee);
                    db.Entry(employee).State=EntityState.Added;
                    db.SaveChanges();*/
                var saved = false;
                while (!saved)
                {
                    try
                    {
                        // Attempt to save changes to the database1
                        db.Employees.Add(employee);
                        db.Entry(employee).State = EntityState.Added;
                        db.SaveChanges();
                        saved = true;
                    }
                    catch (DbUpdateConcurrencyException ex)
                    {
                        foreach (var entry in ex.Entries)
                        {
                            if (entry.Entity is Employee)
                            {
                                var proposedValues = entry.CurrentValues;
                                var databaseValues = entry.GetDatabaseValues();

                                foreach (var property in proposedValues.PropertyNames)
                                {
                                    var proposedValue = proposedValues[property];
                                    var databaseValue = databaseValues[property];

                                    // TODO: decide which value should be written to database
                                    // proposedValues[property] = <value to be saved>;
                                }

                                // Refresh original values to bypass next concurrency check
                                entry.OriginalValues.SetValues(databaseValues);
                            }
                            else
                            {
                                throw new NotSupportedException(
                                    "Don't know how to handle concurrency conflicts for "
                                    + entry);
                            }
                        }
                    }
                }

            }

            
            /*catch(Exception) {
                throw new ArgumentNullException("You should input all parameters needed");
            }*/
        }

        public void UpdateEmployee(Employee emp)
        {

            CatchError.IfNameIsNull(emp, nameof(emp));
            CatchError.IfNullOrEmpty(emp.EmployeeFName, nameof(emp.EmployeeFName));
            CatchError.IfNullOrEmpty(emp.EmployeeLName, nameof(emp.EmployeeLName));
            CatchError.IfNullOrEmpty(emp.EmployeeEmail, nameof(emp.EmployeeEmail));
            CatchError.IfNullOrEmpty(emp.EmployeePhoneNumber, nameof(emp.EmployeePhoneNumber));
            using (var db = new MTSContext())
            {
               
                var updEmpToDB = GetEmpById(emp.EmpID);
                if (db.Entry(updEmpToDB).State != EntityState.Modified)
                {
                    db.Employees.Attach(updEmpToDB);
                    //Attach is used to repopulate a context with an entity that is known to already exist in the database.
                    updEmpToDB.EmployeeFName = emp.EmployeeFName;
                    updEmpToDB.EmployeeLName = emp.EmployeeLName;
                    if (check.IsValid(updEmpToDB.EmployeeEmail))
                    {
                        updEmpToDB.EmployeeEmail = emp.EmployeeEmail;
                    }
                    else
                    {
                        throw new InvalidOperationException("Email Failed to update");
                    }

                    updEmpToDB.EmployeePhoneNumber = emp.EmployeePhoneNumber;
                    //Attaching an existing but modified entity to the db
                    //We know that the employee object already is in the database and we can edit it
                    //and we use State method to change it's value to modified(updated)
                    //everytime we use this method it will be overwriting and modifying the entity of this customer
                  //  db.Entry(updEmpToDB).State = EntityState.Modified;
                   // db.SaveChanges();
                    var saved = false;
                    while (!saved)
                    {
                        try
                        {
                            // Attempt to save changes to the database
                            db.SaveChanges();
                            saved = true;
                        }
                        catch (DbUpdateConcurrencyException ex)
                        {
                            foreach (var entry in ex.Entries)
                            {
                                if (entry.Entity is Employee)
                                {
                                    var proposedValues = entry.CurrentValues;
                                    var databaseValues = entry.GetDatabaseValues();

                                    foreach (var property in proposedValues.PropertyNames)
                                    {
                                        var proposedValue = proposedValues[property];
                                        var databaseValue = databaseValues[property];

                                        // TODO: decide which value should be written to database
                                        // proposedValues[property] = <value to be saved>;
                                    }

                                    // Refresh original values to bypass next concurrency check
                                    entry.OriginalValues.SetValues(databaseValues);
                                }
                                else
                                {
                                    throw new NotSupportedException(
                                        "Don't know how to handle concurrency conflicts for "
                                        + entry);
                                }
                            }
                        }
                    }

                }
                else {
                    throw new OperationCanceledException("Refresh , someone has updated it before you");
                }

            }
        }

        //public void ChangePasswordEmp(string password){} 

        public Employee GetEmpByFirstName(string firstName)
        {
            CatchError.IfNullOrEmpty(firstName, nameof(firstName));
            using (var db = new MTSContext())
            {
                var currentEmployee = db.Employees.FirstOrDefault(x => x.EmployeeFName == firstName);
                return currentEmployee;

            }
        }

        public List<Employee> GetAllEmployeesByFirstName(string firstName)
        {
            //CatchError.IfNullOrEmpty(firstName, nameof(firstName));
            List<Employee> employees;
            using (var db = new MTSContext())
            {
                employees = db.Employees.Where(x => x.EmployeeFName.Contains(firstName)).ToList();


            }
            return employees;
        }


        public Employee GetEmpById(int id)
        {
            CatchError.IfNullOrEmpty(id.ToString(), nameof(id));
            using (var db = new MTSContext())
            {
                var currentEmployee = db.Employees.FirstOrDefault(x => x.EmpID == id);
                return currentEmployee;
            }

        }

        public Employee GetEmpByUsername(string username)
        {
            CatchError.IfNullOrEmpty(username, nameof(username));

            using (var db = new DB.MTSContext())
            {
                var currentEmployee = db.Employees.FirstOrDefault(x => x.EmployeeFName == username);
                return currentEmployee;
            }
        }

        public void RemoveEmployee(Employee emp)
        {
            using (var db = new DB.MTSContext())
            {
                var empToRemove = GetEmpById(emp.EmpID);
                if (db.Entry(empToRemove).State != EntityState.Deleted)
                {
                    db.Employees.Attach(empToRemove);
                    db.Entry(empToRemove).State = EntityState.Deleted;
                    db.SaveChanges();
                }
                else {
                    throw new InvalidOperationException("Someone has already deleted it");
                }
            }
        }

        public bool DoesExist(string username)
        {
            CatchError.IfNullOrEmpty(username, nameof(username));
            using (var db = new DB.MTSContext())
            {
                return db.Employees.Any(x => x.Username == username);
            }
        }

        public bool DoesExistEmail(string email)
        {
            CatchError.IfNullOrEmpty(email, nameof(email));
            using (var db = new DB.MTSContext())
            {
                return db.Employees.Any(x => x.EmployeeEmail == email);
            }
        }


    }


}
