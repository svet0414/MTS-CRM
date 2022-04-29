using System;


namespace MTS_CRM.DTO
{
    public class EmployeeDTO
    {
        public int EmpID { get; set; }
        public string EmployeeEmail { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string EmployeeFName { get; set; }
        public string EmployeeLName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string EmployeePhoneNumber { get; set; }
        public string Position { get; set; }
        public bool IsAdmin { get; set; }
    }
}
