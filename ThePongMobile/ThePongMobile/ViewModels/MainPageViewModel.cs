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
        public MainPageViewModel(INetworkService networkService, INavigationService navigationService, IStorageService<SettingsModel> storageService)
        {
            _networkService = networkService;
            _navigationService = navigationService;
            _storageService = storageService;
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
            var data = _storageService.GetConfiguration();
            var isJoining = false;
            var userLeaving = "Leaving Game";
            try
            {
                await _networkService.MakeHandshake(data.IP, data.Port, data.SchoolCode, userLeaving, isJoining);
            }
            catch
            {

            }
            await _navigationService.PreviousPage();
        }
    }
}