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
            string result = await App.Current.MainPage.DisplayPromptAsync("מילוי חוות דעת כתובה על המתנדב ושליחת הדירוג", "(ניתן להשאיר את חוות הדעת ריקה)", "שלח", "לא תודה");
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
