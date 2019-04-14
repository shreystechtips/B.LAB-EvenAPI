using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xamarin.Forms;
using Xamarin.Essentials;

namespace BuyingAssistant
{
    public partial class OffersPage : ContentPage
    {
        private List<Dictionary<String, String>> arr;
        public OffersPage(List<Dictionary<String, String>> arr)
        {
            InitializeComponent();
            this.arr = arr;
            offers.ItemsSource = getOffers();

            Device.BeginInvokeOnMainThread(async () =>
            {
                await DisplayAlert("Pro Tip", "Click on the icon to the top right to save all of your offers for a product, and reclick it to remove them.", "Thanks");
            });
            itemName.IsVisible = false;
            saveButton.IsVisible = false;
        }

        public class Dict2
        {
            public String name { get; set; }
            public String apr { get; set; }
            public String image { get; set; }
            public String url { get; set; }
        }

        List<Dict2> getOffers()
        {
            List<Dict2> dic = new List<Dict2>();
            Console.WriteLine(arr.Count);
            for (int i = 0; i < arr.Count; i++)
            {
                Dictionary<String, String> temp = arr[i];
                String a = (String)temp["apr"];
                String b = (String)temp["monthly"];
                String c = (String)temp["url"];
                String imageUrl = temp["image"];
                Console.WriteLine(a + "lols" + b + "lolsdfs" + c + "sfgsfgasf" + temp["image"]);
                dic.Add(new Dict2 { name = (String)temp["name"], apr = "APR: " + a, url = c, image = imageUrl });
            }
            return dic;
        }

        void Handle_Clicked(object sender, System.EventArgs e)
        {
            itemName.IsVisible = true;
            saveButton.IsVisible = true;
        }
            
        private void Offers_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            Device.OpenUri(new Uri((e.Item as Dict2).url));
        }

        void Handle_Clicked_1(object sender, System.EventArgs e)
        {
            Dictionary<String,String > s = new Dictionary<String, String>();
            s.Add("cost", MainPage.text);
            s.Add("itemName", itemName.Text);
            JArray saveList = JArray.Parse(Preferences.Get("savedItems", "[]"));
            saveList.Add(JsonConvert.DeserializeObject(JsonConvert.SerializeObject(s)));
            Preferences.Set("savedItems", JsonConvert.SerializeObject(saveList));
            Navigation.PopAsync();
        }
    }
}