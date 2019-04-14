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
using System.Net.Http;
using System.Collections;

namespace BuyingAssistant {
    //Searching for a loan, user enters the product they want and its costs
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            init();
            populateList();
        }

        String savingsOfferUri;

        public void populateList()
        {
            JObject s =  JObject.Parse(Preferences.Get("savedOffers", "{}"));
            Dictionary<String, String> vals = new Dictionary<string, string>();
            for(int i = 0; i < s.Count; i++)
            {
                JArray temp = (JArray) s[i];
                vals.Add ((string) temp["key"],(string) temp["val"]);
            }
            savedList.ItemsSource = vals;
        }

        async private void init()
        {
            //tries to create the web request (it doesn't work right now, we need to research how to do this)
           var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://api.evenfinancial.com/leads/rateTables");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Headers["Authorization"] = "Bearer e7675dd3-ff3b-434b-95aa-70251cc3784b_88140dd4-f13e-4ce3-8322-6eaf2ee9a2d2";
            httpWebRequest.Method = "POST";

using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
{
    string json = "{\"user\":\"test\"," +
                  "\"password\":\"bla\"}";

    streamWriter.Write(json);
    streamWriter.Flush();
    streamWriter.Close();
}
            WebResponse httpResponse;
            String j;
            try
            {
                httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                     j = streamReader.ReadToEnd();
                }
                await Clipboard.SetTextAsync(j);
                JObject returnData = JObject.Parse(j);
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
            catch (HttpRequestException)
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    DisplayAlert("Alert", "Request Error", "Ok");
                });
            }
        }

        //this method is called when the button with the text "click here" is clicked (i called the method this, you can rename this and the clicked parameter in the .xaml file)
        void Handle_Clicked(object sender, System.EventArgs e)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                await DisplayAlert("Alert", "Hi", "Ok");
            });
        }

        void Handle_ItemTapped(object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {
        }

        void SearchTextChanged(object sender, Xamarin.Forms.TextChangedEventArgs e)
        { }

        void adClicked(object sender, System.EventArgs e)
        {
            Device.OpenUri(new Uri(savingsOfferUri));
        }

    }
}