﻿using System;
using System.Collections.Generic;
using System.Text;

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

        public virtual Association Association { get; set; }
        public virtual DailyEvent Event { get; set; }
        public virtual List<OccupationalAreasOfPost> OccupationalAreasOfPosts { get; set; }
        public virtual List<PicturesOfPost> PicturesOfPosts { get; set; }
    }
}
