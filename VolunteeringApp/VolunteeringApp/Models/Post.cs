using System;
using System.Collections.Generic;
using System.Text;
using VolunteeringApp.Services;

namespace VolunteeringApp.Models
{
    public partial class Post
    {
        public Post()
        {
            OccupationalAreasOfPosts = new List<OccupationalAreasOfPost>();
            PicturesOfPosts = new List<PicturesOfPost>();
        }

        public int PostId { get; set; }
        public DateTime? ActionDate { get; set; }
        public string Caption { get; set; }
        public int? AssociationId { get; set; }
        public int? EventId { get; set; }
        public string Pic
        {
            get
            {
                VolunteeringAPIProxy proxy = VolunteeringAPIProxy.CreateProxy();
                string url = $"{proxy.GetBasePhotoUri()}P{this.PostId}.jpg";
                return url;
            }
        }

        public virtual Association Association { get; set; }
        public virtual DailyEvent Event { get; set; }
        public virtual List<OccupationalAreasOfPost> OccupationalAreasOfPosts { get; set; }
        public virtual List<PicturesOfPost> PicturesOfPosts { get; set; }
    }
}
