namespace MTS_CRM.DB
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Runtime.Serialization;

    [Table("Private")]
    public partial class Private : Customer
    {
        [DataMember]
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }

        [DataMember]
        [StringLength(255)]
        public string fname { get; set; }

        [DataMember]
        [StringLength(255)]
        public string lname { get; set; }

        [DataMember]
        public int age { get; set; }
    }
}
