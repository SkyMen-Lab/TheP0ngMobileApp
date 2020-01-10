﻿using System;
using ThePongMobile.Services;
using ThePongMobile.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XF.Material.Forms.UI;
using ThePongMobile.ViewModels;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]

namespace ThePongMobile
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            XF.Material.Forms.Material.Init(this);
            MainPage = new MaterialNavigationPage(new SetupPage(IoContainer.Resolve<SetupPageViewModel>()));
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        private void InitApp(bool useMocks) 
        {
            //TODO: init navigation and dependencies
        }
    }
}