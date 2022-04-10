using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VolunteeringApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Menu : ContentPage
    {
        public Menu()
        {
            InitializeComponent();

            ToolbarItem item = new ToolbarItem();
            item.Text = "Contact Us!";
            item.Priority = 5;
            item.Order = ToolbarItemOrder.Secondary;
            
        }

        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            Page p = new NavigationPage(new Views.HomePage());
            App.Current.MainPage = p;
        }
    }
}