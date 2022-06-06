using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using VolunteeringApp.Models;
using Xamarin.Forms;

namespace VolunteeringApp.ViewModels
{
    public class ShowAssociationViewModel: INotifyPropertyChanged
    {
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        public string Email { get; set; }
        public string UserName { get; set; }
        public string InformationAbout { get; set; }
        public string PhoneNum { get; set; }
        public string ProfilePic { get; set; }
        public List<BranchesOfAssociation> BranchLst { get; set; }
        public List<OccupationalAreasOfAssociation> OccuAreasLst { get; set; }
    }
}
