using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VolunteeringApp.Models;
using VolunteeringApp.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VolunteeringApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AssoEventsNavigationPage : TabbedPage
    {
        public AssoEventsNavigationPage(/*DailyEvent dailyEvent*/)
        {
            //if (dailyEvent != null)

            InitializeComponent();
        }

        private void ToolbarItem_Clicked_AllEvents(object sender, EventArgs e)
        {
            Page p = new NavigationPage(new Views.AllEventsPage());
            App.Current.MainPage = p;
        }

        private void ToolbarItem_Clicked_AssoProfile(object sender, EventArgs e)
        {
            Page p = new NavigationPage(new Views.UpdateAssoPage());
            App.Current.MainPage = p;
        }

        private void ToolbarItem_Clicked_NewEvent(object sender, EventArgs e)
        {
            Page p = new NavigationPage(new Views.NewEventPage());
            App.Current.MainPage = p;
        }

        private async void ToolbarItem_Clicked_Logout(object sender, EventArgs e)
        {
            bool result = await App.Current.MainPage.DisplayAlert("להתנתק?", "", "כן", "לא", FlowDirection.RightToLeft);
            if (result)
            {
                VolunteeringAPIProxy proxy = VolunteeringAPIProxy.CreateProxy();
                bool success = await proxy.Logout();

                if (success)
                {
                    App theApp = (App)App.Current;
                    theApp.CurrentUser = null;
                    await App.Current.MainPage.DisplayAlert("התנתקת בהצלחה", "", "", "בסדר", FlowDirection.RightToLeft);
                    Page p = new NavigationPage(new Views.AllEventsPage());
                    App.Current.MainPage = p;
                }
                else
                    await App.Current.MainPage.DisplayAlert("ההתנתקות נכשלה", "", "", "בסדר", FlowDirection.RightToLeft);

            }
        }
    }
}