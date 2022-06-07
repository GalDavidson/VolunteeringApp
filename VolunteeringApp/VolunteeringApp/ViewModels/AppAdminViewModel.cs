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

        private void CreateTodayVolCollection()
        {
            if (((App)App.Current).LookupTables != null)
            {
                List<Volunteer> volunteersList = ((App)App.Current).LookupTables.Volunteers;
                foreach (Volunteer v in volunteersList)
                {
                    if (((DateTime)v.ActionDate).Day == DateTime.Now.Day)
                        TodayVolList++;
                }
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

        private void CreateMonthVolCollection()
        {
            if (((App)App.Current).LookupTables != null)
            {
                List<Volunteer> volunteersList = ((App)App.Current).LookupTables.Volunteers;
                foreach (Volunteer v in volunteersList)
                {
                    if (((DateTime)v.ActionDate).Month == DateTime.Now.Month)
                        MonthVolList++;
                }
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

        private void CreateTodayEventsCollection()
        {
            if (((App)App.Current).LookupTables != null)
            {
                List<DailyEvent> eventsList = ((App)App.Current).LookupTables.Events;
                foreach (DailyEvent e in eventsList)
                {
                    if (((DateTime)e.StartTime).Day == DateTime.Now.Day)
                        TodayEventsList++;
                }
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

        private void CreateMonthEventsCollection()
        {
            if (((App)App.Current).LookupTables != null)
            {
                List<DailyEvent> eventsList = ((App)App.Current).LookupTables.Events;
                foreach (DailyEvent e in eventsList)
                {
                    if (((DateTime)e.StartTime).Month == DateTime.Now.Month)
                        MonthEventsList++;
                }
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

        private void CreateVolsInEventsCollection()
        {
            if (((App)App.Current).LookupTables != null)
            {
                List<VolunteersInEvent> volsList = ((App)App.Current).LookupTables.VolsInEvents;
                foreach (VolunteersInEvent v in volsList)
                {
                    if (((DateTime)v.ActionDate).Month == DateTime.Now.Month)
                        VolsInEvents++;
                }
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

                ShowAssociationViewModel assoContext = new ShowAssociationViewModel
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
            else
            {
                await App.Current.MainPage.DisplayAlert("שגיאה", "לא בוצע", "אישור");
            }
        }

        #endregion

        #region Volunteers

        //private string gender;
        //public string Gender
        //{
        //    get { return gender; }
        //    set
        //    {
        //        gender = value;
        //        OnPropertyChanged("Gender");
        //    }
        //}

        //private List<Gender> genders;
        //public List<Gender> Genders
        //{
        //    get
        //    {
        //        if (((App)App.Current).LookupTables != null)
        //            return ((App)App.Current).LookupTables.Genders;
        //        return new List<Gender>();
        //    }
        //}

        public ObservableCollection<Volunteer> VolunteersList { get; }
        private void CreateVolunteersCollection()
        {
            if (((App)App.Current).LookupTables != null)
            {
                List<Volunteer> volsList = ((App)App.Current).LookupTables.Volunteers;
                foreach (Volunteer v in volsList)
                {
                    this.VolunteersList.Add(v);
                }
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
                int ratingSum = 0;
                foreach(VolunteersInEvent v in lst)
                {
                    ratingSum += v.RatingNum;
                }

                ShowVolunteerViewModel volContext = new ShowVolunteerViewModel()
                {
                    FName = vol.FName,
                    LName = vol.LName,
                    UserName = vol.UserName,
                    Age = age,
                    RatingNum = ratingSum / lst.Count,
                    TotalEvents = lst.Count,
                    ProfilePic = vol.ProfilePic,
                    Gender = vol.Gender
                };
                Page volPage = new VolProfilePage(volContext);

                if (NavigateToPageEvent != null)
                    NavigateToPageEvent(volPage);

                //Gender = genders[(int)chosenVol.GenderId].GenderType;
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
