using System;
using System.Collections.Generic;
using System.Text;
using VolunteeringApp.Services;

namespace VolunteeringApp.Models
{
    public partial class Volunteer
    {
        public Volunteer()
        {
            VolunteersInEvents = new List<VolunteersInEvent>();
        }

        public int VolunteerId { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Pass { get; set; }
        public string ProfilePic
        {
            get
            {
                VolunteeringAPIProxy proxy = VolunteeringAPIProxy.CreateProxy();
                string url = $"{proxy.GetBasePhotoUri()}V{this.VolunteerId}.jpg";
                return url;
            }
        }

        public int? GenderId { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime? ActionDate { get; set; }

        public virtual Gender Gender { get; set; }
        public virtual List<VolunteersInEvent> VolunteersInEvents { get; set; }
    }
}
