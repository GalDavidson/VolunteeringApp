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

        private string gender;
        public string Gender
        {
            get { return gender; }
            set
            {
                gender = value;
                OnPropertyChanged("Gender");
            }
        }

        private List<Gender> genders;
        public List<Gender> Genders
        {
            get
            {
                if (((App)App.Current).LookupTables != null)
                    return ((App)App.Current).LookupTables.Genders;
                return new List<Gender>();
            }
        }

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
                    GenderID = (int)chosenVol.GenderId,
                };

                gender = genders[(int)chosenVol.GenderId].GenderType;

                volunteerPage.BindingContext = volContext;
                volunteerPage.Title = volContext.UserName;
                if (NavigateToPageEvent != null)
                    NavigateToPageEvent(volunteerPage);
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
