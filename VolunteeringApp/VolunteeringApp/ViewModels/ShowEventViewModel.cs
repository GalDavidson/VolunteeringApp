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
        
        ICommand MoveToVolPageCommand => new Command<VolunteersInEvent>(VolPageCommand);

        public void VolPageCommand(VolunteersInEvent v)
        {
            //Volunteer vol = v.Volunteer;

            //int age = 0;
            //age = DateTime.Now.Subtract(vol.BirthDate).Days;
            //age = age / 365;

            //List<VolunteersInEvent> lst = vol.VolunteersInEvents;

            //ShowVolunteerViewModel volContext = new ShowVolunteerViewModel()
            //{
            //    FName = vol.FName,
            //    LName = vol.LName,
            //    UserName = vol.UserName,
            //    Age = age,
            //    RatingNum= v.RatingNum,
            //    TotalEvents = lst.Count
            //};
            //Page volPage = new VolProfilePage(volContext);

            //if (NavigateToPageEvent != null)
            //    NavigateToPageEvent(volPage);

            Page p = new NavigationPage(new Views.AllEventsPage());
            App.Current.MainPage = p;
        }

        public Action<Page> NavigateToPageEvent;
    }
}
