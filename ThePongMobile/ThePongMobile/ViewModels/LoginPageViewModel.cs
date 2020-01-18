using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using ThePongMobile.Models;
using ThePongMobile.Services;
using ThePongMobile.ViewModels.Base;
using ThePongMobile.Views;
using Xamarin.Forms;

namespace ThePongMobile.ViewModels
{
    public class LoginPageViewModel : BaseViewModel
    {
        public override Type PageType => typeof(LoginPage);

        public ICommand PlayButtonCommand { get; private set; }
        public ICommand CompletedLoginCommand { get; private set; }

        private INavigationService _navigationService;
        private INetworkService _networkService;

        private bool _isEntryCodeCompleted = false;
        private bool _hasError;
        private string _schoolCode;
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

        public LoginPageViewModel(INavigationService navigationService, INetworkService networkService)
        {
            _navigationService = navigationService;
            _networkService = networkService;
            PlayButtonCommand = new Command(PlayButtonPressed);
            CompletedLoginCommand = new Command(EnteredLoginEntry);
        }
        
        private async void PlayButtonPressed()
        {
            //Checks login details here for navigation;

            if (_isEntryCodeCompleted)
                await _navigationService.NavigateToAsync<MainPageViewModel>();
            else
            {
                HasError = true;
                _isEntryCodeCompleted = false;
            }
        }
        private void EnteredLoginEntry()
        {
            ConnectionConfig config = new ConnectionConfig();
            int port = config.Port;
            string serverIP = config.Ip;
            //================================================================//
            //Delete this once merged with setting connectionConfig + storage;
            port = 4545;
            serverIP = "10.0.2.2";
            //================================================================//
            int response = _networkService.MakeHandshake(serverIP, port, SchoolCode);
            if (response == 1)
            {
                _isEntryCodeCompleted = true;
            }
            else
            {
                _isEntryCodeCompleted = false;
                HasError = true;
            }
        }
     }
 }
