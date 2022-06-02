using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace VolunteeringApp.Models
{
    public partial class VolunteersInEvent : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        public VolunteersInEvent()
        {
            Comments = new List<Comment>();
        }

        public int EventId { get; set; }
        public int VolunteerId { get; set; }
        public int RatingNum { get; set; }
        public string WrittenRating { get; set; }
        public DateTime? ActionDate { get; set; }

        public virtual DailyEvent Event { get; set; }
        public virtual Volunteer Volunteer { get; set; }
        public virtual List<Comment> Comments { get; set; }
    }
}
