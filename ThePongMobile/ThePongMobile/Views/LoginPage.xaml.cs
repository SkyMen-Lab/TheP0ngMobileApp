using Newtonsoft.Json;
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
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        private bool isEntryCodeCompleted = false;
        private const string URL = "https://jsonplaceholder.typicode.com/posts"; //Add database website url here
        private HttpClient _client = new HttpClient();
        public LoginPage()
        {
            InitializeComponent();
        }
        protected override bool OnBackButtonPressed()
        {
            return true;
        }

        private void PlayButtonClicked(object sender, EventArgs e)
        {
            if (isEntryCodeCompleted)
                LoginPageViewModel.PlayButtonPressed();
        }

        private async Task GameSessionCodeEnteredAsync(object sender, EventArgs e)
        {
            string RawJSON = await _client.GetStringAsync(URL);
            ConfigData JsonData = JsonConvert.DeserializeObject<ConfigData>(RawJSON);
            string EntryCode = JsonData.Code;
            if (EntryCode == GameCode.Text)
            {
                isEntryCodeCompleted = true;
                GameCode.HasError = false;
            }
            else
                GameCode.HasError = true;
        }
    }
}