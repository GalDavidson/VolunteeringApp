using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VolunteeringApp.ViewModels;
using VolunteeringApp.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VolunteeringApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AssoPastEventsPage : ContentPage
    {
        public AssoPastEventsPage()
        {
            AssoEventsViewModel context = new AssoEventsViewModel();
            //Register to the event so the view model will be able to navigate to the monkeypage
            context.NavigateToPageEvent += NavigateToAsync;
            this.BindingContext = context;

            InitializeComponent();
        }

        public async void NavigateToAsync(Page p)
        {
            await Navigation.PushAsync(p);
        }

        //protected override void OnDisappearing()
        //{
        //    AssoEventsViewModel context = (AssoEventsViewModel)this.BindingContext;
        //    context.SelectedDailyEvent = null;
            
        //    base.OnDisappearing();
        //}
        
    }
}