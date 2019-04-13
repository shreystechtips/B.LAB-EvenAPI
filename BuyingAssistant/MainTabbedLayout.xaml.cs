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
            //Children.Add(new Rankings());
            //Children.Add(new PitScouting());
            Children.Add(new UpdateBankInfoPage());
            InitializeComponent();

        }
    }
}