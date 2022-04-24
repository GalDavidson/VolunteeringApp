using System;
using System.Collections.Generic;
using System.Text;

namespace VolunteeringApp.Models
{
    public partial class OccupationalAreasOfEvent
    {
        public int EventId { get; set; }
        public int OccupationalAreaId { get; set; }

        public virtual DailyEvent Event { get; set; }
        public virtual OccupationalArea OccupationalArea { get; set; }
    }
}
