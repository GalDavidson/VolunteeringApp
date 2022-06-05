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
    public partial class ShowEventPage : ContentPage
    {
        public ShowEventPage()
        {
            InitializeComponent();
        }
        public ShowEventPage(ShowEventViewModel vm)
        {
            ShowEventViewModel context = vm;
            context.NavigateToPageEvent += NavigateToAsync;
            this.BindingContext = vm;
            InitializeComponent();
        }

        public async void NavigateToAsync(Page p)
        {
            await Navigation.PushAsync(p);

        }

    }
}