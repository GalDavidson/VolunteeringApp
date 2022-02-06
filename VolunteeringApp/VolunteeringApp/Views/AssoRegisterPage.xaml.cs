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
    public partial class AssoRegisterPage : ContentPage
    {
        public AssoRegisterPage()
        {
            AssoRegisterViewModel vm = new AssoRegisterViewModel();
            vm.SetImageSourceEvent += OnSetImageSource;
            this.BindingContext = vm;

            InitializeComponent();
        }

        public void OnSetImageSource(ImageSource source)
        {
            theImage.Source = source;
        }
    }
}