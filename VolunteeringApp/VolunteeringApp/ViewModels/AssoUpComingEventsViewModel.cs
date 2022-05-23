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
    class AssoUpComingEventsViewModel: INotifyPropertyChanged
    {
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region events
        private List<DailyEvent> allEvents;
        private ObservableCollection<DailyEvent> upComingEvents;
        public ObservableCollection<DailyEvent> UpComingEvents
        {
            get
            {
                return this.upComingEvents;
            }
            set
            {
                if (this.upComingEvents != value)
                {
                    this.upComingEvents = value;
                    OnPropertyChanged("UpComingEvents");
                }
            }
        }
        private ObservableCollection<DailyEvent> pastEvents;
        public ObservableCollection<DailyEvent> PastEvents
        {
            get
            {
                return this.pastEvents;
            }
            set
            {
                if (this.pastEvents != value)
                {
                    this.pastEvents = value;
                    OnPropertyChanged("PastEvents");
                }
            }
        }

        private void InitEvents()
        {
            App theApp = (App)App.Current;
            this.allEvents = new List<DailyEvent>(theApp.LookupTables.Events);

            foreach (DailyEvent e in allEvents)
            {
                TimeSpan ts = (TimeSpan)(e.StartTime- DateTime.Now);
                if (ts.TotalMinutes < 0 || ts.TotalMinutes == 0)
                    PastEvents.Add(e);
                else
                    UpComingEvents.Add(e);
                
            }
        }
        #endregion

        #region Navigate To Up Coming Event Page
        public ICommand SelectionEventChanged => new Command<DailyEvent>(OnSelectionEventChanged);
        public void OnSelectionEventChanged(DailyEvent e)
        {
            Page eventPage = new ShowEventPage();
            ShowEventViewModel eventContext = new ShowEventViewModel
            {
                EventName = e.EventName,
                EventLocation = e.EventLocation,
                EventDate = (DateTime)e.StartTime,
                StartTime = ((DateTime)e.StartTime).TimeOfDay,
                EndTime = ((DateTime)e.EndTime).TimeOfDay,
                Caption = e.Caption,
                VolunteersList = new ObservableCollection<VolunteersInEvent>(e.VolunteersInEvents)
            };
            eventPage.BindingContext = eventContext;
            if (NavigateToPageEvent != null)
                NavigateToPageEvent(eventPage);

        }

        public Action<Page> NavigateToPageEvent;

        #endregion

        #region Edit or delete event
        public ICommand EditEventCommand => new Command<DailyEvent>(EditFromEvent);
        public async void EditFromEvent(DailyEvent e)
        {
            Page p = new NavigationPage(new Views.UpdateEventPage(e));
            App.Current.MainPage = p;
        }
        #endregion

        public AssoUpComingEventsViewModel()
        {
            UpComingEvents = new ObservableCollection<DailyEvent>();
            PastEvents = new ObservableCollection<DailyEvent>();
            InitEvents();
        }
    }
}
