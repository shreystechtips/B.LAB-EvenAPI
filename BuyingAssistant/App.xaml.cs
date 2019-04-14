using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace BuyingAssistant
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            //If the person's name is blank then prompt them for their bank info
            if (Preferences.Get("PersonName", "").Equals(""))
            {
                MainPage = new NavigationPage(new UpdateBankInfoPage(true));
            }
            else
            {
                MainPage = new NavigationPage(new MainTabbedLayout());
            }
        }

        protected override void OnSleep()
        {
            //When the app is sleeping or the user isn't in the app and in the task manager (switching app mode)
            //then the screen becomes white

        }
    }
}