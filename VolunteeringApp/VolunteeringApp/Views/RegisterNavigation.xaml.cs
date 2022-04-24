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

            ToolbarItem item = new ToolbarItem();
            item.Text = "contact us";
            item.Priority = 3;
            item.Order = ToolbarItemOrder.Secondary;
            item.Clicked += ContactUsCLicked;
        }

        private void ContactUsCLicked(object sender, EventArgs e)
        {

        }

        private void MenuItem1_Clicked(object sender, EventArgs e)
        {
            Page p = new NavigationPage(new Views.HomePage());
            App.Current.MainPage = p;
        }

        private void MenuItem2_Clicked(object sender, EventArgs e)
        {
            Page p = new NavigationPage(new Views.LoginPage());
            App.Current.MainPage = p;
        }
    }
}