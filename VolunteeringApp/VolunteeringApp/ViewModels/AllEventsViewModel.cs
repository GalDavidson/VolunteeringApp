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

        #region סניפים
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

        //private string searchBranch;
        //public string SearchBranch
        //{
        //    get
        //    {
        //        return this.searchBranch;
        //    }
        //    set
        //    {
        //        //if (this.searchBranch != value)
        //        //{
        //        this.searchBranch = value;
        //        OnTypeChanged(value);
        //        OnPropertyChanged("SearchBranch");
        //        //}
        //    }
        //}

        private void InitEvents()
        {
            IsRefresh = true;
            App theApp = (App)App.Current;
            this.allEvents = theApp.LookupTables.Events;


            //Copy list to the filtered list
            this.filteredEvents = new ObservableCollection<DailyEvent>(this.allEvents.OrderBy(b => b.ActionDate));
            //SearchBranch = String.Empty;
            IsRefresh = false;
        }

        #endregion סניפים

        //#region Search Events
        //public void OnTypeChanged(string searching)
        //{
        //    //Filter the list of contacts based on the search term
        //    if (this.allEvents == null)
        //        return;
        //    if (String.IsNullOrWhiteSpace(searching) || String.IsNullOrEmpty(searching))
        //    {
        //        foreach (DailyEvent p in this.allEvents)
        //        {
        //            if (!this.FilteredEvents.Contains(p))
        //                this.FilteredEvents.Add(p);
        //        }
        //    }
        //    else
        //    {
        //        foreach (DailyEvent p in this.allEvents)
        //        {
        //            string branchesString = $"{b.BranchLocation}";

        //            if (!this.FilteredEvents.Contains(p) &&
        //                branchesString.Contains(searching))
        //                this.FilteredEvents.Add(p);
        //            else if (this.FilteredEvents.Contains(p) &&
        //                !branchesString.Contains(searching))
        //                this.FilteredEvents.Remove(p);
        //        }
        //    }

        //    this.FilteredEvents = new ObservableCollection<Event>(this.FilteredEvents.OrderBy(b => b.ActionDate));
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


        public AllEventsViewModel()
        {
            this.filteredEvents = new ObservableCollection<DailyEvent>();
            InitEvents();
        }



    }
}
