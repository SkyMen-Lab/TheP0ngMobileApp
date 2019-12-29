using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Windows.Input;
using ThePongMobile.Models;
using ThePongMobile.Services;
using ThePongMobile.Views;
using Xamarin.Forms;

namespace ThePongMobile.ViewModels
{
    public class SetupPageViewModel : BaseViewModel
    {
        public ICommand EntryCompletedCommand { get; private set; }
        public ICommand ContinueCommand { get; private set; }

        private bool isEntryCodeCompleted = false;
        private const string URL = "https://my-json-server.typicode.com/nnugget/TravelRecord/posts"; //Add database website url here
        private readonly HttpClient _client = new HttpClient();
        private bool _haserror;
        private string _schoolcode;
        private string JsonSchoolEntryGameCode;
        public bool HasError 
        {
            get => _haserror; 
            set => SetValue(ref _haserror, value);  
        }

        public string SchoolCode
        {
            get => _schoolcode;
            set => SetValue(ref _schoolcode, value);
        }
        
        public SetupPageViewModel()
        {
            GetJsonData();
            EntryCompletedCommand = new Command(EntryCodeCompleted);
            ContinueCommand = new Command(ContinueButtonPressed);
        }
        private async void GetJsonData()
        {
            string RawJSON = await _client.GetStringAsync(URL);
            ConfigData JsonData = JsonConvert.DeserializeObject<ConfigData>(RawJSON);
            JsonSchoolEntryGameCode = JsonData.Code;
        }

        private async void ContinueButtonPressed()
        {
            if (isEntryCodeCompleted)
                await Containers._navigationService.PushAsync(new LoginPage());
            else if (!isEntryCodeCompleted)
                HasError = true;
        }

        private void EntryCodeCompleted()
        {
            
            if (JsonSchoolEntryGameCode == SchoolCode)
            {
                isEntryCodeCompleted = true;
                HasError = false;
            }
            else
              HasError = true;
        }
    }
}
