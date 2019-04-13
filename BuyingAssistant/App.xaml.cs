using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace BuyingAssistant
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
        }

        protected override void OnSleep()
        {
            //When the app is sleeping or the user isn't in the app and in the task manager (switching app mode)
            //then the screen becomes white

        }
    }
}