﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using VolunteeringApp.ViewModels;
using VolunteeringApp.Models;

namespace VolunteeringApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AppAdminPage : ContentPage
    {
        public AppAdminPage()
        {
            AppAdminViewModel context = new AppAdminViewModel();
            //Register to the event so the view model will be able to navigate to the monkeypage
            context.NavigateToPageEvent += NavigateToAsync;
            this.BindingContext = context;
            AddItems();

            InitializeComponent();
        }
        private void AddItems()
        {
            App theApp = (App)App.Current;
            Object o = theApp.CurrentUser;
            if (o is AppAdmin)
            {
                ToolbarItem allEventsItem = new ToolbarItem
                {
                    Text = "כל האירועים",
                    Priority = 0,
                    Order = ToolbarItemOrder.Secondary
                };
                allEventsItem.Clicked += ToolbarItem_Clicked_AllEvents;

                ToolbarItem logoutItem = new ToolbarItem
                {
                    Text = "התנתקות",
                    Priority = 0,
                    Order = ToolbarItemOrder.Secondary
                };
                logoutItem.Clicked += ToolbarItem_Clicked_Logout;

                this.ToolbarItems.Add(allEventsItem);
                this.ToolbarItems.Add(logoutItem);
            }
        }

        private void ToolbarItem_Clicked_AllEvents(object sender, EventArgs e)
        {
            Page p = new NavigationPage(new Views.AllEventsPage());
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

        public async void NavigateToAsync(Page p)
        {
            await Navigation.PushAsync(p);

        }
    }
}