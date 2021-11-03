using System;
using System.Collections.Generic;
using System.Text;

namespace VolunteeringApp.Models
{
    public partial class OccupationalAreasOfAssociation
    {
        public int AssociationId { get; set; }
        public int OccupationalAreaId { get; set; }

        public virtual Association Association { get; set; }
        public virtual OccupationalArea OccupationalArea { get; set; }
    }
}
