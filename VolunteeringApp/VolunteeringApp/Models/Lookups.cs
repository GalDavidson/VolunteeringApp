using System;
using System.Collections.Generic;
using System.Text;

namespace VolunteeringApp.Models
{
    public class Lookups
    {
        public List<OccupationalArea> OccupationalAreas { get; set; }
        public List<Branch> Branches { get; set; }
        public List<Gender> Genders { get; set; }
        public List<Association> Associations { get; set; }
        public List<Volunteer> Volunteers { get; set; }
        public List<DailyEvent> Events { get; set; }
        public List<VolunteersInEvent> VolsInEvents { get; set; }
    }
}
