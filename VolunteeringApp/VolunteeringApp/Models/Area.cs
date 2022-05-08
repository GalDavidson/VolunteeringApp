using System;
using System.Collections.Generic;
using System.Text;

namespace VolunteeringApp.Models
{
    public partial class Area
    {
        public Area()
        {
            DailyEvents = new List<DailyEvent>();
        }

        public int AreaId { get; set; }
        public string AreaName { get; set; }

        public virtual List<DailyEvent> DailyEvents { get; set; }
    }
}
