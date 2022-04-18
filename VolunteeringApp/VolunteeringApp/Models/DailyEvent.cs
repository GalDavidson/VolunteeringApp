using System;
using System.Collections.Generic;
using System.Text;

namespace VolunteeringApp.Models
{
    public partial class DailyEvent
    {
        public DailyEvent()
        {
            //OccupationalAreasOfPosts = new List<OccupationalAreasOfPost>();
            //PicturesOfEvents = new List<PicturesOfEvent>();
            Posts = new List<Post>();
            VolunteersInEvents = new List<VolunteersInEvent>();
        }

        public int EventId { get; set; }
        public string EventLocation { get; set; }
        public string Caption { get; set; }
        public int? AssociationId { get; set; }
        public DateTime? ActionDate { get; set; }
        public string EventName { get; set; }
        public DateTime EventDate { get; set; }

        public virtual Association Association { get; set; }
        //public virtual List<PicturesOfEvent> PicturesOfEvents { get; set; }
        public virtual List<Post> Posts { get; set; }
        public virtual List<VolunteersInEvent> VolunteersInEvents { get; set; }
    }
}
