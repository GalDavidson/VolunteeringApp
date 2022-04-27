using System;
using System.Collections.Generic;
using System.Text;

namespace VolunteeringApp.Models
{
    public partial class Region
    {
        public Region()
        {
            DailyEvents = new List<DailyEvent>();
        }

        public int RegionId { get; set; }
        public string RegionName { get; set; }

        public virtual List<DailyEvent> DailyEvents { get; set; }
    }
}
