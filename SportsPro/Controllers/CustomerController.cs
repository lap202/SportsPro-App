using Microsoft.AspNetCore.Mvc;
using SportsPro.Models;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;

namespace SportsPro.Controllers
{
    public class CustomerController : Controller
    {

        private SportsProContext context;

        public CustomerController(SportsProContext ctx)
        {
            context = ctx;           
        }

        

        [Route("[controller]s")]
        public IActionResult List()
        {
            List<Customer> customers;

            customers = context.Customers.Where(p => p.Active == true).OrderBy(p => p.FirstName).ToList();

            return View("Customers", customers);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewData["Action"] = "Update";

            ViewBag.Countries = context.Countries.ToList();
            Customer customer = context.Customers.Find(id);

            return View("EditCustomer", customer);
        }

        [HttpPost]
        public IActionResult Update(Customer customer)
        {
            if (ModelState.IsValid)
            {
                context.Customers.Update(customer);
                context.SaveChanges();
                TempData["Success"] = customer.FullName + " was successfully Updated!";

                return RedirectToAction("List", "Customer");
            }

            return View("EditCustomer", customer);
        }

        [HttpGet]
        public IActionResult Add()
        {
            ViewData["Action"] = "Add";
            ViewBag.Countries = context.Countries.ToList();

            Customer customer = new Customer();

            return View("EditCustomer", customer);
        }

        [HttpPost]
        public IActionResult Add(Customer customer)
        {
            if (ModelState.IsValid)
            {

                context.Customers.Add(customer);
                TempData["Success"] = customer.FullName + " was successfully Added";

                context.SaveChanges();
                return RedirectToAction("List", "Customer");
            }


            return View("EditCustomer", customer);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            Customer customer = context.Customers.Find(id);

            return View("DeleteCustomer", customer);
        }
        [HttpPost]
        public IActionResult Delete(Customer c)
        {
            //code to delete record utilizing the active flag
            var customer = context.Customers.Single(cst => cst.CustomerID == c.CustomerID);

            customer.Active= false;
            context.SaveChanges();

            TempData["Success"] = customer.FullName + " was successfully removed!";

            return RedirectToAction("List", "Customer");
        }

    }
}
