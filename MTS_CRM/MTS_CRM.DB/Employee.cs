namespace MTS_CRM.DB
{
    using MTS_CRM.MODEL;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Runtime.Serialization;

    [Table("Employee")]
    public partial class Employee
    {
        [DataMember]
        [Required]
        [Column(Order = 0)]
        [StringLength(255)]
        [EmailCheck]
        public string EmployeeEmail { get; set; }
        [DataMember]
        [Required]
        [Key]
        [Column(Order = 1)]
        [StringLength(255)]
        public string Username { get; set; }

        [Required]
        [Column(Order = 2)]
        [StringLength(255)]
        [DataMember]
        public string Password { get; set; }
        [DataMember]
        [Required]
        [Column(Order = 3)]
        [StringLength(255)]
        public string EmployeeFName { get; set; }

        [DataMember]
        [Column(Order = 4)]
        [StringLength(255)]
        [Required]
        public string EmployeeLName { get; set; }

        [DataMember]
        [Required]

        public DateTime? DateOfBirth { get; set; }
        [DataMember]
        [Required]
        [Column(Order = 5)]
        [StringLength(8)]
        public string EmployeePhoneNumber { get; set; }

        [DataMember]
        [StringLength(255)]
        public string Position { get; set; }
        [DataMember]
        public bool? IsAdmin { get; set; }

        [DataMember]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EmpID { get; set; }
    }
}
