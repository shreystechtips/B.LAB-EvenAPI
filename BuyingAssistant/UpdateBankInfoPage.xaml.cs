using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

namespace BuyingAssistant {

    using System.IO;
    using System.Net;
    using System.Text;
    using System;
    using Plugin.Clipboard;
    using Xamarin.Forms;

    //Searching for a loan, user enters the product they want and its costs
    public partial class UpdateBankInfoPage : ContentPage {

        public UpdateBankInfoPage () {
            InitializeComponent ();
            init ();
        }

        async private void init () {

            Device.BeginInvokeOnMainThread (async () => {
                await DisplayAlert ("Alert", "Bad API Call", "Ok");
            });
        }

        //this method is called when the button with the text "click here" is clicked (i called the method this, you can rename this and the clicked parameter in the .xaml file)
        void Handle_Clicked (object sender, System.EventArgs e) {

            Device.BeginInvokeOnMainThread (async () => {
                await DisplayAlert ("Alert", "Hi", "Ok");
            });
        }

        private void Entry_TextChanged (object sender, TextChangedEventArgs e) {

            Preferences.Set ("PersonName", e.NewTextValue);
        }

        private void CardBenefits_SelectedIndexChanged (System.Object sender, System.EventArgs e) {

        }

        private void _SelectedIndexChanged (System.Object sender, System.EventArgs e) {

        }
    }
}