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
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SetupPage : ContentPage
    {
        
        public SetupPage()
        {
            InitializeComponent();
        }

        private void ContinuePressed(object sender, EventArgs e)
        {
                SetupPageViewModel.ContinueButtonPressed();
        }

        private async void EntryCodeCompleted(object sender, EventArgs e)
        {
            string RawJSON = await _client.GetStringAsync(URL);
            ConfigData JsonData = JsonConvert.DeserializeObject<ConfigData>(RawJSON);
            string EntryCode = JsonData.Code;
            if (EntryCode == SchoolCode.Text)
            {
                isEntryCodeCompleted = true;
                SchoolCode.HasError = false;
            }
            else
                SchoolCode.HasError = true;
        }
    }
}