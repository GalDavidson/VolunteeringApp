using System;
using System.Collections.Generic;
using System.Text;

namespace VolunteeringApp.Models
{
    public partial class OccupationalArea
    {
        public OccupationalArea()
        {
            OccupationalAreasOfAssociations = new List<OccupationalAreasOfAssociation>();
        }

        public int OccupationalAreaId { get; set; }
        public string OccupationName { get; set; }

        public virtual List<OccupationalAreasOfAssociation> OccupationalAreasOfAssociations { get; set; }
        public virtual List<OccupationalAreasOfPost> OccupationalAreasOfPosts { get; set; }
    }
}
