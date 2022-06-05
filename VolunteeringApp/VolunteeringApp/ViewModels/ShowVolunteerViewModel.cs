using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using VolunteeringApp.Models;
using VolunteeringApp.Services;
using VolunteeringApp.Views;

namespace VolunteeringApp.ViewModels
{
    public class ShowVolunteerViewModel: INotifyPropertyChanged
    {
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        public string FName { get; set; }
        public string LName { get; set; }
        public string UserName { get; set; }

        private int age;
        public int Age
        {
            get => age;
            set
            {
                if (age != value)
                {
                    age = value;
                    OnPropertyChanged("Age");
                }
            }
        }

        public string ProfilePic { get; set; }
        public int RatingNum { get; set; }
        public int TotalEvents { get; set; }
        public string Rank { get; set; }

        public async void ShowProfilePage()
        {
            App app = (App)App.Current;

            if (app.CurrentUser == null)
            {
                bool result = await App.Current.MainPage.DisplayAlert("שגיאה", " יש להתחבר למערכת...", "אישור", "ביטול", FlowDirection.RightToLeft);
                if (result)
                    await app.MainPage.Navigation.PushModalAsync(new RateVolPage());
                else
                {
                    await App.Current.MainPage.DisplayAlert("יש", " יש להתחבר למערכת...", "אישור", "כל הכבוד !!", FlowDirection.RightToLeft);

                }
            }

            //else
            //{
            //    Nickname = app.CurrentUser.Nickname;
            //    ImgUrl = app.CurrentUser.ImageUrl;
            //}
        }
    }
}
