using System;
using System.Collections.Generic;
using System.Text;

namespace VolunteeringApp.Models
{
    public partial class Comment
    {
        public int CommentId { get; set; }
        public string CommentText { get; set; }
        public int EventId { get; set; }
        public int VolunteerId { get; set; }

        public virtual VolunteersInEvent VolunteersInEvent { get; set; }
    }
}
