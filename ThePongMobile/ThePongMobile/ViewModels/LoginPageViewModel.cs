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

        public static string Gamecode;

        public ICommand PlayButtonCommand { get; private set; }
        public ICommand ResetButtonCommand { get; private set; }

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
            set => SetValue(ref _gameCode, value?.ToUpper());
        }

        public LoginPageViewModel(INavigationService navigationService, INetworkService networkService, IStorageService<SettingsModel> storageService)
        {
            _storageService = storageService;
            _navigationService = navigationService;
            _networkService = networkService;
            PlayButtonCommand = new Command(PlayButtonPressed);
            ResetButtonCommand = new Command(ResetButtonPressed);
        }

        private async void ResetButtonPressed()
        {
            bool response = await _navigationService.DisplayAlert("Reset School Code", "Are you sure you want to reset your school code?", "OK", "Cancel");
            if(response)
            {
                _storageService.ClearConfiguration();
                await _navigationService.NavigateToAsync<SetupPageViewModel>();
            }
        }
        
        private async void PlayButtonPressed()
        {
            var data = _storageService.GetConfiguration();
            var isJoining = true;

            int response = await _networkService.MakeHandshake(data.IP, data.Port, data.SchoolCode, _gameCode, isJoining);
            if (response == 200)
            {
                Gamecode = _gameCode;
                await _navigationService.NavigateToAsync<MainPageViewModel>();
            }
            else
                HasError = true;
            
        }
    }
 }
