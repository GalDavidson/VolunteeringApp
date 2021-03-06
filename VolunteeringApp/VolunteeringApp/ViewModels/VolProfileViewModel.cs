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
    public class VolProfileViewModel: INotifyPropertyChanged
    {
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        public const int first = 10;
        public const int sec = 15;
        public const int third = 20;
        public const int fourth = 25;
        public const int fifth = 10;

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
        public string RankPic { get; set; }
        public string Rank { get; set; }
        
    }
}
