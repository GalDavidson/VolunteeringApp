using System;
using System.Collections.Generic;
using System.Text;

namespace VolunteeringApp.Models
{
    public partial class OccupationalAreasOfPost
    {
        public int PostId { get; set; }
        public int OccupationalAreaId { get; set; }

        public virtual OccupationalArea OccupationalArea { get; set; }
        public virtual Post Post { get; set; }
    }
}
