using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsPro.Models
{
    public class IncidentManagerViewModel
    {
        public List<Incident> Incidents { get; set; }
        public string Filter { get; set; }

        public IncidentManagerViewModel() { }
        public IncidentManagerViewModel(List<Incident> incidents, string filter)
        {
            Incidents = incidents;
            Filter = filter;
        }

    }
}
