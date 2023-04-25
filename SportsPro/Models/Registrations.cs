namespace SportsPro.Models
{
    public class Registrations
    {
        public int CustomerID { get; set; }
        public int ProductID { get; set; }

        public Customer customer { get; set; }
        public Product product { get; set; }
    }
}
