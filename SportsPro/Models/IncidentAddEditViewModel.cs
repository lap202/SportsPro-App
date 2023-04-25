using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsPro.Models
{
    public class IncidentAddEditViewModel
    {
        public List<Customer> Customers { get; set; }
        public List<Product> Products { get; set; }
        public List<Technician> Technicians { get; set; }

        public Incident currentIncident;

        public string Action;

        public IncidentAddEditViewModel() { }

        public IncidentAddEditViewModel(List<Customer> cs, List<Product> p, List<Technician> ts, Incident ci, String action)  {
            Customers = cs;
            Products = p;
            Technicians = ts;
            currentIncident = ci;
            Action = action;
        }
    }
}
