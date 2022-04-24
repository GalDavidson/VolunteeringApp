using System;
using System.Collections.Generic;
using System.Text;
using VolunteeringApp.Services;

namespace VolunteeringApp.Models
{
    public partial class Association
    {
        public Association()
        {
            BranchesOfAssociations = new List<BranchesOfAssociation>();
            DailyEvents = new List<DailyEvent>();
            OccupationalAreasOfAssociations = new List<OccupationalAreasOfAssociation>();
        }

        public int AssociationId { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string InformationAbout { get; set; }
        public string PhoneNum { get; set; }
        public string Pass { get; set; }
        public DateTime? ActionDate { get; set; }
        public string ProfilePic 
        { 
            get
            {
                VolunteeringAPIProxy proxy = VolunteeringAPIProxy.CreateProxy();
                string url = $"{proxy.GetBasePhotoUri()}A{this.AssociationId}.jpg";
                return url;
            }
        }

        public virtual List<BranchesOfAssociation> BranchesOfAssociations { get; set; }
        public virtual List<DailyEvent> DailyEvents { get; set; }
        public virtual List<OccupationalAreasOfAssociation> OccupationalAreasOfAssociations { get; set; }
    }
}
