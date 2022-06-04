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
            AddItems();
            InitializeComponent();
        }

        private void AddItems()
        {
            App theApp = (App)App.Current;
            Object o = theApp.CurrentUser;
            if (o is null)
            {
                ToolbarItem loginItem = new ToolbarItem
                {
                    Text = "התחברות",
                    Priority = 0,
                    Order = ToolbarItemOrder.Secondary
                };
                loginItem.Clicked += ToolbarItem_Clicked_Login;

                ToolbarItem registerItem = new ToolbarItem
                {
                    Text = "הרשמה",
                    Priority = 0,
                    Order = ToolbarItemOrder.Secondary
                };
                registerItem.Clicked += ToolbarItem_Clicked_Register;

                this.ToolbarItems.Add(loginItem);
                this.ToolbarItems.Add(registerItem);
            }

            if (o is Association)
            {
                ToolbarItem profileItem = new ToolbarItem
                {
                    Text = "הפרטים שלי",
                    Priority = 0,
                    Order = ToolbarItemOrder.Secondary
                };
                profileItem.Clicked += ToolbarItem_Clicked_AssoProfile;

                ToolbarItem assoEventsItem = new ToolbarItem
                {
                    Text = "האירועים שלי",
                    Priority = 0,
                    Order = ToolbarItemOrder.Secondary
                };
                assoEventsItem.Clicked += ToolbarItem_Clicked_AssoEvents;

                ToolbarItem newEventItem = new ToolbarItem
                {
                    Text = "פרסום אירוע חדש",
                    Priority = 0,
                    Order = ToolbarItemOrder.Secondary
                };
                newEventItem.Clicked += ToolbarItem_Clicked_NewEvent;

                ToolbarItem logoutItem = new ToolbarItem
                {
                    Text = "התנתקות",
                    Priority = 0,
                    Order = ToolbarItemOrder.Secondary
                };
                logoutItem.Clicked += ToolbarItem_Clicked_Logout;

                this.ToolbarItems.Add(profileItem);
                this.ToolbarItems.Add(assoEventsItem);
                this.ToolbarItems.Add(newEventItem);
                this.ToolbarItems.Add(logoutItem);
            }

            if (o is Volunteer)
            {
                ToolbarItem profileItem = new ToolbarItem
                {
                    Text = "הפרטים שלי",
                    Priority = 0,
                    Order = ToolbarItemOrder.Secondary
                };
                profileItem.Clicked += ToolbarItem_Clicked_VolProfile;

                ToolbarItem volEventsItem = new ToolbarItem
                {
                    Text = "האירועים שלי",
                    Priority = 0,
                    Order = ToolbarItemOrder.Secondary
                };
                volEventsItem.Clicked += ToolbarItem_Clicked_VolEvents;

                ToolbarItem logoutItem = new ToolbarItem
                {
                    Text = "התנתקות",
                    Priority = 0,
                    Order = ToolbarItemOrder.Secondary
                };
                logoutItem.Clicked += ToolbarItem_Clicked_Logout;

                this.ToolbarItems.Add(profileItem);
                this.ToolbarItems.Add(volEventsItem);
                this.ToolbarItems.Add(logoutItem);
            }

            if (o is AppAdmin)
            {
                ToolbarItem appAdminItem = new ToolbarItem
                {
                    Text = "דף מנהל.ת",
                    Priority = 0,
                    Order = ToolbarItemOrder.Secondary
                };
                appAdminItem.Clicked += ToolbarItem_Clicked_AdminPage;

                ToolbarItem logoutItem = new ToolbarItem
                {
                    Text = "התנתקות",
                    Priority = 0,
                    Order = ToolbarItemOrder.Secondary
                };
                logoutItem.Clicked += ToolbarItem_Clicked_Logout;

                this.ToolbarItems.Add(appAdminItem);
                this.ToolbarItems.Add(logoutItem);
            }
        }

        private void ToolbarItem_Clicked_Login(object sender, EventArgs e)
        {
            Page p = new NavigationPage(new Views.LoginPage());
            App.Current.MainPage = p;
        }

        private void ToolbarItem_Clicked_Register(object sender, EventArgs e)
        {
            Page p = new NavigationPage(new Views.RegisterNavigation());
            App.Current.MainPage = p;
        }

        private void ToolbarItem_Clicked_AssoProfile(object sender, EventArgs e)
        {
            Page p = new NavigationPage(new Views.UpdateAssoPage());
            App.Current.MainPage = p;
        }

        private void ToolbarItem_Clicked_VolProfile(object sender, EventArgs e)
        {
            Page p = new NavigationPage(new Views.UpdateVolPage());
            App.Current.MainPage = p;
        }

        private void ToolbarItem_Clicked_AssoEvents(object sender, EventArgs e)
        {
            Page p = new NavigationPage(new Views.AssoEventsNavigationPage());
            App.Current.MainPage = p;
        }

        private void ToolbarItem_Clicked_VolEvents(object sender, EventArgs e)
        {
            Page p = new NavigationPage(new Views.VolunteerEventsPage());
            App.Current.MainPage = p;
        }

        private void ToolbarItem_Clicked_NewEvent(object sender, EventArgs e)
        {
            Page p = new NavigationPage(new Views.NewEventPage());
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

        private void ToolbarItem_Clicked_AdminPage(object sender, EventArgs e)
        {
            Page p = new NavigationPage(new Views.AppAdminPage());
            App.Current.MainPage = p;
        }
    }
}