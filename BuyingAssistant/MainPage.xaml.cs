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

namespace BuyingAssistant {
    //Searching for a loan, user enters the product they want and its costs
    public partial class MainPage : ContentPage {
        public MainPage() {
            InitializeComponent();
            init();
            populateList();
        }

        public void populateList() {
            JObject s = JObject.Parse(Preferences.Get("savedOffers", "{}"));
            Dictionary<String, String> vals = new Dictionary<string, string>();
            for (int i = 0; i < s.Count; i++) {
                JArray temp = (JArray)s[i]; 
                vals.Add((string)temp["key"], (string)temp["val"]);
            }
            savedList.ItemsSource = vals;
        }

        async private void init()
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://api.evenfinancial.com/leads/rateTables");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Headers["Authorization"] = "Bearer e7675dd3-ff3b-434b-95aa-70251cc3784b_88140dd4-f13e-4ce3-8322-6eaf2ee9a2d2";
            httpWebRequest.Method = "POST";

            //reads file
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "BuyingAssistant.hardCode.json";
            string result;
            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                result = reader.ReadToEnd();
            }


            JObject full = JObject.Parse(result);
            JObject personal = (JObject)full["personalInformation"];
            personal["firstName"] = Preferences.Get("FirstName", "");
            personal["lastName"] = Preferences.Get("LastName", "");
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

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                streamWriter.Write(JsonConvert.SerializeObject(full));
                streamWriter.Flush();
                streamWriter.Close();
            }

            WebResponse httpResponse;
            String j = "{}";
            try
            {
                httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    j = streamReader.ReadToEnd();
                }
                JObject returnData = JObject.Parse(j);
                testData.Text = j;
                JArray pending = (JArray)returnData["savingsOffers"];
                try
                {
                    JObject temp = (JObject)pending["details"];
                    String bank = temp["name"].ToString();
                    String rate = temp["rate"].ToString();
                    offer.Text = bank + ": " + rate;
                    savingsOfferUri = pending["url"].ToString();
                }
                catch
                {
                    moneyLabel.IsVisible = false;
                    offer.IsVisible = false;
                }
            }
            catch
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    DisplayAlert("Alert", "Server Error", "Ok");
                });
            }

            JObject ret = JObject.Parse(j);
            await Clipboard.SetTextAsync(j);

            List<Dictionary<String, String>> DisplayData = new List<Dictionary<String, String>>();
            foreach (JObject d in ret["loanOffers"])
            {
                d["name"] = ret["name"];
                d["amount"] = ret["maxAmount"];
                d["apr"] = ret["meanApr"];
                d["desc"] = ret["originator"]["description"];
                d["image"] = ret["originator"]["images"][0]["url"];
                d["length"] = ret["termLength"];
                d["url"] = ret["url"];
            }
        }

        //this method is called when the button with the text "click here" is clicked (i called the method this, you can rename this and the clicked parameter in the .xaml file)
        void Handle_Clicked(object sender, System.EventArgs e) {
            Device.BeginInvokeOnMainThread(async () => {
                await DisplayAlert("Alert", "Hi", "Ok");
            });
        }

        void SearchTextChanged(object sender, Xamarin.Forms.TextChangedEventArgs e) {

        }
        
        //First is social security number, second is phone number, third is birthday
        String[] GetRandomValues(){

            String[] Birthdays = {“3/13/1989”, “7/2/1968”, “9/30/1996”, “10/14/1985”, “6/15/1959” };
            String[] PhoneNumbers = {“770-42-9342”, “435-406-8063”, “585-24-2314”, “781-622-2308”, “781-330-3202”};
            String[] SSN = {“622-37-9987”, “770-42-9342”, “528-92-9022”, “505-974-1934”, “012-50-5001”};

            int RB = new Random().Next(1, 6);
            int RPN = new Random().Next(1, 6);
            int RSS = new Random().Next(1, 6);

            return new String[] {Birthdays[RB], PhoneNumbers[RPN], SSN[RSS]};
        }
    }
}

/** Alert syntax

         Device.BeginInvokeOnMainThread(async () =>
          {
            await DisplayAlert("Alert", "Bad API Call", "Ok");
          });
*/
