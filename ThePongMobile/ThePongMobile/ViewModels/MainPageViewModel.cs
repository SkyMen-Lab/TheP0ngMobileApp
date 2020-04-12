using System;
using System.Windows.Input;
using ThePongMobile.Models;
using ThePongMobile.Services;
using ThePongMobile.ViewModels.Base;
using Xamarin.Forms;

namespace ThePongMobile.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        public override Type PageType => typeof(MainPage);

        private INetworkService _networkService;
        private INavigationService _navigationService;
        private IStorageService<SettingsModel> _storageService;
        private string GameCode;
        public MainPageViewModel(INetworkService networkService, INavigationService navigationService, IStorageService<SettingsModel> storageService)
        {
            _networkService = networkService;
            _navigationService = navigationService;
            _storageService = storageService;
            GameCode = LoginPageViewModel._Gamecode;
            Move = new Command<int>(MoveCommand);
            Back = new Command(BackCommand);
        }
        
        public ICommand Move { get; set; }
        public ICommand Back { get; set; }


        private int _score;

        public int Score
        {
            get => _score;
            set => SetValue(ref _score, value);
        }

        private void MoveCommand(int direction)
        {
            _networkService.SendMessage(direction);
        }

        public async void BackCommand()
        {
            var certain = await _navigationService.DisplayConfirmation("Exit Game", "Are you sure you want to leave the game?", "OK", "Cancel");
            if (certain)
            {
                var data = _storageService.GetConfiguration();
                int response = await _networkService.LeaveGame(data.IP, data.Port, data.SchoolCode, GameCode, false, data.ConnectionType);
                if (response == 200)
                    await _navigationService.PreviousPage();
            }
        }
    }
}