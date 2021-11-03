using System;
using System.Collections.Generic;


namespace VolunteeringApp.Models
{
    public partial class Branch
    {
        public Branch()
        {
            BranchesOfAssociations = new List<BranchesOfAssociation>();
        }

        public int BranchId { get; set; }
        public string BranchLocation { get; set; }

        public virtual List<BranchesOfAssociation> BranchesOfAssociations { get; set; }
    }
}
