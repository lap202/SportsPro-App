using System.Collections.Generic;

namespace SportsPro.Models
{
    public class RegistrationsViewModel
    {
        public Customer Customer { get; set; }
        public List<Product> RegisteredProducts { get; set; }
        public List<Product> Products { get; set; }

        public RegistrationsViewModel(Customer customer, List<Product> registeredProducts, List<Product> products)
        {
            Customer = customer;
            RegisteredProducts = registeredProducts;
            Products = products;
        }
    }
}
