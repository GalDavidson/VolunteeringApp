using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using VolunteeringApp.Models;

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
        public DateTime BirthDate { get; set; }

        private string age;
        public string Age
        {
            get => age;
            set
            {
                if (age != value)
                age = value;
                OnPropertyChanged("Age");
            }
        }
    }
}
