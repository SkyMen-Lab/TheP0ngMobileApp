using System;
using System.Threading.Tasks;
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


        public static bool InGame = false;
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
            InGame = true;
        }
        public MainPageViewModel()
        {
            
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
            InGame = false;
            await _navigationService.PreviousPage();
            var certain = await _navigationService.DisplayConfirmation("Exit Game", "Are you sure you want to leave the game?", "OK", "Cancel");
            if (certain)
            {
                int response = await Leave(false);
                if (response == 200)
                {
                    await _navigationService.NavigateToAsync<LoginPageViewModel>();
                    InGame = false;
                }
            }
        }
        public async void OnAppSleep() => await Leave(true);
        private async Task<int> Leave(bool sleep)
        {
            var data = _storageService.GetConfiguration();
            int response = await _networkService.LeaveGame(data.IP, data.Port, data.SchoolCode, GameCode, false);
            if (sleep)
            {
                InGame = false;
                await _navigationService.NavigateToAsync<LoginPageViewModel>();
            }
            return response;
        }


    }
}