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
            this.allEvents = theApp.LookupTables.Events;


            //Copy list to the filtered list
            this.filteredEvents = new ObservableCollection<DailyEvent>(this.allEvents.OrderBy(b => b.ActionDate));
            IsRefresh = false;
        }
        #endregion

        //#region Search Events
        //public Command FilterCommand => new Command(OnTypeChanged);
        //public void OnTypeChanged()
        //{
        //    //Filter the list of contacts based on the search term
        //    if (this.allEvents == null)
        //    {

        //        return;
        //    }

        //    if (this.Area != null)
        //    {
        //        foreach (DailyEvent e in this.allEvents)
        //        {
        //            this.filteredEvents.Clear();
        //            if (e.AreaId == this.Area.AreaId)
        //                this.FilteredEvents.Add(e);
        //        }
        //    }

        //    this.filteredEvents = new ObservableCollection<DailyEvent>(this.FilteredEvents.OrderBy(b => b.ActionDate));
        //}
        //#endregion

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
        //List<Branch> selectedBranches;
        //public List<Branch> SelectedBranches
        //{
        //    get
        //    {
        //        return selectedBranches;
        //    }
        //    set
        //    {
        //        if (selectedBranches != value)
        //        {
        //            selectedBranches = value;
        //        }
        //    }
        //}

        //private string branches;

        //public string Branches
        //{
        //    get => branches;
        //    set
        //    {
        //        branches = value;
        //        OnPropertyChanged("branches");
        //    }
        //}


        //public ICommand UpdateBranches => new Command(OnPressedBranches);
        //public async void OnPressedBranches(object branchesList)
        //{
        //    SelectedBranches.Clear();
        //    Branches = string.Empty;
        //    if (branchesList is IList<object>)
        //    {
        //        List<object> list = ((IList<object>)branchesList).ToList();
        //        foreach (object b in list)
        //        {
        //            SelectedBranches.Add((Branch)b);
        //        }
        //        if (selectedBranches.Count == 0) { Branches = "לא נבחרו סניפים"; }
        //    }
        //}
        #endregion

        #region רשימת איזורים

        public List<Area> Areas { get; }

        private void CreateAreasCollection()
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
            get => area;
            set
            {
                if (this.area != value)
                    area = value;
                OnPropertyChanged("Area");
            }
        }
        #endregion

        public AllEventsViewModel()
        {
            this.filteredEvents = new ObservableCollection<DailyEvent>();
            InitEvents();
            //CreateAreasCollection();
            this.RegisterToEventCommand = new Command(OnPress);
        }

        public ICommand RegisterToEventCommand { protected set; get; }

        public async void OnPress()
        {
            App app = (App)App.Current;
            Object o = app.CurrentUser;

            if (o == null)
                await App.Current.MainPage.DisplayAlert("", "על מנת להירשם לאירוע התנדבות עליך להירשם/להתחבר לאפליקציה", "בסדר");

            if (o is Volunteer)
            {
                VolunteersInEvent v = new VolunteersInEvent
                {
                    EventId = 
                }
            }
        }

    }
}
