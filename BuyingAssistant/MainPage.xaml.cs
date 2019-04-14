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

        async private void init() {
            //tries to create the web request (it doesn't work right now, we need to research how to do this)
            /*
            BankBalance.Text = "trying";        
           JObject data = new JObject();

            WebRequest req = WebRequest.Create("https://api.evenfinancial.com/leads/rateTables");
                req.Method = "POST";
            req.ContentType = "application/json";
            BankBalance.Text = "created req";
                string postData = "";//"api_option=" + "paste" + "&api_paste_code=" + temp + "&api_dev_key=" + "add_api_key";
                byte[] byteArray = Encoding.UTF8.GetBytes(postData);
                req.ContentLength = byteArray.Length;

                Stream ds = req.GetRequestStream();
                ds.Write(byteArray, 0, byteArray.Length);
                BankBalance.Text = "write req";
                ds.Close();
                */

            var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://api.evenfinancial.com/leads/rateTables");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Headers["Authorization"] = "Bearer e7675dd3-ff3b-434b-95aa-70251cc3784b_88140dd4-f13e-4ce3-8322-6eaf2ee9a2d2";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream())) {
                string json = "{\"user\":\"test\"," +
                              "\"password\":\"bla\"}";

                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();
            }

            WebResponse httpResponse;
            String j;
            try {
                httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream())) {
                    j = streamReader.ReadToEnd();

                }
            } catch {
                Console.WriteLine("error 401");
                j = "ere";
            }
            Device.BeginInvokeOnMainThread(() => {
                DisplayAlert("Alert", j, "Ok");
                Console.WriteLine(j);
            });
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

            String[] Birthdays = {“3/13/1989”, “7/2/1968”, “9/30/1996”, “10/14/1985”, “6/15/1959” }
            String[] PhoneNumbers = {“770-42-9342”, “435-406-8063”, “585-24-2314”, “781-622-2308”, “781-330-3202”}
            String[] SSN = {“622-37-9987”, “770-42-9342”, “528-92-9022”, “505-974-1934”, “012-50-5001”}

            int RB = new Random().Next(1, 6);
            int RPN = new Random().Next(1, 6);
            int RSS = new Random().Next(1, 6);

            return new String[] {Birthdays[RB], PhoneNumbers[RPN], SSN[RSS]}
        }
    }
}

/** Alert syntax

         Device.BeginInvokeOnMainThread(async () =>
          {
            await DisplayAlert("Alert", "Bad API Call", "Ok");
          });
*/
