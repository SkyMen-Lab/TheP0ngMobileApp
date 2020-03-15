using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using ThePongMobile.Services;
using ThePongMobile.ViewModels.Base;
using ThePongMobile.Views;
using Xamarin.Forms;

namespace ThePongMobile.ViewModels
{
    public class AboutPageViewModel : BaseViewModel
    {
        public override Type PageType => typeof(AboutPage);
        public ICommand LeaveCommand { get; set; }
        private INavigationService _navigationService;
        public AboutPageViewModel(INavigationService navigation)
        {
            _navigationService = navigation;
            LeaveCommand = new Command(LeaveButtonPressed);
        }
        private async void LeaveButtonPressed()
        {
            await _navigationService.PreviousModalPage();
        }
    }
}
