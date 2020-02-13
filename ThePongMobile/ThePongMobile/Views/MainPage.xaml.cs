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
        public MainPage()
        {
            InitializeComponent();
        }
        protected override bool OnBackButtonPressed()
        {
            //TODO: return to Login Screen

            //It might be better to leave it disabled because it is easy to accidently press during the game
            //and rejoining may be frustrating for users.
            return true;
        }
    }
}