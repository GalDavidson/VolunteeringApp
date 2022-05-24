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
            AddItems();
            InitializeComponent();
        }

        private void AddItems()
        {
            ToolbarItem loginItem = new ToolbarItem
            {
                Text = "התחברות",
                Priority = 0,
                Order = ToolbarItemOrder.Secondary
            };
            loginItem.Clicked += ToolbarItem_Clicked_Login;

            this.ToolbarItems.Add(loginItem);
        }


        private void ToolbarItem_Clicked_Login(object sender, EventArgs e)
        {
            Page p = new NavigationPage(new Views.LoginPage());
            App.Current.MainPage = p;
        }
    }
}