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
using System.Reflection;

namespace ThePongMobile.ViewModels
{
    public class SetupPageViewModel : BaseViewModel
    {

        private bool _isEntryCodeCompleted = false;
        private bool _hasError;
        private string _schoolCode;
<<<<<<< HEAD
=======
        private string ConfigCode;
>>>>>>> Added TCP client
        private INavigationService _navigationService;
        private INetworkService _networkService;
        
        public override Type PageType => typeof(SetupPage);
        public ICommand EntryCompletedCommand { get; private set; }
        public ICommand ContinueCommand { get; private set; }

        public bool HasError 
        {
            get => _hasError; 
            set => SetValue(ref _hasError, value);  
        }

        public string SchoolCode
        {
            get => _schoolCode;
            set => SetValue(ref _schoolCode, value);
        }
        
        public SetupPageViewModel(INavigationService navigationService, INetworkService networkService)
        {
            _navigationService = navigationService;
            _networkService = networkService;
            EntryCompletedCommand = new Command(EntryCodeCompleted);
            ContinueCommand = new Command(ContinueButtonPressed);
<<<<<<< HEAD
=======
            GetConfigDataAsync();
        }

        private async void GetConfigDataAsync()
        {
            //Added a function here otherwise the user could enter the login details and click continue before the API call had been made, now much more unlikely
            var config =  await _networkService.GetConfigDataAsync();
            ConfigCode = config.Code;
>>>>>>> Added TCP client
        }

        private async void ContinueButtonPressed()
        {
            if (_isEntryCodeCompleted)
                await _navigationService.NavigateToAsync<LoginPageViewModel>();
            else if (!_isEntryCodeCompleted)
                HasError = true;
        }

<<<<<<< HEAD
        private async void EntryCodeCompleted()
        {
            var config = await _networkService.GetConfigDataAsync();
            
            if (config.Code == SchoolCode)
=======
        private void EntryCodeCompleted()
        {
            if (ConfigCode == SchoolCode)
>>>>>>> Added TCP client
            {
                _isEntryCodeCompleted = true;
                HasError = false;
            }
            else
              HasError = true;
        }
    }
}