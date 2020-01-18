using System;
using System.Windows.Input;
using ThePongMobile.Services;
using ThePongMobile.ViewModels.Base;
using Xamarin.Forms;

namespace ThePongMobile.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        public override Type PageType => typeof(MainPage);

        private INetworkService _networkService;
        public MainPageViewModel(INetworkService networkService)
        {
            _networkService = networkService;
            Move = new Command<int>(MoveCommand);
        }
        
        public ICommand Move { get; set; }

        private int _score;

        public int Score
        {
            get => _score;
            set => SetValue(ref _score, value);
        }

        private void MoveCommand(int direction)
        {
            _networkService.SendMessage(direction);
            Score = _networkService.ReceiveScore();
        }
    }
}