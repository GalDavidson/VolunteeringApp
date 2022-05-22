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
    class VolunteerEventsViewModel: INotifyPropertyChanged
    {
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region כל האירועים של משתמש
        public ObservableCollection<DailyEvent> eventsList;
        public ObservableCollection<DailyEvent> EventsList
        {
            get
            {
                return this.eventsList;
            }
            set
            {
                if (this.eventsList != value)
                {
                    this.eventsList = value;
                    OnPropertyChanged("EventsList");
                }
            }
        }
        private void CreateEventsCollection()
        {
            App theApp = (App)App.Current;
            List<DailyEvent> events = theApp.LookupTables.Events;
            Volunteer vol = (Volunteer)theApp.CurrentUser;

            foreach (DailyEvent e in events) 
            {
                VolunteersInEvent found = e.VolunteersInEvents.Where(v => v.VolunteerId == vol.VolunteerId).FirstOrDefault();
                if (found != null)
                    EventsList.Add(e);
            }
            this.EventsList = new ObservableCollection<DailyEvent>(this.EventsList);
        }

        //NavigateToEventPage
        public ICommand SelectionAssociationChanged => new Command<DailyEvent>(OnSelectionEventChanged);
        public void OnSelectionEventChanged(DailyEvent e)
        {
            App theApp = (App)App.Current;
            Volunteer vol = (Volunteer)theApp.CurrentUser;

            ObservableCollection<VolunteersInEvent> lst = new ObservableCollection<VolunteersInEvent>();
            foreach (VolunteersInEvent v in e.VolunteersInEvents)
            {
                if (v.VolunteerId != vol.VolunteerId)
                    lst.Add(v);
            }

            Page eventPage = new ShowEventPage();
            ShowEventViewModel eventContext = new ShowEventViewModel
            {
                EventName = e.EventName,
                EventLocation = e.EventLocation,
                EventDate = (DateTime)e.EventDate,
                StartTime = (TimeSpan)e.StartTime,
                EndTime = (TimeSpan)e.EndTime,
                Caption = e.Caption,
                VolunteersList = lst
            };
            eventPage.BindingContext = eventContext;
            if (NavigateToPageEvent != null)
                NavigateToPageEvent(eventPage);

        }

        //sign out of event
        public ICommand RemoveEventCommand => new Command<DailyEvent>(RemoveFromEvent);
        public async void RemoveFromEvent(DailyEvent e)
        {
            bool result = await App.Current.MainPage.DisplayAlert("את.ה בטוח.ה?", "", "כן", "לא", FlowDirection.RightToLeft);
            if (result)
            {
                App theApp = (App)App.Current;
                Volunteer vol = (Volunteer)theApp.CurrentUser;

                List<VolunteersInEvent> lst = new List<VolunteersInEvent>(e.VolunteersInEvents);
                e.VolunteersInEvents = new List<VolunteersInEvent>();
                foreach(VolunteersInEvent v in lst)
                {
                    if (v.VolunteerId == vol.VolunteerId)
                        e.VolunteersInEvents.Add(v);
                }

                VolunteeringAPIProxy proxy = VolunteeringAPIProxy.CreateProxy();
                bool ok = await proxy.RemoveVolFromEvent(e);

                if (ok)
                {
                    Console.WriteLine("yay");
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("שגיאה", "לא בוצע", "אישור");
                }
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("שגיאה", "לא בוצע", "אישור");
            }
        }
        #endregion

        public Action<Page> NavigateToPageEvent;


        public VolunteerEventsViewModel()
        {
            EventsList = new ObservableCollection<DailyEvent>();
            CreateEventsCollection();
        }
    }
}
