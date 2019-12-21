using System;
using System.Collections.Generic;
using System.Text;
using ThePongMobile.Views;
using Xamarin.Forms;

namespace ThePongMobile.ViewModels
{
    public class LoginPageViewModel
    {
        public static async void PlayButtonPressed()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new MainPage());
        }
    }
}
