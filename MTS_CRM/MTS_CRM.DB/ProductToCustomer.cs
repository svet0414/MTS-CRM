namespace MTS_CRM.DB
{
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("ProductToCustomer")]
    public partial class ProductToCustomer
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }

        public int customerID { get; set; }

        public int productID { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual Product Product { get; set; }
    }
}
