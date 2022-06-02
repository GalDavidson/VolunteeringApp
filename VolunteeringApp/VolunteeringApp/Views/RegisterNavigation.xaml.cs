using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using VolunteeringApp.ViewModels;

namespace VolunteeringApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterNavigation : TabbedPage
    {
        public RegisterNavigation()
        {
            this.BindingContext = new LoginViewModel();
            InitializeComponent();
        }

        private void ToolbarItem_Clicked_AllEvents(object sender, EventArgs e)
        {
            Page p = new NavigationPage(new Views.AllEventsPage());
            App.Current.MainPage = p;
        }

        private void ToolbarItem_Clicked_Login(object sender, EventArgs e)
        {
            Page p = new NavigationPage(new Views.LoginPage());
            App.Current.MainPage = p;
        }
    }
}