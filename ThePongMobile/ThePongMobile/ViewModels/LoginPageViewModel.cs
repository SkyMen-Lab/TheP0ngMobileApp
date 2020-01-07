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
        public ICommand PlayButtonCommand { get; private set; }
        public ICommand CompletedLoginCommand { get; private set; }

        public LoginPageViewModel()
        {

            PlayButtonCommand = new Command(PlayButtonPressed);
            CompletedLoginCommand = new Command(EnteredLoginEntry);
        }
        private async void PlayButtonPressed()
        {
            await Containers._navigationService.PushAsync(new MainPage());

        }
        private void EnteredLoginEntry()
        {
            //Checks login details here for navigation;
        }
     }
 }
