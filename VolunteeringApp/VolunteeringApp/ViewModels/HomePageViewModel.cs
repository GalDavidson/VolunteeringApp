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
    class HomePageViewModel: INotifyPropertyChanged
    {
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion


        private TimeSpan entryStartTime = DateTime.Now.TimeOfDay;
        public TimeSpan EntryStartTime
        {
            get => this.entryStartTime;
            set
            {
                if (value != this.entryStartTime)
                {
                    this.entryStartTime = value;
                    OnPropertyChanged("EntryStartTime");
                }
            }
        }

        private TimeSpan entry;
        public TimeSpan Entry
        {
            get => this.entry;
            set
            {
                if (value != this.entry)
                {
                    this.entry= value;
                    OnPropertyChanged("Entry");
                }
            }
        }

        private string po;
        public string Po
        {
            get => this.po;
            set
            {
                if (value != this.po)
                {
                    this.po = value;
                    OnPropertyChanged("Po");
                }
            }
        }


        private void ValidateDate()
        {
            //if (DateTime.Now.Date == EntryDate.Date)
            //{
            double t = (Entry - EntryStartTime).Hours;
            if (t < 1 )
            {
                Po = "success";
            }
            //}
        }

        public HomePageViewModel()
        {
            ValidateDate();
        }
    }
}
