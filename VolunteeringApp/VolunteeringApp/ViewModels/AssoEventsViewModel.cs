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
    class AssoEventsViewModel: INotifyPropertyChanged
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

        private async void InitEvents()
        {
            UpComingEvents.Clear();
            PastEvents.Clear();
            VolunteeringAPIProxy proxy = VolunteeringAPIProxy.CreateProxy();
            this.allEvents = await proxy.GetEvents();
            
            App theApp = (App)App.Current;
            Association current = (Association)theApp.CurrentUser;

            foreach (DailyEvent e in allEvents)
            {
                if (e.AssociationId == current.AssociationId)
                {
                    TimeSpan ts = (TimeSpan)(e.StartTime - DateTime.Now);
                    if (ts.TotalMinutes < 0 || ts.TotalMinutes == 0)
                        PastEvents.Add(e);
                    else
                        UpComingEvents.Add(e);
                }
            }
        }
        #endregion

        #region Navigate To Event Page

        private DailyEvent selectedDailyEvent;
        public DailyEvent SelectedDailyEvent
        {
            get
            {
                return this.selectedDailyEvent;
            }
            set
            {

                if (value != this.selectedDailyEvent)
                {
                    this.selectedDailyEvent = value;
                    OnSelectionEventChanged(value);
                    OnPropertyChanged("SelectedDailyEvent");
                }
                
            }
        }
        public void OnSelectionEventChanged(DailyEvent e)
        {
            if (e == null)
                return;
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
            
            //selectedDailyEvent = null;
            //OnPropertyChanged("SelectedDailyEvent");
        }

        public Action<Page> NavigateToPageEvent;
        #endregion

        #region Edit and delete event
        public ICommand EditEventCommand => new Command<DailyEvent>(EditEvent);
        public void EditEvent(DailyEvent e)
        {
            Page p = new NavigationPage(new Views.UpdateEventPage(e));
            App.Current.MainPage = p;
        }

        public ICommand DeleteEventCommand => new Command<DailyEvent>(DelEvent);

        public async void DelEvent(DailyEvent e)
        {
            bool result = await App.Current.MainPage.DisplayAlert("את.ה בטוח.ה?", "", "כן", "לא", FlowDirection.RightToLeft);
            if (result)
            {
                VolunteeringAPIProxy proxy = VolunteeringAPIProxy.CreateProxy();
                bool ok = await proxy.DelEvent(e);
                //((App)App.Current).TriggerDeleteEvent();

                if (ok)
                {
                    UpComingEvents.Remove(e);
                    Page p = new NavigationPage(new Views.AssoEventsNavigationPage());
                    App.Current.MainPage = p;
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

        public AssoEventsViewModel()
        {
            UpComingEvents = new ObservableCollection<DailyEvent>();
            PastEvents = new ObservableCollection<DailyEvent>();
            InitEvents();
            //((App)App.Current).DeleteEvent += InitEvents;
        }
    }
}
