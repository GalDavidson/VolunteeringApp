using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VolunteeringApp.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using VolunteeringApp.ViewModels;

namespace VolunteeringApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UpdateVolPage : ContentPage
    {
        public UpdateVolPage()
        {
            this.BindingContext = new UpdateVolViewModel();
            InitializeComponent();
        }

        private void ToolbarItem_Clicked_AllEvents(object sender, EventArgs e)
        {
            Page p = new NavigationPage(new Views.AllEventsPage());
            App.Current.MainPage = p;
        }

        private void ToolbarItem_Clicked_volEvents(object sender, EventArgs e)
        {
            Page p = new NavigationPage(new Views.VolunteerEventsPage());
            App.Current.MainPage = p;
        }

        private void ToolbarItem_Clicked_VolProfile(object sender, EventArgs e)
        {
            Page p = new NavigationPage(new Views.UpdateVolPage());
            App.Current.MainPage = p;
        }

        private async void ToolbarItem_Clicked_Logout(object sender, EventArgs e)
        {
            bool result = await App.Current.MainPage.DisplayAlert("להתנתק?", "", "כן", "לא", FlowDirection.RightToLeft);
            if (result)
            {
                App theApp = (App)App.Current;
                theApp.CurrentUser = null;
                await App.Current.MainPage.DisplayAlert("התנתקת בהצלחה", "", "", "בסדר", FlowDirection.RightToLeft);
                Page p = new NavigationPage(new Views.AllEventsPage());
                App.Current.MainPage = p;
            }
        }
    }
}
