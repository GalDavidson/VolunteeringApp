using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using VolunteeringApp.ViewModels;
using VolunteeringApp.Models;

using System.ComponentModel;
using VolunteeringApp.Services;
using VolunteeringApp.Views;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using Xamarin.Essentials;

namespace VolunteeringApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AllEventsPage: ContentPage
    {
        public AllEventsPage()
        {
            AllEventsViewModel a = new AllEventsViewModel();
            BindingContext = a;
            InitializeComponent();
           
        }

        private void AreaPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        //private void AreaPicker_SelectedIndexChanged(object sender, EventArgs e)
        //{

        //    List<DailyEvent> lst = new List<DailyEvent>();
        //    foreach (DailyEvent de in (List<DailyEvent>)AreaPicker.ItemsSource)
        //    {
        //        lst.Add(de);
        //    }

        //    if (lst == null)
        //    {
        //        Console.WriteLine("לא קיימים אירועים קרובים");
        //        return;
        //    }

        //    this.AreaPicker.ItemsSource = null;
        //    foreach (DailyEvent de in lst)
        //    {
        //        if (de.AreaId == ((Area)AreaPicker.SelectedItem).AreaId)
        //            this.AreaPicker.ItemsSource.Add(e);
        //    }

        //    //this.filteredEvents = new ObservableCollection<DailyEvent>(this.FilteredEvents.OrderBy(b => b.ActionDate));
        //}
    }
}