using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using Plugin.Clipboard;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace BuyingAssistant {

    //Searching for a loan, user enters the product they want and its costs
    public partial class UpdateBankInfoPage : ContentPage {

        public UpdateBankInfoPage (Boolean isNextVisible) {
            InitializeComponent ();
            registerButton.IsVisible = isNextVisible;
            SetFields ();
        }

        void SetFields () {
            FirstName.Text = Preferences.Get ("FirstName", "");
            LastName.Text = Preferences.Get ("LastName", "");
            AnnualIncome.Text = Preferences.Get ("AnnualIncome", "");
            CardBenefits.SelectedIndex = Preferences.Get ("CardBenefits", -1);
            CreditRange.SelectedIndex = Preferences.Get ("CreditRange", -1);
            TypeOfAccount.SelectedIndex = Preferences.Get ("TypeOfAccount", -1);
            PaymentFrequency.SelectedIndex = Preferences.Get ("PaymentFrequency", -1);
            CurrentEmploymentStatus.SelectedIndex = Preferences.Get ("CurrentEmploymentStatus", -1);
            FinanceOfResidence.SelectedIndex = Preferences.Get ("FinanceOfResidence", -1);
            ResidenceType.SelectedIndex = Preferences.Get ("ResidenceType", -1);
            ReasonForLoan.SelectedIndex = Preferences.Get ("ReasonForLoan", -1);
            HighestEducationalDegree.SelectedIndex = Preferences.Get ("HighestEducationalDegree", -1);
            TypeOfReturnOfferWanted.SelectedIndex = Preferences.Get ("TypeOfReturnOfferWanted", -1);
        }

        public static String alert = "";

        void CreateAlert () {
            if (FirstName.Text.Equals (""))
                alert += "\n- First Name";
            if (LastName.Text.Equals (""))
                alert += "\n- Last Name";
            if (AnnualIncome.Text.Equals (""))
                alert += "\n- Annual Income";
            if (CardBenefits.SelectedIndex == -1)
                alert += "\n- Card Benefits";
            if (CreditRange.SelectedIndex == -1)
                alert += "\n- Credit Range";
            if (TypeOfAccount.SelectedIndex == -1)
                alert += "\n- Type of Account";
            if (PaymentFrequency.SelectedIndex == -1)
                alert += "\n- Payment Rate";
            if (CurrentEmploymentStatus.SelectedIndex == -1)
                alert += "\n- Current Employment Status";
            if (FinanceOfResidence.SelectedIndex == -1)
                alert += "\n- Finance of Residence";
            if (ResidenceType.SelectedIndex == -1)
                alert += "\n- Residence Type";
            if (ReasonForLoan.SelectedIndex == -1)
                alert += "\n- Reason for Loan";
            if (HighestEducationalDegree.SelectedIndex == -1)
                alert += "\n- Highest Education Degree";
            if (TypeOfReturnOfferWanted.SelectedIndex == -1)
                alert += "\n- Type of Return Offer Wanted";
        }

        void FirstName_TextChanged (object sender, Xamarin.Forms.TextChangedEventArgs e) {
            Preferences.Set ("FirstName", e.NewTextValue);
        }

        void LastName_TextChanged (object sender, Xamarin.Forms.TextChangedEventArgs e) {
            Preferences.Set ("LastName", e.NewTextValue);
        }

        void AnnualIncome_TextChanged (object sender, Xamarin.Forms.TextChangedEventArgs e) {
            Preferences.Set ("AnnualIncome", e.NewTextValue);
        }

        void CardBenefits_SelectedIndexChanged (object sender, System.EventArgs e) {
            Preferences.Set ("CardBenefits", CardBenefits.SelectedIndex);
        }

        void CreditRange_SelectedIndexChanged (object sender, System.EventArgs e) {
            Preferences.Set ("CreditRange", CreditRange.SelectedIndex);
        }

        void TypeOfAccount_SelectedIndexChanged (object sender, System.EventArgs e) {
            Preferences.Set ("TypeOfAccount", TypeOfAccount.SelectedIndex);
        }

        void PaymentFrequency_SelectedIndexChanged (object sender, System.EventArgs e) {
            Preferences.Set ("PaymentFrequency", PaymentFrequency.SelectedIndex);
        }

        void CurrentEmploymentStatus_SelectedIndexChanged (object sender, System.EventArgs e) {
            Preferences.Set ("CurrentEmploymentStatus", CurrentEmploymentStatus.SelectedIndex);
        }

        void FinanceOfResidence_SelectedIndexChanged (object sender, System.EventArgs e) {
            Preferences.Set ("FinanceOfResidence", FinanceOfResidence.SelectedIndex);
        }

        void ResidenceType_SelectedIndexChanged (object sender, System.EventArgs e) {
            Preferences.Set ("ResidenceType", ResidenceType.SelectedIndex);
        }

        void ReasonForLoan_SelectedIndexChanged (object sender, System.EventArgs e) {
            Preferences.Set ("ReasonForLoan", ReasonForLoan.SelectedIndex);
        }

        void HighestEducationalDegree_SelectedIndexChanged (object sender, System.EventArgs e) {
            Preferences.Set ("HighestEducationalDegree", HighestEducationalDegree.SelectedIndex);
        }

        void TypeOfReturnOfferWanted_SelectedIndexChanged (object sender, System.EventArgs e) {
            Preferences.Set ("TypeOfReturnOfferWanted", TypeOfReturnOfferWanted.SelectedIndex);
        }

        async void Handle_Clicked (object sender, System.EventArgs e) {
            if (FirstName.Text.Equals ("") ||
                LastName.Text.Equals ("") ||
                AnnualIncome.Text.Equals ("") ||
                CardBenefits.SelectedIndex == -1 ||
                CreditRange.SelectedIndex == -1 ||
                TypeOfAccount.SelectedIndex == -1 ||
                PaymentFrequency.SelectedIndex == -1 ||
                CurrentEmploymentStatus.SelectedIndex == -1 ||
                FinanceOfResidence.SelectedIndex == -1 ||
                ResidenceType.SelectedIndex == -1 ||
                ReasonForLoan.SelectedIndex == -1 ||
                HighestEducationalDegree.SelectedIndex == -1 ||
                TypeOfReturnOfferWanted.SelectedIndex == -1) {
                CreateAlert ();
                await DisplayAlert ("Please fill out the following:", alert, "OK");
            } else {
                await Navigation.PushAsync (new MainTabbedLayout ());
            }

        }
    }
}