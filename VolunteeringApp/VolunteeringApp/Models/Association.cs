using System;
using System.Collections.Generic;
using System.Text;


namespace VolunteeringApp.Models
{
    public partial class Association
    {
        public Association()
        {
            BranchesOfAssociations = new List<BranchesOfAssociation>();
            DailyEvents = new List<DailyEvent>();
            OccupationalAreasOfAssociations = new List<OccupationalAreasOfAssociation>();
            Posts = new List<Post>();
        }

        public int AssociationId { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string InformationAbout { get; set; }
        public string PhoneNum { get; set; }
        public string Pass { get; set; }
        public DateTime? ActionDate { get; set; }
        public string ProfilePic { get; set; }

        public virtual List<BranchesOfAssociation> BranchesOfAssociations { get; set; }
        public virtual List<DailyEvent> DailyEvents { get; set; }
        public virtual List<OccupationalAreasOfAssociation> OccupationalAreasOfAssociations { get; set; }
        public virtual List<Post> Posts { get; set; }
    }
}
