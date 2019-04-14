using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xamarin.Forms;

namespace BuyingAssistant
{
    public partial class OffersPage : ContentPage
    {
        private JArray arr;
        public OffersPage(JArray arr)
        {
            InitializeComponent();
            offers.ItemsSource = getOffers();
            this.arr = arr;
        }

        public class Dict2
        {
            public String avgApr { get; set; }
            public String monthlyPay { get; set; }
            public String url { get; set; }
        }

        List<Dict2> getOffers()
        {
            List<Dict2> dic = new List<Dict2>();
            for (int i = 0; i < arr.Count; i++)
            {
                JObject temp = (JObject)arr[i];
                String a = (String) temp["apr"];
                String b = (String) temp["monthlyPayment"];
                String c = (String) temp["url"];
                dic.Add (new Dict2 { avgApr = a, monthlyPay = b, url = c });
            }
            return dic;
        }

        private void Offers_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            Device.OpenUri(new Uri((e.Item as Dict2).url));
        }
    }
}