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

        public ObservableCollection<Association> AssociationsList { get; }
        #endregion

        public AppAdminViewModel()
        {
            AssociationsList = new ObservableCollection<Association>();
            this.isRefreshing = false;
            CreateAssociationsCollection();
        }

        #region Associasions
        async void CreateAssociationsCollection()
        {
            VolunteeringAPIProxy proxy = VolunteeringAPIProxy.CreateProxy();
            List<Association> theAssociations = await proxy.GetAssociations();
            foreach (Association a in theAssociations)
            {
                this.AssociationsList.Add(a);
            }
        }

        public ICommand SelctionChanged => new Command<Object>(OnSelectionChanged);
        public void OnSelectionChanged(Object obj)
        {
            if (obj is Association)
            {
                Association chosenAsso = (Association)obj;
                Page associationPage = new ShowAssociationPage();
                ShowAssociationViewModel assoContext = new ShowAssociationViewModel
                {
                    Email = chosenAsso.Email,
                    Username = chosenAsso.UserName,
                    InformationAbout = chosenAsso.InformationAbout,
                    PhoneNum = chosenAsso.PhoneNum
                };
                associationPage.BindingContext = assoContext;
                associationPage.Title = assoContext.Username;
                if (NavigateToPageEvent != null)
                    NavigateToPageEvent(associationPage);
            }
        }
        //Delete association
        public ICommand DeleteCommand => new Command<Association>(RemoveAsso);
        void RemoveAsso(Association a)
        {
            if (AssociationsList.Contains(a))
            {
                AssociationsList.Remove(a);
            }

        }
        public ICommand RefreshCommand => new Command(RefreshAssociations);

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

        public Action<Page> NavigateToPageEvent;
    }
}
