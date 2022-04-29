namespace MTS_CRM.DTO
{
    public class CustomerDTO
    {
        public int CustID { get; set; }

        public string email { get; set; }

        public string phoneNo { get; set; }

        public string yearOfJoin { get; set; }

        public LocationDTO Location { get; set; }
    }
}
