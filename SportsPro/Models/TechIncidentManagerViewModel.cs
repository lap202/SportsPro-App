using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsPro.Models
{
    public class TechIncidentManagerViewModel
    {
        public List<Incident> Incidents { get; set; }
        public Technician tech { get; set; }
        public string Filter { get; set; }

        public TechIncidentManagerViewModel() { }
        public TechIncidentManagerViewModel(List<Incident> incidents,Technician technician, string filter)
        {
            Incidents = incidents;
            tech = technician;
            Filter = filter;
        }

    }
}
