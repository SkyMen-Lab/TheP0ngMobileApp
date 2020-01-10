using System;
using System.Threading.Tasks;
using ThePongMobile.Services;
using ThePongMobile.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XF.Material.Forms.UI;
using ThePongMobile.ViewModels;
using ThePongMobile.ViewModels.Base;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]

namespace ThePongMobile
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            XF.Material.Forms.Material.Init(this);
            InitApp(true);
        }

        protected override async void OnStart()
        {
            // Handle when your app starts
            await InitNavigation();
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        private Task InitNavigation()
        {
            var navService = IoContainer.Resolve<INavigationService>();
            return navService.InitAsync();
        }

        private void InitApp(bool useMocks)
        {
            IoContainer.RegisterServices(useMocks);
        }
    }
}