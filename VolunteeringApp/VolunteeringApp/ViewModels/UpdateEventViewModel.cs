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
    public class UpdateEventViewModel : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region שם האירוע
        private bool showEventNameError;

        public bool ShowEventNameError
        {
            get => showEventNameError;
            set
            {
                showEventNameError = value;
                OnPropertyChanged("ShowEventNameError");
            }
        }

        private string eventName;

        public string EventName
        {
            get => eventName;
            set
            {
                eventName = value;
                ValidateEventName();
                OnPropertyChanged("EventName");
            }
        }

        private string eventNameError;

        public string EventNameError
        {
            get => eventNameError;
            set
            {
                eventNameError = value;
                OnPropertyChanged("EventNameError");
            }
        }

        private void ValidateEventName()
        {
            this.ShowConditions = false;

            this.ShowEventNameError = string.IsNullOrEmpty(EventName);
            if (ShowEventNameError)
                EventNameError = ERROR_MESSAGES.REQUIRED_FIELD;
        }
        #endregion

        #region כתובת האירוע
        private bool showLocationError;

        public bool ShowLocationError
        {
            get => showLocationError;
            set
            {
                showLocationError = value;
                OnPropertyChanged("ShowLocationError");
            }
        }

        private string location;

        public string Location
        {
            get => location;
            set
            {
                location = value;
                ValidateLocation();
                OnPropertyChanged("Location");
            }
        }

        private string locationError;
        public string LocationError
        {
            get => locationError;
            set
            {
                locationError = value;
                OnPropertyChanged("LocationError");
            }
        }

        private void ValidateLocation()
        {
            this.ShowConditions = false;

            this.ShowLocationError = string.IsNullOrEmpty(Location);
            if (ShowLocationError)
                LocationError = ERROR_MESSAGES.REQUIRED_FIELD;
        }
        #endregion

        #region תאריך האירוע

        private DateTime entryDate = DateTime.Now;
        public DateTime EntryDate
        {
            get => this.entryDate;
            set
            {
                if (value != this.entryDate)
                {
                    this.entryDate = value;
                    ValidateDate();
                    OnPropertyChanged("EntryDate");
                }
            }
        }

        private bool showDateError;
        public bool ShowDateError
        {
            get => showDateError;
            set
            {
                showDateError = value;
                OnPropertyChanged("ShowDateError");
            }
        }

        private string dateError;
        public string DateError
        {
            get => dateError;
            set
            {
                dateError = value;
                OnPropertyChanged("DateError");
            }
        }

        private void ValidateDate()
        {
            //DateError = "";

            //int curStart = 0;
            //int curEnd = 0;

            //if (EntryStartTime.Hours == 0)
            //    curStart = 24;
            //if (EntryEndTime.Hours == 0)
            //    curEnd = 24;

            //if (DateTime.Now.Date == EntryDate.Date)
            //{
            //    if ((EntryEndTime-EntryStartTime).Minutes < 30)
            //    {
            //        ShowDateError = true;
            //        DateError += "לא ניתן לקיים אירוע שנמשך פחות מחצי שעה";
            //    }
            //}

            //TimeSpan t = DateTime.Now.TimeOfDay;
            //if ((EntryStartTime - t).Hours < 1)
            //{
            //    ShowDateError = true;
            //    DateError += "לא ניתן לפרסם אירוע שמתקיים בעוד פחות משעה";
            //}
        }
        #endregion

        #region שעת התחלה
        private TimeSpan entryStartTime = new TimeSpan(0, 0, 0, 0);
        public TimeSpan EntryStartTime
        {
            get => this.entryStartTime;
            set
            {
                if (value != this.entryStartTime)
                {
                    this.entryStartTime = value;
                    ValidateDate();
                    OnPropertyChanged("EntryStartTime");
                }
            }
        }
        #endregion

        #region שעת סיום
        private TimeSpan entryEndTime = new TimeSpan(0, 0, 0, 0);
        public TimeSpan EntryEndTime
        {
            get => this.entryEndTime;
            set
            {
                if (value != this.entryEndTime)
                {
                    this.entryEndTime = value;
                    ValidateDate();
                    OnPropertyChanged("EntryEndTime");
                }
            }
        }
        #endregion

        #region תיאור האירוע
        private bool showCaptionError;

        public bool ShowCaptionError
        {
            get => showCaptionError;
            set
            {
                showCaptionError = value;
                OnPropertyChanged("ShowCaptionError");
            }
        }

        private string caption;

        public string Caption
        {
            get => caption;
            set
            {
                caption = value;
                ValidateCaption();
                OnPropertyChanged("Caption");
            }
        }

        private string captionError;

        public string CaptionError
        {
            get => captionError;
            set
            {
                captionError = value;
                OnPropertyChanged("CaptionError");
            }
        }

        private void ValidateCaption()
        {
            this.ShowConditions = false;

            this.ShowCaptionError = string.IsNullOrEmpty(Caption);
            if (ShowCaptionError)
                CaptionError = ERROR_MESSAGES.REQUIRED_FIELD;
        }
        #endregion

        #region אירוע קיים

        private DailyEvent existEvent;

        public DailyEvent ExistEvent
        {
            get => existEvent;
            set
            {
                if (this.existEvent != value)
                {
                    existEvent = value;
                    OnPropertyChanged("ExistEvent");
                }
            }
        }
        #endregion

        #region תחומי עיסוק

        private List<OccupationalArea> allOccupationalAreas;
        private ObservableCollection<OccupationalArea> filteredOccuAreas;
        public ObservableCollection<OccupationalArea> FilteredOccuAreas
        {
            get
            {
                return this.filteredOccuAreas;
            }
            set
            {
                if (this.filteredOccuAreas != value)
                {

                    this.filteredOccuAreas = value;
                    OnPropertyChanged("FilteredOccuAreas");
                }
            }
        }

        private string searchTerm;
        public string SearchTerm
        {
            get
            {
                return this.searchTerm;
            }
            set
            {
                //if (this.searchTerm != value)
                //{
                this.searchTerm = value;
                OnTextChanged(value);
                OnPropertyChanged("SearchTerm");
                //}
            }
        }

        private void InitOccuAreas()
        {
            IsRefreshing = true;
            App theApp = (App)App.Current;
            this.allOccupationalAreas = theApp.LookupTables.OccupationalAreas;


            //Copy list to the filtered list
            this.FilteredOccuAreas = new ObservableCollection<OccupationalArea>(this.allOccupationalAreas.OrderBy(a => a.OccupationName));
            SearchTerm = String.Empty;
            IsRefreshing = false;
            //Select the relevant Occup Area for the specific user
            
            foreach (OccupationalAreasOfEvent oae in ExistEvent.OccupationalAreasOfEvents)
            {
                OccupationalArea area = this.FilteredOccuAreas.Where(occ => occ.OccupationalAreaId == oae.OccupationalAreaId).FirstOrDefault();
                if (area != null)
                    SelectedOccuAreas.Add(area);
            }
        }

        private string newOccuArea;
        public string NewOccuArea
        {
            get => newOccuArea;
            set
            {
                newOccuArea = value;
                OnPropertyChanged("NewOccuArea");
            }
        }
        #endregion תחומי עיסוק

        #region אזור בארץ
        public ObservableCollection<Area> Areas { get; }

        public void CreateAreaCollection()
        {
            if (((App)App.Current).LookupTables != null)
            {
                List<Area> areasList = ((App)App.Current).LookupTables.Areas;
                foreach (Area a in areasList)
                {
                    this.Areas.Add(a);
                }
            }
        }

        private Area area;
        public Area Area
        {
            get { return area; }
            set
            {
                area = value;
                OnPropertyChanged("Area");
            }
        }

        private int areaIndex;
        public int AreaIndex
        {
            get { return areaIndex; }
            set
            {
                areaIndex = value;
                OnPropertyChanged("AreaIndex");
            }
        }

        #endregion

        #region Search
        public void OnTextChanged(string search)
        {
            //Filter the list of contacts based on the search term
            if (this.allOccupationalAreas == null)
                return;
            if (String.IsNullOrWhiteSpace(search) || String.IsNullOrEmpty(search))
            {
                foreach (OccupationalArea a in this.allOccupationalAreas)
                {
                    if (!this.FilteredOccuAreas.Contains(a))
                        this.FilteredOccuAreas.Add(a);
                }
            }
            else
            {
                foreach (OccupationalArea a in this.allOccupationalAreas)
                {
                    string occuAreaString = $"{a.OccupationName}";

                    if (!this.FilteredOccuAreas.Contains(a) &&
                        occuAreaString.Contains(search))
                        this.FilteredOccuAreas.Add(a);
                    else if (this.FilteredOccuAreas.Contains(a) &&
                        !occuAreaString.Contains(search))
                        this.FilteredOccuAreas.Remove(a);
                }
            }

            this.FilteredOccuAreas = new ObservableCollection<OccupationalArea>(this.FilteredOccuAreas.OrderBy(a => a.OccupationName));
        }
        #endregion

        #region AreaSelection
        ObservableCollection<Object> selectedOccuAreas;
        public ObservableCollection<Object> SelectedOccuAreas
        {
            get
            {
                return selectedOccuAreas;
            }
            set
            {
                if (selectedOccuAreas != value)
                {
                    selectedOccuAreas = value;
                    OnPropertyChanged("SelectedOccuAreas");
                }
            }
        }

        private string occupationalAreas;

        public string OccupationalAreas
        {
            get => occupationalAreas;
            set
            {
                occupationalAreas = value;
                OnPropertyChanged("OccupationalAreas");
            }
        }



        public ICommand UpdateOccuArea => new Command<Object>(OnPressedOccuArea);
        public async void OnPressedOccuArea(object occuAreasList)
        {

            OccupationalAreas = string.Empty;
            
            if (selectedOccuAreas.Count == 0) { OccupationalAreas = "לא נבחרו תחומי עיסוק"; }
        }
        #endregion

        #region Add New Area
        public ICommand AddOccuArea => new Command(OnAddOccuArea);
        public async void OnAddOccuArea()
        {

            if (string.IsNullOrEmpty(NewOccuArea))
            {
                await App.Current.MainPage.DisplayAlert("שגיאה", "!לא ניתן להוסיף ערך זה", "בסדר");
                return;
            }

            OccupationalArea NewOccuAr = new OccupationalArea
            {
                OccupationName = NewOccuArea
            };

            bool IsExist = false;
            if (allOccupationalAreas.Contains(NewOccuAr)) { IsExist = true; }

            if (!IsExist)
            {
                VolunteeringAPIProxy proxy = VolunteeringAPIProxy.CreateProxy();
                bool ok = await proxy.AddOccupationalArea(NewOccuAr);

                if (ok)
                {
                    allOccupationalAreas.Add(NewOccuAr);
                    await App.Current.MainPage.DisplayAlert("", "בסדר", "!הוספת תחום עיסוק בהצלחה");
                }
                else if (!ok)
                {
                    await App.Current.MainPage.DisplayAlert("בסדר", "הוספת תחום עיסוק נכשלה", "שגיאה");
                }
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("בסדר", "תחום עיסוק זה כבר קיים במערכת", "שגיאה");
            }

        }
        #endregion

        #region Refresh
        private bool isRefreshing;
        public bool IsRefreshing
        {
            get => isRefreshing;
            set
            {
                if (this.isRefreshing != value)
                {
                    this.isRefreshing = value;
                    OnPropertyChanged(nameof(IsRefreshing));
                }
            }
        }
        public ICommand RefreshCommand => new Command(OnRefresh);
        public void OnRefresh()
        {
            InitOccuAreas();
        }
        #endregion

        #region הצגת בעיות
        private bool showConditions;

        public bool ShowConditions
        {
            get => showConditions;
            set
            {
                showConditions = value;
                OnPropertyChanged("ShowConditions");
            }
        }
        #endregion

        #region הבעיה הבאה
        private string nextError;

        public string NextError
        {
            get => nextError;
            set
            {
                nextError = value;
                OnPropertyChanged("NextError");
            }
        }

        private bool showNextError;

        public bool ShowNextError
        {
            get => showNextError;
            set
            {
                showNextError = value;
                OnPropertyChanged("ShowNextError");
            }
        }
        #endregion הבעיה הבאה

        #region serverStatus
        private string serverStatus;
        public string ServerStatus
        {
            get { return serverStatus; }
            set
            {
                serverStatus = value;
                OnPropertyChanged("ServerStatus");
            }
        }
        #endregion serverStatus

        public UpdateEventViewModel(DailyEvent e)
        {
            App app = (App)App.Current;
            Association currentUser = (Association)app.CurrentUser;
            VolunteeringAPIProxy proxy = VolunteeringAPIProxy.CreateProxy();

            this.ExistEvent = e;
            this.EventName = e.EventName;
            this.Location = e.EventLocation;
            this.EntryDate = (DateTime)e.StartTime;

            DateTime start = (DateTime)e.StartTime;
            DateTime end = (DateTime)e.EndTime;

            this.EntryStartTime = new TimeSpan(start.Hour, start.Minute, start.Second);
            this.EntryEndTime = new TimeSpan(end.Hour, end.Minute, end.Second);
            this.Caption = e.Caption;
            this.AreaIndex = (int)(e.AreaId - 1);

            this.ShowEventNameError = false;
            this.ShowLocationError = false;
            this.ShowDateError = false;
            this.ShowCaptionError = false;

            this.SelectedOccuAreas = new ObservableCollection<Object>();
            this.FilteredOccuAreas = new ObservableCollection<OccupationalArea>();

            InitOccuAreas();

            Areas = new ObservableCollection<Area>();
            CreateAreaCollection();

            this.SubmitCommand = new Command(OnSubmit);
            //this.DeleteCommand = new Command(OnDelete);
        }

        public ICommand SubmitCommand { protected set; get; }
       
        private bool ValidateForm()
        {
            ValidateEventName();
            ValidateLocation();
            ValidateDate();
            ValidateCaption();

            //check if any validation failed
            if (ShowCaptionError || ShowEventNameError || ShowLocationError || ShowDateError)
                return false;
            return true;
        }

        public async void OnSubmit()
        {

            if (ValidateForm())
            {
                ServerStatus = "מתחבר לשרת...";
                await App.Current.MainPage.Navigation.PushModalAsync(new Views.ServerStatusPage(this));

                DateTime start = new DateTime(EntryDate.Year, EntryDate.Month, EntryDate.Day, EntryStartTime.Hours, EntryStartTime.Minutes, EntryStartTime.Seconds);
                DateTime end = new DateTime(EntryDate.Year, EntryDate.Month, EntryDate.Day, EntryEndTime.Hours, EntryEndTime.Minutes, EntryEndTime.Seconds);

                DailyEvent ev = new DailyEvent
                {
                    EventId = this.ExistEvent.EventId,
                    EventName = this.EventName,
                    EventLocation = this.Location,
                    Caption = this.Caption,
                    ActionDate = this.ExistEvent.ActionDate,
                    AssociationId = this.ExistEvent.AssociationId,
                    StartTime = start,
                    EndTime = end,
                    AreaId = this.Area.AreaId
                };

                foreach (OccupationalArea o in selectedOccuAreas)
                {
                    OccupationalAreasOfEvent oc = new OccupationalAreasOfEvent
                    {
                        OccupationalAreaId = o.OccupationalAreaId,
                        EventId = ev.EventId
                    };
                    ev.OccupationalAreasOfEvents.Add(oc);
                }

                ev.Area = new Area
                {
                    AreaId = this.Area.AreaId,
                    AreaName = this.Area.AreaName
                };

                VolunteeringAPIProxy proxy = VolunteeringAPIProxy.CreateProxy();
                bool dailyEvent = await proxy.UpdateEvent(ev);

                if (!dailyEvent)
                {
                    await App.Current.MainPage.DisplayAlert("שגיאה", "עדכון פרטים נכשל, בדוק את הפרטים המוקלדים", "בסדר");
                }
                else
                {
                    Page page = new NavigationPage(new Views.AllEventsPage());
                    App.Current.MainPage = page;

                }
            }
            else
            {
                ShowNextError = true;
                NextError = "אירעה שגיאה! לא ניתן להמשיך בהרשמה";
            }
        }
    }
}
