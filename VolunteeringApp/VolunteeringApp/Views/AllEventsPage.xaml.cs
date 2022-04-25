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
using VolunteeringApp.Views;

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
    }
}