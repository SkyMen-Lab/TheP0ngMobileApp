using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ThePongMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainScreenPage : ContentPage
    {
        public MainScreenPage()
        {
            InitializeComponent();
        }
        protected override bool OnBackButtonPressed()
        {
            return true;
        }

        private void UpArrowClicked(object sender, EventArgs e)
        {

        }

        private void DownArrowClicked(object sender, EventArgs e)
        {

        }
    }
}