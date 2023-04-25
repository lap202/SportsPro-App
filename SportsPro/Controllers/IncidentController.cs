using Microsoft.AspNetCore.Mvc;
using SportsPro.Models;
using System.Collections.Generic;
using System.Linq;

namespace SportsPro.Controllers
{
    public class IncidentController : Controller
    {
        private SportsProContext context;

        public IncidentController(SportsProContext ctx)
        {
            context = ctx;
        }

        [Route("[controller]s")]
        public IActionResult List(string filter)
        {
            List<Incident> incidents;
            if (filter == "noTech") { 
                incidents = context.Incidents.OrderBy(p => p.IncidentID).Where(p => p.TechnicianID == null).ToList();
            }
            else if(filter == "open")
            {
                incidents = context.Incidents.OrderBy(p => p.IncidentID).Where(p => p.DateClosed == null).ToList();
            }
            else
            {
                filter = "all";
                incidents = context.Incidents.OrderBy(p => p.IncidentID).ToList();
            }
            

            foreach (Incident i in incidents)
            {
                i.Customer = context.Customers.Find(i.CustomerID);
                i.Product = context.Products.Find(i.ProductID);
            }

            IncidentManagerViewModel model = new IncidentManagerViewModel(incidents, filter);

            return View("Incidents", model);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            Incident incident = context.Incidents.Find(id);
            
            var Customers = context.Customers.Where(c => c.Active || c.CustomerID == incident.CustomerID).ToList();
            var Products = context.Products.ToList();
            var Technicians = context.Technicians.Where(t => t.Active || t.TechnicianID == incident.TechnicianID).ToList();

            IncidentAddEditViewModel model = new IncidentAddEditViewModel(Customers, Products, Technicians, incident, "Update");


            return View("EditIncident", model);
        }

        [HttpPost]
        public IActionResult Update(Incident incident)
        {
            if (ModelState.IsValid)
            {
                context.Incidents.Update(incident);
                context.SaveChanges();
                TempData["Success"] = incident.Title + " was successfully Updated!";

                return RedirectToAction("List", "Incident");
            }

            var Customers = context.Customers.Where(c => c.Active || c.CustomerID == incident.CustomerID).ToList();
            var Products = context.Products.ToList();
            var Technicians = context.Technicians.Where(t => t.Active || t.TechnicianID == incident.TechnicianID).ToList();

            IncidentAddEditViewModel model = new IncidentAddEditViewModel(Customers, Products, Technicians, incident, "Update");

            return View("EditIncident", model);
        }

        [HttpGet]
        public IActionResult Add()
        {
            var Customers = context.Customers.Where(c => c.Active == true).ToList();
            var Products = context.Products.ToList();
            var Technicians = context.Technicians.Where(t => t.Active == true).ToList();

            Incident incident = new Incident();

            IncidentAddEditViewModel model = new IncidentAddEditViewModel(Customers, Products, Technicians, incident, "Add");

            return View("EditIncident", model);
        }

        [HttpPost]
        public IActionResult Add(Incident incident)
        {
            if (ModelState.IsValid)
            {

                context.Incidents.Add(incident);
                TempData["Success"] = incident.Title + " was successfully Added";

                context.SaveChanges();
                return RedirectToAction("List", "Incident");
            }

            var Customers = context.Customers.Where(c => c.Active == true).ToList();
            var Products = context.Products.ToList();
            var Technicians = context.Technicians.Where(t => t.Active == true).ToList();

            IncidentAddEditViewModel model = new IncidentAddEditViewModel(Customers, Products, Technicians, incident, "Add");

            return View("EditIncident", model);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            Incident incident = context.Incidents.Find(id);

            incident.Customer = context.Customers.Find(incident.CustomerID);
            incident.Product = context.Products.Find(incident.ProductID);

            return View("DeleteIncident", incident);
        }

        [HttpPost]
        public IActionResult Delete(Incident incident)
        {

            context.Incidents.Remove(incident);
            context.SaveChanges();

            TempData["Success"] = incident.Title + " was successfully Removed!";

            return RedirectToAction("List", "Incident");
        }

        [HttpGet]
        public IActionResult ListByTech()
        {
            return RedirectToAction("Get", "TechIncident");
        }

    }
}
