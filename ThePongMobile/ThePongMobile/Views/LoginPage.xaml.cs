using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ThePongMobile.Models;
using ThePongMobile.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ThePongMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }
        protected override bool OnBackButtonPressed()
        {
            return true;
        }

        private void PlayButtonClicked(object sender, EventArgs e)
        {
            LoginPageViewModel.PlayButtonPressed();
        }

        private void GameSessionCodeEntered(object sender, EventArgs e)
        {
            
        }
    }
}