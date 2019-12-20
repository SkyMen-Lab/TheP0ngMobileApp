using System;
using System.Collections.Generic;
using System.Text;
using ThePongMobile.Views;
using Xamarin.Forms;

namespace ThePongMobile.ViewModels
{
    public class SetupPageViewModel
    {
        public static async void ContinueButtonPressed()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new LoginPage());
        }
    }
}
