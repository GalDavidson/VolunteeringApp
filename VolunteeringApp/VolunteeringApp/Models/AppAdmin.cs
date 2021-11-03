using System;
using System.Collections.Generic;
using System.Text;

namespace VolunteeringApp.Models
{
    public partial class AppAdmin
    {
        public int AdminId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Pass { get; set; }
        public string AdminName { get; set; }
    }
}
