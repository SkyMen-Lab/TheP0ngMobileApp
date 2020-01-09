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
        public MainPage(MainPageViewModel mainPageViewModel)
        {
            InitializeComponent();
            BindingContext = mainPageViewModel;
        }
        protected override bool OnBackButtonPressed()
        {
            //TODO: return to Login Screen
            return true;
        }
    }
}