namespace MTS_CRM.DTO
{
    public class CompanyDTO : CustomerDTO
    {
        public int ID { get; set; }
        public string name { get; set; }
        public string CVR { get; set; }
    }
}
