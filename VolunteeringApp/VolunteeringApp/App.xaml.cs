using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VolunteeringApp
{
    public partial class App : Application
    {
        public Object CurrentUser { get; set; }

        public static bool IsDevEnv
        {
            get 
            { 
                return true;
            }
        }

        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
