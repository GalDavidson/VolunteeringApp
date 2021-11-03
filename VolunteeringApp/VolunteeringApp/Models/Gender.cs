using System;
using System.Collections.Generic;
using System.Text;

namespace VolunteeringApp.Models
{
    public partial class Gender
    {
        public Gender()
        {
            Volunteers = new List<Volunteer>();
        }

        public int GenderId { get; set; }
        public string GenderType { get; set; }

        public virtual List<Volunteer> Volunteers { get; set; }
    }
}
