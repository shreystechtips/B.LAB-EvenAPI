using System;
using System.Collections;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using BuyingAssistant;
namespace BuyingAssistant
{
    public partial class MainTabbedLayout : Xamarin.Forms.TabbedPage
    {
        public MainTabbedLayout()
        {
            Children.Add(new MainPage());
            Children.Add(new UpdateBankInfoPage(false));
            Children.Add(new AboutPage());
            NavigationPage.SetHasNavigationBar(this,false);
            InitializeComponent();
        }
    }
}