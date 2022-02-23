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
    public class AppAdminViewModel
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

        public AppAdminViewModel()
        {
            AssociationsList = new ObservableCollection<Association>();
            VolunteersList = new ObservableCollection<Volunteer>();
            this.isRefreshing = false;
            CreateAssociationsCollection();
            CreatevolunteersCollection();
        }

        #region Associasions
        public ObservableCollection<Association> AssociationsList { get; }
        async void CreateAssociationsCollection()
        {
            VolunteeringAPIProxy proxy = VolunteeringAPIProxy.CreateProxy();
            List<Association> theAssociations = await proxy.GetAssociations();
            foreach (Association a in theAssociations)
            {
                this.AssociationsList.Add(a);
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
                    PhoneNum = chosenAsso.PhoneNum
                };
                associationPage.BindingContext = assoContext;
                associationPage.Title = assoContext.UserName;
                if (NavigateToPageEvent != null)
                    NavigateToPageEvent(associationPage);
            }
        }
        //Delete association
        public ICommand DeleteAssoCommand => new Command<Association>(RemoveAsso);
        void RemoveAsso(Association a)
        {
            if (AssociationsList.Contains(a))
            {
                AssociationsList.Remove(a);
            }

        }
        public ICommand RefreshAssoCommand => new Command(RefreshAssociations);

        async void RefreshAssociations()
        {
            VolunteeringAPIProxy proxy = VolunteeringAPIProxy.CreateProxy();
            List<Association> theAssociations = await proxy.GetAssociations();
            this.AssociationsList.Clear();
            foreach (Association a in theAssociations)
            {
                AssociationsList.Add(a);
            }
            this.IsRefreshing = false;
        }
        #endregion

        #region Volunteers
        public ObservableCollection<Volunteer> VolunteersList { get; }
        async void CreatevolunteersCollection()
        {
            VolunteeringAPIProxy proxy = VolunteeringAPIProxy.CreateProxy();
            List<Volunteer> theVolunteers = await proxy.GetVolunteers();
            foreach (Volunteer v in theVolunteers)
            {
                this.VolunteersList.Add(v);
            }
        }

        public ICommand SelectionVolunteerChanged => new Command<Object>(OnSelectionVolunteerChanged);
        public void OnSelectionVolunteerChanged(Object obj)
        {
            if (obj is Volunteer)
            {
                Volunteer chosenVol = (Volunteer)obj;
                Page volunteerPage = new ShowVolunteerPage();
                ShowVolunteerViewModel volContext = new ShowVolunteerViewModel
                {
                    LName = chosenVol.LName,
                    FName = chosenVol.FName,
                    Email = chosenVol.Email,
                    UserName = chosenVol.UserName,
                    BirthDate = chosenVol.BirthDate,
                    GenderID = (int)chosenVol.GenderId
                };
                volunteerPage.BindingContext = volContext;
                volunteerPage.Title = volContext.UserName;
                if (NavigateToPageEvent != null)
                    NavigateToPageEvent(volunteerPage);
            }
        }
        //Delete volunteer
        public ICommand DeleteVolCommand => new Command<Volunteer>(RemoveVol);
        void RemoveVol(Volunteer v)
        {
            if (VolunteersList.Contains(v))
            {
                VolunteersList.Remove(v);
            }

        }
        public ICommand RefreshVolCommand => new Command(RefreshVolunteers);

        async void RefreshVolunteers()
        {
            VolunteeringAPIProxy proxy = VolunteeringAPIProxy.CreateProxy();
            List<Volunteer> theVolunteers = await proxy.GetVolunteers();
            this.VolunteersList.Clear();
            foreach (Volunteer v in theVolunteers)
            {
                VolunteersList.Add(v);
            }
            this.IsRefreshing = false;
        }
        #endregion

        public Action<Page> NavigateToPageEvent;
    }
}
