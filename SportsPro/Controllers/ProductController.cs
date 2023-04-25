using Microsoft.AspNetCore.Mvc;
using SportsPro.Models;
using System.Linq;
using System.Collections.Generic;

namespace SportsPro.Controllers
{
    public class ProductController : Controller
    {
        private SportsProContext context;

        public ProductController(SportsProContext ctx)
        {
            context = ctx;
        }

        [Route("[controller]s")]
        public ViewResult List()
        {
            List<Product> products;

            products = context.Products
                .OrderBy(p => p.ReleaseDate)
                .ToList();

            return View("Products", products);
        }

        [HttpGet]
        public ViewResult Edit(int id)
        {
            ViewData["Action"] = "Update";

            Product product = context.Products.Find(id);

            return View("EditProduct", product);
        }

        [HttpPost]
        public RedirectToActionResult Update(Product product)
        {
            if (ModelState.IsValid)
            {
                context.Products.Update(product);
                context.SaveChanges();
                TempData["Success"] = product.Name + " was successfully Updated!";

                return RedirectToAction("List", "Product");
            }

            return RedirectToAction("EditProduct", "Product");
        }

        [HttpGet]
        public ViewResult Add()
        {
            ViewData["Action"] = "Add";

            Product product = new Product();

            return View("EditProduct", product);
        }

        [HttpPost]
        public RedirectToActionResult Add(Product product)
        {
            if (ModelState.IsValid)
            {

                context.Products.Add(product);
                TempData["Success"] = product.Name + " was successfully Added";

                context.SaveChanges();
                return RedirectToAction("List", "Product");
            }

            
            return RedirectToAction("EditProduct", "Product");
        }

        [HttpGet]
        public ViewResult Delete(int id)
        {
            Product product = context.Products.Find(id);

            return View("DeleteProduct", product);
        }

        [HttpPost]
        public RedirectToActionResult Delete(Product product)
        {
            
            context.Products.Remove(product);
            context.SaveChanges();

            TempData["Success"] = product.Name + " was successfully Removed!";

            return RedirectToAction("List", "Product");
        }

    }
}
