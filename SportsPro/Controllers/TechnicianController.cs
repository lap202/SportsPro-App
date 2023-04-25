using Microsoft.AspNetCore.Mvc;
using SportsPro.Models;
using System.Collections.Generic;
using System.Linq;

namespace SportsPro.Controllers
{
    public class TechnicianController : Controller
    {
        private SportsProContext context;

        public TechnicianController(SportsProContext ctx)
        {
            context = ctx;
        }

        [Route("[controller]s")]
        public IActionResult List()
        {
            List<Technician> technicians;

            technicians = context.Technicians
                .Where(t => t.Active == true)
                .OrderBy(t => t.Name)
                .ToList();

            return View("Technicians", technicians);
        }

        [HttpGet]
        public IActionResult Add()
        {
            ViewData["Action"] = "Add";

            return View("EditTechnician", new Technician());
        }

        [HttpPost]
        public IActionResult Add(Technician technician)
        {
            if (ModelState.IsValid)
            {
                //code to add records
                context.Technicians.Add(technician);
                context.SaveChanges();
                TempData["Success"] = technician.Name + " was successfully Added";

                return RedirectToAction("List", "Technician");
            }

                return View("editTechnician", technician);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewData["Action"] = "Update";

            Technician technician = context.Technicians.Find(id);

            return View("EditTechnician", technician);
        }

        [HttpPost]
        public IActionResult Update(Technician technician)
        {
            if (ModelState.IsValid)
            {
                //code to update records
                context.Technicians.Update(technician);
                TempData["Success"] = technician.Name + " was successfully Updated!";
                context.SaveChanges();

                return RedirectToAction("List", "Technician");
            }


            return View("editTechnician", technician);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            Technician technician = context.Technicians.Find(id);

            return View("DeleteTechnician", technician);
        }

        [HttpPost]
        public IActionResult Delete(Technician t)
        {
            //code to delete record utilizing the active flag
            var technician = context.Technicians.Single(cst => cst.TechnicianID == t.TechnicianID);

            technician.Active = false;

            context.SaveChanges();

            TempData["Success"] = technician.Name + " was successfully removed!";

            return RedirectToAction("List", "Technician");
        }
    }
}

