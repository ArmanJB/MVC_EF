namespace MVC_EF.Models
{
    public class Address
    {
        public int AddressID { get; set; }
        public int ContactID { get; set; }
        public string Description { get; set; }

        public virtual Contact Contact { get; set; }
    }
}