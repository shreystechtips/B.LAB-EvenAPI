using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;
using System.IO;
using Plugin.Clipboard;
using Xamarin.Forms;
using System.Net;

namespace BuyingAssistant {

    //Searching for a loan, user enters the product they want and its costs
    public partial class UpdateBankInfoPage : ContentPage {

        public UpdateBankInfoPage(Boolean isNextVisible) {
            InitializeComponent();
            registerButton.IsVisible = isNextVisible;
            setFields();
        }

        void setFields(){
            PersonName.Text = Preferences.Get("PersonName", "");
            CardBenefits.SelectedIndex = Preferences.Get("CardBenefits", -1);
            CreditRange.SelectedIndex = Preferences.Get("CreditRange", -1);
            TypeOfAccount.SelectedIndex = Preferences.Get("TypeOfAccount", -1);
            PaymentRate.SelectedIndex = Preferences.Get("PaymentRate", -1);
            CurrentEmploymentStatus.SelectedIndex = Preferences.Get("CurrentEmploymentStatus", -1);
            FinanceOfResidence.SelectedIndex = Preferences.Get("FinanceOfResidence", -1);
            ResidenceType.SelectedIndex = Preferences.Get("ResidenceType", -1);
            ReasonForLoan.SelectedIndex = Preferences.Get("ReasonForLoan", -1);
            HighestEducationalDegree.SelectedIndex = Preferences.Get("HighestEducationalDegree", -1);
            TypeOfReturnOfferWanted.SelectedIndex = Preferences.Get("TypeOfReturnOfferWanted", -1);
        }

        public static String alert = "";

        void createAlert()
        {
            alert = "";
            if (PersonName.Text.Equals(""))
                alert += "\n- Person Name";
            if (CardBenefits.SelectedIndex == -1)
                alert += "\n- Card Benefits";
            if (CreditRange.SelectedIndex == -1)
                alert += "\n- Credit Range";
            if (TypeOfAccount.SelectedIndex == -1)
                alert += "\n- Type of Account";
            if (PaymentRate.SelectedIndex == -1)
                alert += "\n- Payment Rate";
            if (CurrentEmploymentStatus.SelectedIndex == -1)
                alert += "\n- Current Employment Status";
            if (FinanceOfResidence.SelectedIndex == -1)
                alert += "\n- Finance of Residence";
            if (ResidenceType.SelectedIndex == -1)
                alert += "\n- Residence Type";
            if (ReasonForLoan.SelectedIndex == -1)
                alert += "\n- Reason for loan";
            if (HighestEducationalDegree.SelectedIndex == -1)
                alert += "\n- Highest Education Degree";
            if (TypeOfReturnOfferWanted.SelectedIndex == -1)
                alert += "\n- Type of return offer wanted";
        }
        void Entry_TextChanged(object sender, Xamarin.Forms.TextChangedEventArgs e) {
            Preferences.Set("PersonName", e.NewTextValue);
        }

         void CardBenefits_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            Preferences.Set("CardBenefits", CardBenefits.SelectedIndex);
        }

         void CreditRange_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            Preferences.Set("CreditRange", CreditRange.SelectedIndex);
        }

         void TypeOfAccount_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            Preferences.Set("TypeOfAccount", TypeOfAccount.SelectedIndex);
        }

         void PaymentRate_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            Preferences.Set("PaymentRate", PaymentRate.SelectedIndex);
        }

         void CurrentEmploymentStatus_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            Preferences.Set("CurrentEmploymentStatus", CurrentEmploymentStatus.SelectedIndex);
        }

         void FinanceOfResidence_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            Preferences.Set("FinanceOfResidence", FinanceOfResidence.SelectedIndex);
        }

         void ResidenceType_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            Preferences.Set("ResidenceType", ResidenceType.SelectedIndex);
        }

         void ReasonForLoan_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            Preferences.Set("ReasonForLoan", ReasonForLoan.SelectedIndex);
        }

         void HighestEducationalDegree_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            Preferences.Set("HighestEducationalDegree", HighestEducationalDegree.SelectedIndex);
        }

         void TypeOfReturnOfferWanted_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            Preferences.Set("TypeOfReturnOfferWanted", TypeOfReturnOfferWanted.SelectedIndex);
        }

        async void Handle_Clicked(object sender, System.EventArgs e)
        {
            if (PersonName.Text.Equals("") ||
                CardBenefits.SelectedIndex == -1 ||
                CreditRange.SelectedIndex == -1 ||
                TypeOfAccount.SelectedIndex == -1 ||
                PaymentRate.SelectedIndex == -1 ||
                CurrentEmploymentStatus.SelectedIndex == -1 ||
                FinanceOfResidence.SelectedIndex == -1 ||
                ResidenceType.SelectedIndex == -1 ||
                ReasonForLoan.SelectedIndex == -1 ||
                HighestEducationalDegree.SelectedIndex == -1 ||
                TypeOfReturnOfferWanted.SelectedIndex == -1)
            {
                createAlert();
                await DisplayAlert("Please fill out the following:", alert, "OK");
            }
            else
            {
                await Navigation.PushAsync(new MainTabbedLayout());
            }

        }
    }
}