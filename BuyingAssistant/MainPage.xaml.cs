using System;
using System.IO;
using Plugin.Clipboard;
using Xamarin.Forms;
using System.Text;
using System.Net;
using Xamarin.Essentials;

namespace BuyingAssistant {
    //Searching for a loan, user enters the product they want and its costs
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            init();
        }
        async private void init()
        {
            //tries to create the web request (it doesn't work right now, we need to research how to do this)
            test.Text = "trying";
                WebRequest req = WebRequest.Create("https://api.evenfinancial.com/leads/rateTables");

                req.Method = "POST";
                req.ContentType = "application/json";
                test.Text = "created req";
                string postData = "";//"api_option=" + "paste" + "&api_paste_code=" + temp + "&api_dev_key=" + "add_api_key";
                byte[] byteArray = Encoding.UTF8.GetBytes(postData);
                req.ContentLength = byteArray.Length;

                Stream ds = req.GetRequestStream();
                ds.Write(byteArray, 0, byteArray.Length);
                test.Text = "write req";
                ds.Close();
        
            try
            {
                WebResponse wr = req.GetResponse();
                ds = wr.GetResponseStream();
                StreamReader reader = new StreamReader(ds);

                String ret = await reader.ReadToEndAsync();
                CrossClipboard.Current.SetText(ret);
                await DisplayAlert("Success", ret, "OK");
            }
            catch
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await DisplayAlert("Alert", "Bad API Call", "Ok");
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
    }
}