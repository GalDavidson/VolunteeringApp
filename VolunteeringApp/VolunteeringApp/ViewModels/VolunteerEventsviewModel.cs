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
    class VolunteerEventsViewModel: INotifyPropertyChanged
    {
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region כל האירועים של משתמש
        public ObservableCollection<DailyEvent> eventsList;
        public ObservableCollection<DailyEvent> EventsList
        {
            get
            {
                return this.eventsList;
            }
            set
            {
                if (this.EventsList != value)
                {
                    this.EventsList = value;
                    OnPropertyChanged("EventsList");
                }
            }
        }
        private void CreateEventsCollection()
        {
            App theApp = (App)App.Current;
            this.EventsList = new ObservableCollection<DailyEvent>(theApp.LookupTables.Events);
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
        //public ICommand DeleteAssoCommand => new Command<Association>(RemoveAsso);
        //public async void RemoveAsso(Association a)
        //{
        //    bool result = await App.Current.MainPage.DisplayAlert("את.ה בטוח.ה?", "", "כן", "לא", FlowDirection.RightToLeft);
        //    if (result)
        //    {
        //        VolunteeringAPIProxy proxy = VolunteeringAPIProxy.CreateProxy();
        //        bool ok = await proxy.RemoveAsso(a);

        //        if (ok)
        //        {
        //            AssociationsList.Remove(a);
        //        }
        //        else
        //        {
        //            await App.Current.MainPage.DisplayAlert("שגיאה", "לא בוצע", "אישור");
        //        }
        //    }
        //    else
        //    {
        //        await App.Current.MainPage.DisplayAlert("שגיאה", "לא בוצע", "אישור");
        //    }
        //}
        #endregion

        public Action<Page> NavigateToPageEvent;


        public VolunteerEventsViewModel()
        {
            CreateEventsCollection();
        }
    }
}
