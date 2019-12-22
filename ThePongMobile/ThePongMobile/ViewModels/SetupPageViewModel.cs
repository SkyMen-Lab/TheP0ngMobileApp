using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Windows.Input;
using ThePongMobile.Models;
using ThePongMobile.Views;
using Xamarin.Forms;

namespace ThePongMobile.ViewModels
{
    public class SetupPageViewModel : BaseViewModel
    {
        public ICommand EntryCompletedCommand { get; private set; }
        public ICommand ContinueCommand { get; private set; }

        public bool isEntryCodeCompleted = false;
        public const string URL = "https://my-json-server.typicode.com/nnugget/TravelRecord/posts"; //Add database website url here
        public readonly HttpClient _client = new HttpClient();
        
        private bool _haserror;
        private string _schoolcode;
        private readonly PageService _pageService;

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
        
        public SetupPageViewModel(PageService pageservice)
        {
            _pageService = pageservice;

            EntryCompletedCommand = new Command(EntryCodeCompleted);
            ContinueCommand = new Command(ContinueButtonPressed);
        }

        private async void ContinueButtonPressed()
        {
            if (isEntryCodeCompleted)
                await _pageService.PushAsync(new LoginPage());
            else
                HasError = true;
        }

        private async void EntryCodeCompleted()
        {
            string RawJSON = await _client.GetStringAsync(URL);
            ConfigData JsonData = JsonConvert.DeserializeObject<ConfigData>(RawJSON);
            string SchoolEntryCode = JsonData.Code;
            if (SchoolEntryCode == SchoolCode)
            {
                isEntryCodeCompleted = true;
                HasError = false;
            }
            else
              HasError = true;
        }
    }
}
