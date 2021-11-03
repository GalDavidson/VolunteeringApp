using System;
using System.Collections.Generic;
using System.Text;

namespace VolunteeringApp.Models
{
    public partial class BranchesOfAssociation
    {
        public int AssociationId { get; set; }
        public int BranchId { get; set; }

        public virtual Association Association { get; set; }
        public virtual Branch Branch { get; set; }
    }
}
