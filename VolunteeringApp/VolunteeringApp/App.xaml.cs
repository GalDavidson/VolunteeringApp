using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using VolunteeringApp.Models;
using VolunteeringApp.Services;
using System.Threading.Tasks;
using VolunteeringApp.Views;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;

namespace VolunteeringApp
{
    public partial class App : Application
    {
        public Object CurrentUser { get; set; }

        public Lookups LookupTables { get; set; }

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

            ViewModels.ServerStatusViewModel vm = new ViewModels.ServerStatusViewModel();
            vm.ServerStatus = "טוען נתונים...";
            MainPage = new Views.ServerStatusPage(vm);

        }

        protected async override void OnStart()
        {
            VolunteeringAPIProxy proxy = VolunteeringAPIProxy.CreateProxy();
            this.LookupTables = await proxy.GetLookupsAsync();
            if (LookupTables != null)
            {
                MainPage = new NavigationPage(new LoginPage());
            }
            else
            {
                ViewModels.ServerStatusViewModel vm = new ViewModels.ServerStatusViewModel();
                vm.ServerStatus = "אירעה שגיאה בהתחברות לשרת";
                MainPage = new Views.ServerStatusPage(vm);
            }
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
