using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VolunteeringApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VolunteeringApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ShowPastEventPage : ContentPage
    {
        public ShowPastEventPage()
        {
            InitializeComponent();
        }
        public ShowPastEventPage(ShowPastEventViewModel vm)
        {
            this.BindingContext = vm;
            InitializeComponent();
        }

        private void rating_ItemTapped(object sender, EventArgs e)
        {

        }
    }
}