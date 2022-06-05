using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using VolunteeringApp.Models;
using System.ComponentModel;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using VolunteeringApp.Services;
using VolunteeringApp.Views;

namespace VolunteeringApp.ViewModels
{
    public class RateVolViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
