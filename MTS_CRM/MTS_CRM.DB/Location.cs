namespace MTS_CRM.DB
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Runtime.Serialization;

    [Table("Location")]
    public partial class Location
    {
        /*[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Location()
        {
            Customers = new HashSet<Customer>();
        }*/
        [DataMember]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [DataMember]
        [Required]
        [StringLength(255)]
        public string city { get; set; }

        public int zipCode { get; set; }

        [DataMember]
        [Required]
        [StringLength(255)]
        public string address { get; set; }

        [DataMember]
        [Required]
        [StringLength(255)]
        public string country { get; set; }

        /*[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Customer> Customers { get; set; }*/
    }
}
