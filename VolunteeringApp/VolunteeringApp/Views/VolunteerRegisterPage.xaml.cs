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
    public partial class VolunteerRegisterPage : ContentPage
    {
        public VolunteerRegisterPage()
        {
            VolRegisterViewModel vm = new VolRegisterViewModel();
            vm.SetImageSourceEvent += OnSetImageSource;
            this.BindingContext = vm;

            InitializeComponent();
        }

        public void OnSetImageSource(ImageSource source)
        {
            image.Source = source;
        }
    }
}