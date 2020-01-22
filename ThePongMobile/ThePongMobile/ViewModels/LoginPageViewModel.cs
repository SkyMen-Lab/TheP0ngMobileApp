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

        private INavigationService _navigationService;
        private INetworkService _networkService;
        private IStorageService<SettingsModel> _storageService;
        
        private bool _hasError;
        private string _gameCode;
        public bool HasError 
        {
            get => _hasError; 
            set => SetValue(ref _hasError, value);  
        }

        public string GameCode
        {
            get => _gameCode;
            set => SetValue(ref _gameCode, value);
        }

        public LoginPageViewModel(INavigationService navigationService, INetworkService networkService, IStorageService<SettingsModel> storageService)
        {
            _storageService = storageService;
            _navigationService = navigationService;
            _networkService = networkService;
            PlayButtonCommand = new Command(PlayButtonPressed);
        }
        
        private async void PlayButtonPressed()
        {
            //Checks login details here for navigation;
            
            ConnectionConfig config = new ConnectionConfig();
            int port = config.Port;
            string serverIP = config.IP;

            var data = _storageService.GetConfiguration();
            //================================================================//
            //Delete this once merged with setting connectionConfig + storage;
            port = 4545;
            serverIP = "10.0.2.2";
            //================================================================//
            int response = await _networkService.MakeHandshake(serverIP, port, data.SchoolCode, _gameCode);

            if (response == 1)
                await _navigationService.NavigateToAsync<MainPageViewModel>();
            else
            {
                HasError = true;
            }
        }
    }
 }
