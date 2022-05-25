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
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            this.BindingContext = new LoginViewModel();

            InitializeComponent();
        }

        private void ToolbarItem_Clicked_Register(object sender, EventArgs e)
        {
            Page p = new NavigationPage(new Views.RegisterNavigation());
            App.Current.MainPage = p;
        }
    }
}