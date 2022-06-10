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
    public class AppAdminViewModel: INotifyPropertyChanged
    {
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region IsRefreshing
        private bool isRefreshing;
        public bool IsRefreshing
        {
            get
            {
                return this.isRefreshing;
            }
            set
            {
                if (this.isRefreshing != value)
                {
                    this.isRefreshing = value;
                    OnPropertyChanged(nameof(IsRefreshing));
                }
            }
        }
        #endregion

        #region TodayVolunteers
        private int todayVolList;
        public int TodayVolList
        {
            get
            {
                return this.todayVolList;
            }
            set
            {
                if (this.todayVolList != value)
                {
                    this.todayVolList = value;
                    OnPropertyChanged(nameof(TodayVolList));
                }
            }
        }

        private async void CreateTodayVolCollection()
        {
            VolunteeringAPIProxy proxy = VolunteeringAPIProxy.CreateProxy();

            List<Volunteer> volList = await proxy.GetVolunteers();
            foreach (Volunteer v in volList)
            {
                if (((DateTime)v.ActionDate).Day == DateTime.Now.Day)
                    this.TodayVolList++;
            }
        }
        #endregion

        #region MonthVolunteers
        private int monthVolList;
        public int MonthVolList
        {
            get
            {
                return this.monthVolList;
            }
            set
            {
                if (this.monthVolList != value)
                {
                    this.monthVolList = value;
                    OnPropertyChanged(nameof(MonthVolList));
                }
            }
        }

        private async void CreateMonthVolCollection()
        {
            VolunteeringAPIProxy proxy = VolunteeringAPIProxy.CreateProxy();

            List<Volunteer> volList = await proxy.GetVolunteers();
            foreach (Volunteer v in volList)
            {
                if (((DateTime)v.ActionDate).Month == DateTime.Now.Month)
                    this.MonthVolList++;
            }
        }
        #endregion

        #region TodayEvents
        private int todayEventsList;
        public int TodayEventsList
        {
            get
            {
                return this.todayEventsList;
            }
            set
            {
                if (this.todayEventsList != value)
                {
                    this.todayEventsList = value;
                    OnPropertyChanged(nameof(TodayEventsList));
                }
            }
        }

        private async void CreateTodayEventsCollection()
        {
            VolunteeringAPIProxy proxy = VolunteeringAPIProxy.CreateProxy();

            List<DailyEvent> eventsList = await proxy.GetEvents();
            foreach (DailyEvent de in eventsList)
            {
                if (((DateTime)de.ActionDate).Day == DateTime.Now.Day)
                    this.TodayEventsList++;
            }
        }
        #endregion

        #region MonthEvents
        private int monthEventsList;
        public int MonthEventsList
        {
            get
            {
                return this.monthEventsList;
            }
            set
            {
                if (this.monthEventsList != value)
                {
                    this.monthEventsList = value;
                    OnPropertyChanged(nameof(MonthEventsList));
                }
            }
        }

        private async void CreateMonthEventsCollection()
        {
            VolunteeringAPIProxy proxy = VolunteeringAPIProxy.CreateProxy();

            List<DailyEvent> eventsList = await proxy.GetEvents();
            foreach (DailyEvent de in eventsList)
            {
                if (((DateTime)de.ActionDate).Month == DateTime.Now.Month)
                    this.MonthEventsList++;
            }
        }
        #endregion

        #region ThisMonthVolunteersToEvents
        private int volsInEvents;
        public int VolsInEvents
        {
            get
            {
                return this.volsInEvents;
            }
            set
            {
                if (this.volsInEvents != value)
                {
                    this.volsInEvents = value;
                    OnPropertyChanged(nameof(VolsInEvents));
                }
            }
        }

        private async void CreateVolsInEventsCollection()
        {
            VolunteeringAPIProxy proxy = VolunteeringAPIProxy.CreateProxy();

            List<VolunteersInEvent> volList = await proxy.GetVolunteersInEvents();
            foreach (VolunteersInEvent de in volList)
            {
                if (((DateTime)de.ActionDate).Month == DateTime.Now.Month)
                    this.VolsInEvents++;
            }
        }
        #endregion

        public AppAdminViewModel()
        {
            AssociationsList = new ObservableCollection<Association>();
            VolunteersList = new ObservableCollection<Volunteer>();
            this.isRefreshing = false;
            CreateAssociationsCollection();
            CreateVolunteersCollection();
            CreateTodayVolCollection();
            CreateMonthVolCollection();
            CreateTodayEventsCollection();
            CreateMonthEventsCollection();
            CreateVolsInEventsCollection();
        }

        #region Associasions

        public ObservableCollection<Association> AssociationsList { get; }
        private async void CreateAssociationsCollection()
        {
            VolunteeringAPIProxy proxy = VolunteeringAPIProxy.CreateProxy();

            List<Association> assoList = await proxy.GetAssociations();
            foreach (Association a in assoList)
            {
                this.AssociationsList.Add(a);
            }
        }


        private Association selectedAsso;
        public Association SelectedAsso
        {
            get
            {
                return this.selectedAsso;
            }
            set
            {

                if (value != null && value != this.selectedAsso)
                {
                    this.selectedAsso = value;
                    OnSelectionAssociationChanged(value);
                }
                if (value == null)
                {
                    this.selectedAsso = null;
                    OnPropertyChanged("SelectedAsso");

                }

            }
        }

        public ICommand SelectionAssociationChanged => new Command<Object>(OnSelectionAssociationChanged);
        public void OnSelectionAssociationChanged(Object obj)
        {
            if (obj is Association)
            {
                Association chosenAsso = (Association)obj;
                Page associationPage = new ShowAssociationPage();

                AssoProfileViewModel assoContext = new AssoProfileViewModel
                {
                    Email = chosenAsso.Email,
                    UserName = chosenAsso.UserName,
                    InformationAbout = chosenAsso.InformationAbout,
                    PhoneNum = chosenAsso.PhoneNum,
                    BranchLst = chosenAsso.BranchesOfAssociations.ToList(),
                    OccuAreasLst = chosenAsso.OccupationalAreasOfAssociations.ToList(),
                    ProfilePic = chosenAsso.ProfilePic
                };
                associationPage.BindingContext = assoContext;

                if (NavigateToPageEvent != null)
                    NavigateToPageEvent(associationPage);

                //SelectedAsso = null;
            }
        }
        //Delete association
        public ICommand DeleteAssoCommand => new Command<Association>(RemoveAsso);
        public async void RemoveAsso(Association a)
        {
            bool result = await App.Current.MainPage.DisplayAlert("את.ה בטוח.ה?", "", "כן", "לא", FlowDirection.RightToLeft);
            if (result)
            {
                VolunteeringAPIProxy proxy = VolunteeringAPIProxy.CreateProxy();
                bool ok = await proxy.RemoveAsso(a);

                if (ok)
                {
                    AssociationsList.Remove(a);
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("שגיאה", "לא בוצע", "אישור");
                }
            }
        }

        #endregion

        #region Volunteers
        public ObservableCollection<Volunteer> VolunteersList { get; }
        private async void CreateVolunteersCollection()
        {
            VolunteeringAPIProxy proxy = VolunteeringAPIProxy.CreateProxy();

            List<Volunteer> volList = await proxy.GetVolunteers();
            foreach (Volunteer v in volList)
            {
                this.VolunteersList.Add(v);
            }
        }

        public ICommand SelectionVolunteerChanged => new Command<Object>(OnSelectionVolunteerChanged);
        public void OnSelectionVolunteerChanged(Object obj)
        {
            if (obj is Volunteer)
            {
                Volunteer vol = (Volunteer)obj;
                Page volunteerPage = new ShowVolunteerPage();

                int age = 0;
                age = DateTime.Now.Subtract(vol.BirthDate).Days;
                age = age / 365;

                List<VolunteersInEvent> lst = vol.VolunteersInEvents;

                string icon = "";
                string rank = "";

                App theApp = (App)App.Current;
                List<Rank> ranks = theApp.LookupTables.Ranks;

                if (lst.Count < 11)
                {
                    rank = ranks[0].RankName;
                    icon = "footSteps.png";
                }
                if (lst.Count > 10 && lst.Count < 16)
                {
                    rank = ranks[1].RankName;
                    icon = "coolHand.png";
                }
                if (lst.Count > 15 && lst.Count < 21) 
                {
                    rank = ranks[2].RankName;
                    icon = "clap.png";
                }
                if (lst.Count > 20 && lst.Count < 26)
                {
                    rank = ranks[3].RankName;
                    icon = "fire.png";
                }
                if (lst.Count > 25)
                {
                    rank = ranks[4].RankName;
                    icon = "party.png";
                }

                VolProfileViewModel volContext = new VolProfileViewModel()
                {
                    FName = vol.FName,
                    LName = vol.LName,
                    UserName = vol.UserName,
                    Age = age,
                    RatingNum = (int)vol.AvgRating,
                    TotalEvents = lst.Count,
                    ProfilePic = vol.ProfilePic,
                    Rank = rank,
                    RankPic = icon
                };
                Page volPage = new VolProfilePage(volContext);

                if (NavigateToPageEvent != null)
                    NavigateToPageEvent(volPage);
            }
        }
        //Delete volunteer
        public ICommand DeleteVolCommand => new Command<Volunteer>(RemoveVol);

        public async void RemoveVol(Volunteer v)
        {
            bool result = await App.Current.MainPage.DisplayAlert("את.ה בטוח.ה?", "", "כן", "לא", FlowDirection.RightToLeft);
            if (result)
            {
                VolunteeringAPIProxy proxy = VolunteeringAPIProxy.CreateProxy();
                bool ok = await proxy.RemoveVol(v);

                if (ok)
                {
                    VolunteersList.Remove(v);
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
    }
}
