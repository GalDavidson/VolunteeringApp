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
            AddItems();
            InitializeComponent();
        }

        private void AddItems()
        {
            App theApp = (App)App.Current;
            Object o = theApp.CurrentUser;
            if (o is Volunteer)
            {
                ToolbarItem allEventsItem = new ToolbarItem
                {
                    Text = "כל האירועים",
                    Priority = 0,
                    Order = ToolbarItemOrder.Secondary
                };
                allEventsItem.Clicked += ToolbarItem_Clicked_AllEvents;

                ToolbarItem volEventsItem = new ToolbarItem
                {
                    Text = "האירועים שלי",
                    Priority = 0,
                    Order = ToolbarItemOrder.Secondary
                };
                allEventsItem.Clicked += ToolbarItem_Clicked_volEvents;

                ToolbarItem volProfileItem = new ToolbarItem
                {
                    Text = "פרסום אירוע חדש",
                    Priority = 0,
                    Order = ToolbarItemOrder.Secondary
                };
                volProfileItem.Clicked += ToolbarItem_Clicked_VolProfile;

                ToolbarItem logoutItem = new ToolbarItem
                {
                    Text = "התנתקות",
                    Priority = 0,
                    Order = ToolbarItemOrder.Secondary
                };
                logoutItem.Clicked += ToolbarItem_Clicked_Logout;

                this.ToolbarItems.Add(allEventsItem);
                this.ToolbarItems.Add(volEventsItem);
                this.ToolbarItems.Add(volProfileItem);
                this.ToolbarItems.Add(logoutItem);
            }
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
