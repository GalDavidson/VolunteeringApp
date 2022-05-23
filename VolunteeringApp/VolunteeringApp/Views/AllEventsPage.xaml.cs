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

            

            ToolbarItem item = new ToolbarItem();
            item.Text = "contact us";
            item.Priority = 3;
            item.Order = ToolbarItemOrder.Secondary;
            item.Clicked += ContactUsCLicked;
            this.ToolbarItems.Add(item);
        }

        private void ContactUsCLicked(object sender, EventArgs e)
        {

        }

        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            Page p = new NavigationPage(new Views.LoginPage());
            App.Current.MainPage = p;
        }

        private void ToolbarItem_Clicked_1(object sender, EventArgs e)
        {
            Page p = new NavigationPage(new Views.RegisterNavigation());
            App.Current.MainPage = p;
        }
    }
}