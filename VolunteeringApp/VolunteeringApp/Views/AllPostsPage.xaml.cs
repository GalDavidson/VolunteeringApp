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
    public partial class AllPostsPage : ContentPage
    {
        public AllPostsPage()
        {
            BindingContext = new AllPostsViewModel();
            InitializeComponent();
        }
    }
}