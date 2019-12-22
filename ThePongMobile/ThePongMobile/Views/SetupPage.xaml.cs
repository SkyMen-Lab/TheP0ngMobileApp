using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ThePongMobile.Models;
using ThePongMobile.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ThePongMobile.Views
{
    public partial class SetupPage : ContentPage
    {
        SetupPageViewModel setupPageViewModel = new SetupPageViewModel();

        public bool isEntryCodeCompleted = false;
        public const string URL = "https://my-json-server.typicode.com/nnugget/TravelRecord/posts"; //Add database website url here
        public readonly HttpClient _client = new HttpClient();
        public SetupPage()
        {
            InitializeComponent();
        }

        private void ContinuePressed(object sender, EventArgs e)
        {
            if(isEntryCodeCompleted) 
                setupPageViewModel.ContinueButtonPressed();
            else
                SchoolCode.HasError = true;
        }

        private async void EntryCodeCompleted(object sender, EventArgs e)
        {
            string RawJSON = await _client.GetStringAsync(URL);
            ConfigData JsonData = JsonConvert.DeserializeObject<ConfigData>(RawJSON);
            string SchoolEntryCode = JsonData.Code;
            if (SchoolEntryCode == SchoolCode.Text)
            {
                isEntryCodeCompleted = true;
                SchoolCode.HasError = false;
            }
            else
                SchoolCode.HasError = true;
        }
    }
}