using System;
using System.Collections.Generic;
using System.Text;
using VolunteeringApp.Services;

namespace VolunteeringApp.Models
{
    public partial class DailyEvent
    {
        public DailyEvent()
        {
            //OccupationalAreasOfPosts = new List<OccupationalAreasOfPost>();
            //PicturesOfEvents = new List<PicturesOfEvent>();
            //Posts = new List<Post>();
            OccupationalAreasOfEvents = new List<OccupationalAreasOfEvent>();
            VolunteersInEvents = new List<VolunteersInEvent>();
        }

        public int EventId { get; set; }
        public string EventLocation { get; set; }
        public int? AssociationId { get; set; }
        public DateTime? ActionDate { get; set; }
        public string EventName { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public int? AreaId { get; set; }
        public string Caption { get; set; }

        public string Pic
        {
            get
            {
                VolunteeringAPIProxy proxy = VolunteeringAPIProxy.CreateProxy();
                string url = $"{proxy.GetBasePhotoUri()}E{this.EventId}.jpg";
                return url;
            }
        }

        public virtual Area Area { get; set; }
        public virtual Association Association { get; set; }
        //public virtual List<PicturesOfEvent> PicturesOfEvents { get; set; }
        public virtual List<OccupationalAreasOfEvent> OccupationalAreasOfEvents { get; set; }
        public virtual List<VolunteersInEvent> VolunteersInEvents { get; set; }
    }
}
