using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using ThePongMobile.Services;
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
<<<<<<< HEAD

        public LoginPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
=======
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
>>>>>>> Added TCP client
            PlayButtonCommand = new Command(PlayButtonPressed);
            CompletedLoginCommand = new Command(EnteredLoginEntry);
        }
        private async void PlayButtonPressed()
        {
<<<<<<< HEAD
            await _navigationService.NavigateToAsync<MainPageViewModel>();

        }
        private void EnteredLoginEntry()
        {
            //Checks login details here for navigation;
=======
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
            //TODO: set connection port + Server IP:
            int port = 4545;
            //10.0.2.2 is so that this can work on emulator device.
            string serverIP = "10.0.2.2";

            int response = _networkService.SendSchoolCode(serverIP, port, SchoolCode);
            if (response == 1)
            {
                _isEntryCodeCompleted = true;
            }
            else
            {
                _isEntryCodeCompleted = false;
                HasError = true;
            }
>>>>>>> Added TCP client
        }
     }
 }
