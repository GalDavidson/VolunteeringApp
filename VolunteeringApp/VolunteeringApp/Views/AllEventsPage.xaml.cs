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

            AddItems();
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
                profileItem.Clicked += ToolbarItem_Clicked_Profile;

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
                //logoutItem.Clicked += ToolbarItem_Clicked_Register;

                this.ToolbarItems.Add(profileItem);
                this.ToolbarItems.Add(assoEventsItem);
                this.ToolbarItems.Add(newEventItem);
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

        private void ToolbarItem_Clicked_Profile(object sender, EventArgs e)
        {
            Page p = new NavigationPage(new Views.UpdateAssoPage());
            App.Current.MainPage = p;
        }

        private void ToolbarItem_Clicked_AssoEvents(object sender, EventArgs e)
        {
            Page p = new NavigationPage(new Views.AssoUpComingEventsPage());
            App.Current.MainPage = p;
        }

        private void ToolbarItem_Clicked_NewEvent(object sender, EventArgs e)
        {
            Page p = new NavigationPage(new Views.NewEventPage());
            App.Current.MainPage = p;
        }

    }
}