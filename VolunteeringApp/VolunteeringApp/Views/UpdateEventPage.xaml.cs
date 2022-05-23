using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VolunteeringApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using VolunteeringApp.Models;
namespace VolunteeringApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UpdateEventPage : ContentPage
    {
        public UpdateEventPage(DailyEvent dailyEvent)
        {
            this.BindingContext = new UpdateEventViewModel(dailyEvent);
            InitializeComponent();
        }
    }
}