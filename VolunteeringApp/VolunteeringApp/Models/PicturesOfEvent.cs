using System;
using System.Collections.Generic;
using System.Text;

namespace VolunteeringApp.Models
{
    public partial class PicturesOfEvent
    {
        public int PicId { get; set; }
        public string PicUrl { get; set; }
        public int? EventId { get; set; }

        public virtual DailyEvent Event { get; set; }
    }
}
