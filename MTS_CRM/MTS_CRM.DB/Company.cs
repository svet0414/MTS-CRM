namespace MTS_CRM.DB
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Runtime.Serialization;

    [Table("Company")]
    public partial class Company : Customer
    {
        [DataMember]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }

        [DataMember]
        [Required]
        [StringLength(255)]
        public string name { get; set; }

        [DataMember]
        [Required]
        [StringLength(255)]
        public string CVR { get; set; }


    }
}
