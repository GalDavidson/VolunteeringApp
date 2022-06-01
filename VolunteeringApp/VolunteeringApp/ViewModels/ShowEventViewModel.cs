using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;
using VolunteeringApp.Services;
using VolunteeringApp.Models;
using VolunteeringApp.Views;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;
using Xamarin.Essentials;

namespace VolunteeringApp.ViewModels
{
    public class ShowEventViewModel: INotifyPropertyChanged
    {

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        public string EventName { get; set; }
        public string EventLocation { get; set; }
        public DateTime EventDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public string Caption { get; set; }
        public ObservableCollection<VolunteersInEvent> VolunteersList { get; set; }
        private int ratingValue;
        public int RatingValue
        {
            get
            {
                return this.ratingValue;
            }
            set
            {
                this.ratingValue = value;
                OnPropertyChanged("RatingValue");
            }
        }

        private List<int> ratingValueLst;
        public List<int> RatingValueLst
        {
            get
            {
                return this.ratingValueLst;
            }
            set
            {
                this.ratingValueLst = value;
                OnPropertyChanged("RatingValueLst");
            }
        }
    }
}
