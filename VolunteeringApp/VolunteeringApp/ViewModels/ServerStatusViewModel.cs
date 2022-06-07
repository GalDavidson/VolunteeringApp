using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using VolunteeringApp.Services;
using System.Text.Json;
using VolunteeringApp.Models;
using VolunteeringApp.Views;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms.Xaml;


namespace VolunteeringApp.ViewModels
{
    class ServerStatusViewModel: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private string serverStatus;
        public string ServerStatus
        {
            get { return this.serverStatus; }

            set
            {
                if (this.serverStatus != value)
                {
                    this.serverStatus = value;
                    OnPropertyChanged(nameof(ServerStatus));
                }
            }
        }
    }
}
