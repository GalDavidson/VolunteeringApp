using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using VolunteeringApp.ViewModels;
using VolunteeringApp.Models;

namespace VolunteeringApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewEventPage : ContentPage
    {
        public NewEventPage()
        {
            NewEventViewModel n = new NewEventViewModel();
            n.SetImageSourceEvent += OnSetImageSource;
            this.BindingContext = n;
            AddItems();

            InitializeComponent();
        }

        private void AddItems()
        {
            App app = (App)App.Current;
            Object o = app.CurrentUser;

            if (o is Association)
            {
                ToolbarItem allEventsItem = new ToolbarItem
                {
                    Text = "כל האירועים",
                    Priority = 0,
                    Order = ToolbarItemOrder.Secondary
                };
                allEventsItem.Clicked += ToolbarItem_Clicked_AllEvents;

                ToolbarItem assoEventsItem = new ToolbarItem
                {
                    Text = "האירועים שלי",
                    Priority = 0,
                    Order = ToolbarItemOrder.Secondary
                };
                assoEventsItem.Clicked += ToolbarItem_Clicked_AssoEvents;

                ToolbarItem profileItem = new ToolbarItem
                {
                    Text = "הפרטים שלי",
                    Priority = 0,
                    Order = ToolbarItemOrder.Secondary
                };
                profileItem.Clicked += ToolbarItem_Clicked_AssoProfile;

                ToolbarItem logoutItem = new ToolbarItem
                {
                    Text = "התנתקות",
                    Priority = 0,
                    Order = ToolbarItemOrder.Secondary
                };
                logoutItem.Clicked += ToolbarItem_Clicked_Logout;

                this.ToolbarItems.Add(allEventsItem);
                this.ToolbarItems.Add(assoEventsItem);
                this.ToolbarItems.Add(profileItem);
                this.ToolbarItems.Add(logoutItem);
            }
        }

        private void ToolbarItem_Clicked_AssoProfile(object sender, EventArgs e)
        {
            Page p = new NavigationPage(new Views.UpdateAssoPage());
            App.Current.MainPage = p;
        }

        private void ToolbarItem_Clicked_AllEvents(object sender, EventArgs e)
        {
            Page p = new NavigationPage(new Views.AllEventsPage());
            App.Current.MainPage = p;
        }

        private void ToolbarItem_Clicked_AssoEvents(object sender, EventArgs e)
        {
            Page p = new NavigationPage(new Views.AssoEventsNavigationPage());
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

        public void OnSetImageSource(ImageSource source)
        {
            theImage.Source = source;
        }
    }
}