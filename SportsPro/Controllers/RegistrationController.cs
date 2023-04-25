using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportsPro.Models;
using System.Collections.Generic;
using System.Linq;

namespace SportsPro.Controllers
{
    public class RegistrationController : Controller
    {
        private SportsProContext context;

        public RegistrationController(SportsProContext ctx)
        {
            context = ctx;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetCustomer()
        {
            HttpContext.Session.Remove("selectedCustomer");

            List<Customer> customers;

            customers = context.Customers.Where(p => p.Active == true).OrderBy(p => p.FirstName).ToList();

            return View(customers);
        }

        public IActionResult GetRegistrations(int id = 0)
        {
            int customerID = 0;

            if (id == 0)
            {
                customerID = HttpContext.Session.GetInt32("selectedCustomer") ?? 0;
            }
            else
            {
                customerID = id;
                HttpContext.Session.SetInt32("selectedCustomer", id);
            }
            Customer customer = context.Customers.Find(customerID);

            List<Product> products = context.Products.ToList();
            List<Product> productsQuery = context.Products.ToList();
            List<Registrations> registrations = context.Registrations.Where(c => c.CustomerID == customerID).ToList();
            var result = from p in productsQuery
                         join reg in registrations
                         on p.ProductID equals reg.ProductID

                         select new
                         {
                             p.ProductID,
                             p.YearlyPrice,
                             p.Name,
                             p.ReleaseDate,
                             p.ProductCode
                         };

            List<Product> registeredProducts = new List<Product>();

            foreach (var r in result)
            {
                Product product = new Product();
                product.ProductID = r.ProductID;
                product.ProductCode = r.ProductCode;
                product.YearlyPrice = r.YearlyPrice;
                product.Name = r.Name;
                product.ReleaseDate = r.ReleaseDate;

                registeredProducts.Add(product);

                var itemToRemove = products.Single(prod => prod.ProductID == product.ProductID);
                products.Remove(itemToRemove);
            }

            RegistrationsViewModel vm = new RegistrationsViewModel(customer, registeredProducts, products);

            return View(vm);
        }

        [HttpPost]
        public IActionResult AddRegistration(int ProductID, int CustomerID)
        {
            Registrations registration = new Registrations();
            registration.ProductID = ProductID;
            registration.CustomerID = CustomerID;

            context.Registrations.Add(registration);
            context.SaveChanges();

            return RedirectToAction("GetRegistrations");
        }

        [HttpPost]
        public IActionResult DeleteRegistration(int ProductID, int CustomerID)
        {
            Registrations registration = new Registrations();
            registration.ProductID = ProductID;
            registration.CustomerID = CustomerID;

            context.Registrations.Remove(registration);
            context.SaveChanges();

            return RedirectToAction("GetRegistrations");
        }

    }
}
