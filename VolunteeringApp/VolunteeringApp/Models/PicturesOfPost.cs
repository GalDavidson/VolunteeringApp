using System;
using System.Collections.Generic;
using System.Text;

namespace VolunteeringApp.Models
{
    public partial class PicturesOfPost
    {
        public int PicId { get; set; }
        public string PicUrl { get; set; }
        public int? PostId { get; set; }

        public virtual Post Post { get; set; }
    }
}
