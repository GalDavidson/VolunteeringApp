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
    class AllEventsViewModel : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region כל האירועים
        private List<DailyEvent> allEvents;
        private ObservableCollection<DailyEvent> filteredEvents;
        public ObservableCollection<DailyEvent> FilteredEvents
        {
            get
            {
                return this.filteredEvents;
            }
            set
            {
                if (this.filteredEvents != value)
                {
                    this.filteredEvents = value;
                    OnPropertyChanged("FilteredEvents");
                }
            }
        }

        private void InitEvents()
        {
            IsRefresh = true;
            App theApp = (App)App.Current;
            this.allEvents = new List<DailyEvent>(theApp.LookupTables.Events);


            //Copy list to the filtered list
            this.filteredEvents = new ObservableCollection<DailyEvent>(this.allEvents);
            IsRefresh = false;
        }
        #endregion

        #region רשימת איזורים
        public List<Area> Areas { get; set; }

        private void CreateAreasCollection()
        {
            if (((App)App.Current).LookupTables != null)
            {
                Areas = ((App)App.Current).LookupTables.Areas;
                Areas.Add(new Area
                {
                    AreaId = Areas.Count + 1,
                    AreaName = "כל האיזורים"
                });
            }
        }

        private Area area;
        public Area Area
        {
            get => area;
            set
            {
                if (this.area != value)
                {
                    area = value;
                    OnPropertyChanged("Area");
                }


            }
        }
        #endregion

        #region רשימת תחומי עיסוק

        public List<OccupationalArea> OccupationalAreas { get; set; }

        private void CreateOccupationalAreasCollection()
        {
            if (((App)App.Current).LookupTables != null)
            {
                OccupationalAreas = new List<OccupationalArea>((((App)App.Current).LookupTables.OccupationalAreas).OrderBy(b => b.OccupationName));

                OccupationalAreas.Add(new OccupationalArea
                {
                    OccupationalAreaId = OccupationalAreas.Count + 1,
                    OccupationName = "כל סוגי ההתנדבות"
                });
            }
        }

        private OccupationalArea occupationalArea;
        public OccupationalArea OccupationalArea
        {
            get => occupationalArea;
            set
            {
                if (this.occupationalArea != value)
                {
                    occupationalArea = value;
                    OnPropertyChanged("OccupationalArea");
                }
            }
        }
        #endregion

        #region כפתור הרשמה לאירוע
        private bool isEnabled;
        public bool IsEnabled
        {
            get => isEnabled;
            set
            {
                if (this.isEnabled != value)
                {
                    isEnabled = value;
                    OnPropertyChanged("IsEnabled");
                }


            }
        }

        private bool isVisible;
        public bool IsVisible
        {
            get => isVisible;
            set
            {
                if (this.isVisible != value)
                {
                    isVisible = value;
                    OnPropertyChanged("IsVisible");
                }


            }
        }
        #endregion

        #region Refresh Events
        private bool isRefresh;
        public bool IsRefresh
        {
            get => isRefresh;
            set
            {
                if (this.isRefresh != value)
                {
                    this.isRefresh = value;
                    OnPropertyChanged(nameof(IsRefresh));
                }
            }
        }
        public ICommand RefreshEventsCommand => new Command(OnRefreshEvents);
        public void OnRefreshEvents()
        {
            InitEvents();
        }
        #endregion

        #region EventSelection
        private DailyEvent selectedEvent;
        public DailyEvent SelectedEvent
        {
            get
            {
                return this.selectedEvent;
            }
            set
            {
                if (this.selectedEvent != value)
                {
                    this.selectedEvent = value;
                    OnPropertyChanged("SelectedEvent");
                }
            }
        }
        #endregion


        public AllEventsViewModel()
        {
            this.Areas = new List<Area>();
            this.OccupationalAreas = new List<OccupationalArea>();
            this.FilteredEvents = new ObservableCollection<DailyEvent>();
            InitEvents();
            CreateAreasCollection();
            CreateOccupationalAreasCollection();
            this.RegisterToEventCommand = new Command(OnPress);
            this.FilterEventsCommand = new Command(OnFilter);
            this.CleanFilterEventsCommand = new Command(OnCleanFilter);

            App theApp = (App)App.Current;
            Object o = theApp.CurrentUser;

            if (o is Association || o is AppAdmin)
            {
                IsEnabled = false;
                IsVisible = false;
            }
            else
            {
                IsEnabled = true;
                IsVisible = true;
            }
        }

        public ICommand FilterEventsCommand { protected set; get; }

        private void OnFilter()
        {
            //Filter the list of contacts based on the search term
            if (this.allEvents == null)
            {
                return;
            }

            if (this.Area == null || this.Area.AreaName == "כל האיזורים") 
            {
                if (this.OccupationalArea == null || this.OccupationalArea.OccupationName == "כל סוגי ההתנדבות")
                {
                    return;
                }
                else
                {
                    this.FilteredEvents.Clear();
                    foreach (DailyEvent e in this.allEvents)
                    {
                        foreach (OccupationalAreasOfEvent o in e.OccupationalAreasOfEvents)
                        {
                            if (this.OccupationalArea.OccupationalAreaId == o.OccupationalAreaId)
                            {
                                this.FilteredEvents.Add(e);
                            }
                        }
                    }
                    this.FilteredEvents = new ObservableCollection<DailyEvent>((this.FilteredEvents).OrderBy(b => b.ActionDate));
                }
            }
            else
            {
                if (this.OccupationalArea == null)
                {
                    this.FilteredEvents.Clear();
                    foreach (DailyEvent e in this.allEvents)
                    {
                        if (e.AreaId == this.Area.AreaId)
                            this.FilteredEvents.Add(e);
                    }
                    this.FilteredEvents = new ObservableCollection<DailyEvent>(this.FilteredEvents.OrderBy(b => b.ActionDate));
                }
                else
                {
                    this.FilteredEvents.Clear();
                    foreach (DailyEvent e in this.allEvents)
                    {
                        if (e.AreaId == this.Area.AreaId)
                        {
                            foreach (OccupationalAreasOfEvent o in e.OccupationalAreasOfEvents)
                            {
                                if (this.OccupationalArea.OccupationalAreaId == o.OccupationalAreaId)
                                    this.FilteredEvents.Add(e);
                            }
                        }
                    }
                    this.FilteredEvents = new ObservableCollection<DailyEvent>(this.FilteredEvents.OrderBy(b => b.ActionDate));
                }
            }
        }

        public ICommand CleanFilterEventsCommand { protected set; get; }
        private void OnCleanFilter()
        {
            this.FilteredEvents = new ObservableCollection<DailyEvent>(this.allEvents);
            this.Area = new Area
            {
                AreaId = 0,
                AreaName = "בחירת איזור בארץ"
            };
            this.OccupationalArea = new OccupationalArea
            {
                OccupationalAreaId = 0,
                OccupationName = "בחירת סוג התנדבות"
            };
        }

        public ICommand RegisterToEventCommand { protected set; get; }

        public async void OnPress()
        {
            App app = (App)App.Current;
            Object o = app.CurrentUser;

            if (o == null)
            {
                await App.Current.MainPage.DisplayAlert("", "על מנת להירשם לאירוע התנדבות עליך להירשם או להתחבר לאפליקציה כמתנדב", "בסדר");
                return;
            }

            if (o is Volunteer)
            {
                VolunteersInEvent v = new VolunteersInEvent
                {
                    EventId = SelectedEvent.EventId,
                    Event = SelectedEvent,
                    VolunteerId = ((Volunteer)o).VolunteerId,
                    Volunteer = (Volunteer)o,
                    ActionDate = DateTime.Now,
                    RatingNum = 0,
                    WrittenRating = ""
                };

                VolunteeringAPIProxy proxy = VolunteeringAPIProxy.CreateProxy();
                VolunteersInEvent vol = await proxy.NewVolInEvent(v);

                if (vol == null)
                {
                    await App.Current.MainPage.DisplayAlert("שגיאה", "הרשמה לאירוע התנדבות נכשלה", "בסדר");
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("", "נרשמת בהצלחה לאירוע", "בסדר");
                }
            }
        }

    }
}
