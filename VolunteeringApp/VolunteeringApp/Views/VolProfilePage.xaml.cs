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
    public partial class VolProfilePage : ContentPage
    {
        public VolProfilePage()
        {
            InitializeComponent();
        }

        public VolProfilePage(ShowVolunteerViewModel vm)
        {
            this.BindingContext = vm;
            InitializeComponent();
        }

        public async void NavigateToAsync(Page p)
        {
            await Navigation.PushAsync(p);
        }
    }
}