namespace MTS_CRM.DB
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Runtime.Serialization;

    [Table("Customer")]
    public partial class Customer
    {
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        /*public Customer()
        {
            ProductToCustomers = new HashSet<ProductToCustomer>();
        }*/
        [DataMember]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [DataMember]
        [Required]
        [StringLength(255)]
        public string email { get; set; }

        [DataMember]
        [Required]
        [StringLength(20)]
        public string phoneNo { get; set; }
        [DataMember]
        public DateTime? yearOfJoin { get; set; }
        [DataMember]
        public int locationID { get; set; }

        /*[DataMember]
        public virtual Company Company { get; set; }
        [DataMember]
        public virtual Location Location { get; set; }
        [DataMember]
        public virtual Private Private { get; set; }*/
        /*[DataMember]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductToCustomer> ProductToCustomers { get; set; }*/
    }
}
