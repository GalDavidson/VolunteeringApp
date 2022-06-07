﻿using System;
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
    public class ShowPastEventViewModel: INotifyPropertyChanged
    {
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        public string EventName { get; set; }
        public string EventLocation { get; set; }
        public DateTime EventDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public string Caption { get; set; }

        private ObservableCollection<VolunteersInEvent> volunteersList;
        public ObservableCollection<VolunteersInEvent> VolunteersList
        { 
            get => volunteersList;
            set
            {
                if (volunteersList != value)
                {
                    volunteersList = value;
                    OnPropertyChanged("VolunteersList");
                }
            }
        }

        public ICommand RateVolunteerCommand => new Command<VolunteersInEvent>(RateVol);
        public async void RateVol(VolunteersInEvent vol)
        {
            string result = await App.Current.MainPage.DisplayPromptAsync("", "מילוי חוות דעת כתובה על המתנדב", "שלח", "לא תודה");
            if (result != null)
            {
                vol.WrittenRating = result;
            }
            else
                return;
            
            VolunteeringAPIProxy proxy = VolunteeringAPIProxy.CreateProxy();
            bool success = await proxy.UpdateRate(vol);

            if (success)
                await App.Current.MainPage.DisplayAlert("", "עודכן בהצלחה", "בסדר");
            else
                await App.Current.MainPage.DisplayAlert("שגיאה", "לא עודכן", "בסדר");
        }

        public ICommand MoveToVolPageCommand => new Command<VolunteersInEvent>(VolPageCommand);

        public void VolPageCommand(VolunteersInEvent v)
        {
            Volunteer vol = v.Volunteer;

            int age = 0;
            age = DateTime.Now.Subtract(vol.BirthDate).Days;
            age = age / 365;

            List<VolunteersInEvent> lst = vol.VolunteersInEvents;

            ShowVolunteerViewModel volContext = new ShowVolunteerViewModel()
            {
                FName = vol.FName,
                LName = vol.LName,
                UserName = vol.UserName,
                Age = age,
                RatingNum = v.RatingNum,
                TotalEvents = lst.Count,
                ProfilePic = vol.ProfilePic
            };
            Page volPage = new VolProfilePage(volContext);

            if (NavigateToPageEvent != null)
                NavigateToPageEvent(volPage);
        }

        public Action<Page> NavigateToPageEvent;

        #region rating
        private int ratingValue;
        public int RatingValue
        {
            get
            {
                return this.ratingValue;
            }
            set
            {
                this.ratingValue = value;
                OnPropertyChanged("RatingValue");
            }
        }

        private List<int> ratingValueLst;
        public List<int> RatingValueLst
        {
            get
            {
                return this.ratingValueLst;
            }
            set
            {
                this.ratingValueLst = value;
                OnPropertyChanged("RatingValueLst");
            }
        }

        #endregion
    }
}
