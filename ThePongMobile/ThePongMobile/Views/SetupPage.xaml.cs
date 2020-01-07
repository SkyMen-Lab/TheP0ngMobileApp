using ThePongMobile.Services;
using ThePongMobile.ViewModels;
using Xamarin.Forms;
namespace ThePongMobile.Views
{
    public partial class SetupPage : ContentPage
    {
        public SetupPage()
        {
            BindingContext = new SetupPageViewModel();
            InitializeComponent();
        }
    }
}