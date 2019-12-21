using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThePongMobile.Services;
using ThePongMobile.ViewModels;
using Xamarin.Forms;

namespace ThePongMobile
{
    public partial class MainPage : ContentPage
    {
        NetworkService _networkService = new NetworkService();
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new MainPageViewModel(_networkService);
        }
        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}