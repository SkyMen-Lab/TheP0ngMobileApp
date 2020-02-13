using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Windows.Input;
using ThePongMobile.Models;
using ThePongMobile.Services;
using ThePongMobile.Views;
using Xamarin.Forms;
using System.Reflection;
using ThePongMobile.ViewModels.Base;

namespace ThePongMobile.ViewModels
{
    public class SetupPageViewModel : BaseViewModel
    {

        private bool _hasError;
        private string _schoolCode;
        private INavigationService _navigationService;
        private INetworkService _networkService;
        private IStorageService<SettingsModel> _storageService;
        
        public override Type PageType => typeof(SetupPage);
        public ICommand ContinueCommand { get; private set; }
        

        public bool HasError 
        {
            get => _hasError; 
            set => SetValue(ref _hasError, value);  
        }

        public string SchoolCode
        {
            get => _schoolCode;
            set => SetValue(ref _schoolCode, value);
        }
        
        public SetupPageViewModel(INavigationService navigationService, INetworkService networkService, IStorageService<SettingsModel> storageService)
        {
            _navigationService = navigationService;
            _networkService = networkService;
            _storageService = storageService;
            ContinueCommand = new Command(ContinueButtonPressed);
        }
        

        private async void ContinueButtonPressed()
        {
            var schoolData = await _networkService.GetSchoolDataAsync(_schoolCode);
            if (schoolData != null && schoolData.Config != null)
            {
                var settings = new SettingsModel()
                {
                    SchoolName = schoolData.Name,
                    SchoolCode = SchoolCode,
                    ConnectionType = schoolData.Config.ConnectionType,
                    IP = schoolData.Config.IP,
                    Port = schoolData.Config.Port
                };
                _storageService.SetConfiguration(settings);
                await _navigationService.NavigateToAsync<LoginPageViewModel>();
            }
            else
                HasError = true;
        }
    }
}