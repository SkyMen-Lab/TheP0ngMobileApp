using System.Windows.Input;
using ThePongMobile.Services;
using Xamarin.Forms;

namespace ThePongMobile.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        public MainPageViewModel()
        {
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
            Containers._networkService.SendMessage(direction);
            Score = Containers._networkService.ReceiveScore();
        }
    }
}