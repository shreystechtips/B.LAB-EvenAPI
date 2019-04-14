using System;
using System.IO;
using Plugin.Clipboard;
using Xamarin.Forms;
using System.Text;
using System.Net;
using Xamarin.Essentials;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Reflection;


namespace BuyingAssistant {
    //Searching for a loan, user enters the product they want and its costs
    public partial class MainPage : ContentPage {
        public MainPage() {
            InitializeComponent();
            populateList();
        }

        public class BetterSctruct {
            public String Key { get; set; }
            public String Value { get; set; }
        }
        protected override void OnAppearing() {
            populateList();
        }
        String savingsOfferUri;

        List<BetterSctruct> list;
        JArray item;

        public void populateList() {
            JArray items = JArray.Parse(Preferences.Get("savedItems", "[]"));
            List<BetterSctruct> dict = new List<BetterSctruct>();
            foreach (JObject s in items) {
                dict.Add(new BetterSctruct { Key = (String)s["cost"], Value = (String)s["itemName"] });
            }
            savedList.ItemsSource = dict;
            list = dict;
            item = items;
        }
        public void searchOffer(object sender, System.EventArgs e) {
            UpdateBankInfoPage s = new UpdateBankInfoPage(false);
            s.CreateAlert();
            if (!String.IsNullOrWhiteSpace(UpdateBankInfoPage.alert)) {
                DisplayAlert("Error, Missing:", UpdateBankInfoPage.alert, "OK");
            } else {
                init();
            }
        }

        async private void init() {
            Preferences.Set("textVal", searchBar.Text);
            var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://api.evenfinancial.com/leads/rateTables");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Headers["Authorization"] = "Bearer e7675dd3-ff3b-434b-95aa-70251cc3784b_88140dd4-f13e-4ce3-8322-6eaf2ee9a2d2";
            httpWebRequest.Method = "POST";

            //reads file
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "BuyingAssistant.hardCode.json";
            string result;
            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream)) {
                result = reader.ReadToEnd();
            }


            JObject full = JObject.Parse(result);
            JObject personal = (JObject)full["personalInformation"];
            personal["firstName"] = Preferences.Get("FirstName", "");
            personal["lastName"] = Preferences.Get("LastName", "");
            //personal["dateOfBirth"] = GetRandomValues()[0];
            //personal["primaryPhone"] = GetRandomValues()[1];
            //personal["ssn"] = GetRandomValues()[3];

            JObject loanInf = (JObject)full["loanInformation"];
            loanInf["loanAmount"] = Convert.ToInt32(searchBar.Text);
            Preferences.Get("AnnualIncome", "");
            JObject creditInf = (JObject)full["creditInformation"];
            creditInf["providedNumericCreditScore"] = Preferences.Get("CreditRange", -1);

            //CardBenefits.SelectedIndex = Preferences.Get("CardBenefits", -1);
            //TypeOfAccount.SelectedIndex = Preferences.Get("TypeOfAccount", -1);
            //PaymentFrequency.SelectedIndex = Preferences.Get("PaymentFrequency", -1);
            //CurrentEmploymentStatus.SelectedIndex = Preferences.Get("CurrentEmploymentStatus", -1);
            //FinanceOfResidence.SelectedIndex = Preferences.Get("FinanceOfResidence", -1);
            //ResidenceType.SelectedIndex = Preferences.Get("ResidenceType", -1);
            //ReasonForLoan.SelectedIndex = Preferences.Get("ReasonForLoan", -1);
            //HighestEducationalDegree.SelectedIndex = Preferences.Get("HighestEducationalDegree", -1);
            //TypeOfReturnOfferWanted.SelectedIndex = Preferences.Get("TypeOfReturnOfferWanted", -1);

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream())) {
                streamWriter.Write(JsonConvert.SerializeObject(full));
                streamWriter.Flush();
                streamWriter.Close();
            }

            WebResponse httpResponse;
            String j = "{}";
            try {
                httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream())) {
                    j = streamReader.ReadToEnd();
                }
                JObject returnData = JObject.Parse(j);
                JArray pending = (JArray)returnData["savingsOffers"];

            } catch {
                Device.BeginInvokeOnMainThread(() => {
                    DisplayAlert("Alert", "Server Error", "Ok");
                });
            }

            JObject ret = JObject.Parse(j);
            await Clipboard.SetTextAsync(j);

            List<Dictionary<String, String>> DisplayData = new List<Dictionary<String, String>>();
            await Clipboard.SetTextAsync(ret["loanOffers"].ToString());
            foreach (JObject d in (JArray)ret["loanOffers"]) {
                Dictionary<string, string> temp = new Dictionary<string, string>();
                temp.Add("name", (String)d["originator"]["name"]);
                temp.Add("amount", (String)d["maxAmount"]);
                temp.Add("apr", (String)d["meanApr"]);
                temp.Add("monthly", (String)d["monthlyPayment"]);
                temp.Add("desc", (String)d["originator"]["description"]);
                temp.Add("image", (String)d["originator"]["images"][0]["url"]);
                temp.Add("length", (String)d["termLength"]);
                temp.Add("url", (String)d["url"]);
                DisplayData.Add(temp);
            }
            await Navigation.PushAsync(new OffersPage(DisplayData));
        }

        //this method is called when the button with the text "click here" is clicked (i called the method this, you can rename this and the clicked parameter in the .xaml file)
        void Handle_Clicked(object sender, System.EventArgs e) {
            Device.BeginInvokeOnMainThread(async () => {
                await DisplayAlert("Alert", "Hi", "Ok");
            });
        }

        void SearchTextChanged(object sender, Xamarin.Forms.TextChangedEventArgs e) {
            Preferences.Set("textVal", searchBar.Text);
        }

        //First is social security number, second is phone number, third is birthday
        public String[] GetRandomValues() {

            String[] Birthdays = { "3/13/1989", "7/2/1968", "9/30/1996", "10/14/1985", "6/15/1959" };
            String[] PhoneNumbers = { "770429342", "4354068063", "585242314", "7816222308", "7813303202" };
            String[] SSN = { "622-37-9987", "770-42-9342", "528-92-9022", "505-974-1934", "012-50-5001" };

            int RB = new Random().Next(0, 5);
            int RPN = new Random().Next(0, 5);
            int RSS = new Random().Next(0, 5);

            return new String[] { Birthdays[RB], PhoneNumbers[RPN], SSN[RSS] };
        }

        void Handle_ItemTapped(object sender, Xamarin.Forms.ItemTappedEventArgs e) {
            Navigation.PushAsync(new OffersPage(JsonConvert.DeserializeObject<List<Dictionary<String, String>>>(item[list.IndexOf(e.Item as BetterSctruct)]["offerDetails"].ToString())));
        }

        public void OnDelete(object sender, EventArgs e)
        {
            Console.WriteLine(sender.ToString());
            DisplayAlert("Delete Context Action",  " delete context action", "OK");
        }


    }

}

/** Alert syntax

         Device.BeginInvokeOnMainThread(async () =>
         {
            await DisplayAlert("Alert", "Bad API Call", "Ok");
         });
*/
