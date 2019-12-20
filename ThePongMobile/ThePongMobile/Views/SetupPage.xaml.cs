using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThePongMobile.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ThePongMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SetupPage : ContentPage
    {
        public SetupPage()
        {
            InitializeComponent();
        }

        private void ContinuePressed(object sender, EventArgs e)
        {
            SetupPageViewModel.ContinueButtonPressed();
        }

        private void EntryCodeCompleted(object sender, EventArgs e)
        {
            //Checks code and requests configs from the server(leave for now)
        }
    }
}