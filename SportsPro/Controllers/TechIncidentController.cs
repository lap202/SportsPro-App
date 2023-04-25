using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SportsPro.Models;
using System.Collections.Generic;
using System.Linq;

namespace SportsPro.Controllers
{
    public class TechIncidentController : Controller
    {
        private SportsProContext context;

        public TechIncidentController(SportsProContext ctx)
        {
            context = ctx;
        }
        [HttpGet]
        public IActionResult List()
        {
            int technicianID = HttpContext.Session.GetInt32("selectedTechnician") ?? 0;

            List<Incident> incidents = context.Incidents
                .Where(p => p.TechnicianID == technicianID)
                .OrderBy(p => p.IncidentID).ToList();

            foreach (Incident i in incidents)
            {
                i.Customer = context.Customers.Find(i.CustomerID);
                i.Product = context.Products.Find(i.ProductID);
            }

            Technician tech = context.Technicians.Find(technicianID);

            TechIncidentManagerViewModel model = new TechIncidentManagerViewModel(incidents, tech, "");

            return View("Incidents", model);
        }

        [HttpPost]
        public IActionResult List(Technician technician)
        {
            HttpContext.Session.SetInt32("selectedTechnician", technician.TechnicianID);

            List<Incident> incidents = context.Incidents
                .Where(p => p.TechnicianID == technician.TechnicianID)
                .OrderBy(p => p.IncidentID).ToList();

            foreach (Incident i in incidents)
            {
                i.Customer = context.Customers.Find(i.CustomerID);
                i.Product = context.Products.Find(i.ProductID);
            }

            Technician tech = context.Technicians.Find(technician.TechnicianID);

            TechIncidentManagerViewModel model = new TechIncidentManagerViewModel(incidents, tech, "");

            return View("Incidents", model);
        }

        [HttpGet]
        public IActionResult Get()
        {
            HttpContext.Session.Remove("selectedTechnician");

            var Technicians = context.Technicians.OrderBy(t => t.Name).ToList();

            return View("GetTechnician", Technicians);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            Incident incident = context.Incidents.Find(id);

            var Customers = context.Customers.Where(c => c.Active || c.CustomerID == incident.CustomerID).ToList();
            var Products = context.Products.ToList();
            var Technicians = context.Technicians.Where(t => t.Active || t.TechnicianID == incident.TechnicianID).ToList();

            IncidentAddEditViewModel model = new IncidentAddEditViewModel(Customers, Products, Technicians, incident, "Save");


            return View("EditTechIncident", model);
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

            IncidentAddEditViewModel model = new IncidentAddEditViewModel(Customers, Products, Technicians, incident, "Save");

            return View("EditTechIncident", model);
        }
    }
}
