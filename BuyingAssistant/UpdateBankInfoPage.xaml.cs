using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

namespace BuyingAssistant {

    using System;
    using System.IO;
    using Plugin.Clipboard;
    using Xamarin.Forms;
    using System.Text;
    using System.Net;

    //Searching for a loan, user enters the product they want and its costs
    public partial class UpdateBankInfoPage : ContentPage {

        public UpdateBankInfoPage() {
            InitializeComponent();
            init();
        }

        async private void init() {

        }

        /*         
                //this method is called when the button with the text "click here" is clicked (i called the method this, you can rename this and the clicked parameter in the .xaml file)
                void Handle_Clicked(object sender, System.EventArgs e) {

                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        await DisplayAlert("Alert", "Hi", "Ok");
                    });
                }
        */

        private void Entry_TextChanged(object sender, TextChangedEventArgs e) {
            Preferences.Set("PersonName", e.NewTextValue);
        }

        private void CardBenefits_SelectedIndexChanged(System.Object sender, System.EventArgs e) {
            Preferences.Set("CardBenefits", CardBenefits.SelectedIndex);
        }

        private void CreditRange_SelectedIndexChanged(System.Object sender, System.EventArgs e) {
            Preferences.Set("CreditRange", CreditRange.SelectedIndex);
        }

        private void TypeOfAccount_SelectedIndexChanged(System.Object sender, System.EventArgs e) {
            Preferences.Set("TypeOfAccount", TypeOfAccount.SelectedIndex);
        }

        private void PaymentRate_SelectedIndexChanged(System.Object sender, System.EventArgs e) {
            Preferences.Set("PaymentRate", PaymentRate.SelectedIndex);
        }

        private void CurrentEmploymentStatus_SelectedIndexChanged(System.Object sender, System.EventArgs e) {
            Preferences.Set("CurrentEmpoymentStatus", CurrentEmploymentStatus.SelectedIndex);
        }

        private void FinanceOfResidence_SelectedIndexChanged(System.Object sender, System.EventArgs e) {
            Preferences.Set("FinanceOfResidence", FinanceOfResidence.SelectedIndex);
        }

        private void ResidenceType_SelectedIndexChanged(System.Object sender, System.EventArgs e) {
            Preferences.Set("ResidenceType", ResidenceType.SelectedIndex);
        }

        private void ReasonForLoan_SelectedIndexChanged(System.Object sender, System.EventArgs e) {
            Preferences.Set("ReasonForLoan", ReasonForLoan.SelectedIndex);
        }

        private void HighestEducationalDegree_SelectedIndexChanged(System.Object sender, System.EventArgs e) {
            Preferences.Set("HighestEducationalDegree", HighestEducationalDegree.SelectedIndex);
        }

        private void TypeOfReturnOfferWanted_SelectedIndexChanged(object sender, System.EventArgs e) {
            Preferences.Set("TypeOfReturnOfferWanted", TypeOfReturnOfferWanted.SelectedIndex);
        }
    }
}