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

        public static string _Gamecode;

        public ICommand PlayButtonCommand { get; private set; }
        public ICommand ResetButtonCommand { get; private set; }
        public ICommand TeamCodeHelpCommand { get; private set; }
        public ICommand AboutPageCommand { get; private set; }

        

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
            TeamCodeHelpCommand = new Command(TeamCodeHelp);
            AboutPageCommand = new Command(AboutPagePressed);
        }
        private async void AboutPagePressed()
        {
           await _navigationService.PushModalAsync<AboutPageViewModel>();
        }
        private async void TeamCodeHelp()
        {
             await _navigationService.DisplayInformation("Where to Find the Game Code?"
                 , "If you are unsure about what the game code is or where to find it" +
                 ", you should ask a member of our team to find out more."
                 , "OK");
        }

        private async void ResetButtonPressed()
        {
            bool response = await _navigationService.DisplayConfirmation("Reset School Code", "Are you sure you want to reset your school code?", "OK", "Cancel");
            if(response)
            {
                _storageService.ClearConfiguration();
                await _navigationService.NavigateToAsync<SetupPageViewModel>();
            }
        }
        
        private async void PlayButtonPressed()
        {
            var data = _storageService.GetConfiguration();
            if(_gameCode != null)
            {
                int response = await _networkService.JoinGame(data.IP, data.Port, data.SchoolCode, _gameCode, true);
                if (response == 200)
                {
                    _Gamecode = _gameCode;
                    await _navigationService.NavigateToAsync<MainPageViewModel>();
                }
                else
                {
                    HasError = true;
                }
            }
            else
            {
                HasError = true;
            }
               
            
        }
    }
 }
